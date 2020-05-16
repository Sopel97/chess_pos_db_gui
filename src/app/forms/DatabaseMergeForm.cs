using chess_pos_db_gui.src.util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Json;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace chess_pos_db_gui.src.app.forms
{
    // TODO: support only consecutive merging
    //       Currently there is no such databases, but there may in the future.
    //       It's harder to implement and actually define.
    public partial class DatabaseMergeForm : Form
    {
        int i = 0;
        private DatabaseProxy Database { get; set; }
        private Dictionary<string, List<DatabaseMergableFile>> MergableFiles { get; set; }
        private EntryGroups Groups { get; set; }
        private List<Entry> UnassignedEntries { get; set; }
        private string SelectedPartition { get; set; }

        public DatabaseMergeForm(DatabaseProxy database)
        {
            InitializeComponent();

            Groups = new EntryGroups();
            UnassignedEntries = new List<Entry>();

            unassignedEntriesView.Columns.Add("Name", 165);
            unassignedEntriesView.Columns.Add("Size", 85);
            unassignedEntriesView.VirtualListSize = 0;
            WinFormsControlUtil.MakeDoubleBuffered(unassignedEntriesView);

            entryGroupsView.Columns.Add("Name", 165);
            entryGroupsView.Columns.Add("Size", 85);
            entryGroupsView.VirtualListSize = 0;
            WinFormsControlUtil.MakeDoubleBuffered(entryGroupsView);

            tempStorageUsageUnitComboBox.SelectedItem = "GB";

            WinFormsControlUtil.MakeDoubleBuffered(totalMergeProgressBar);
            WinFormsControlUtil.MakeDoubleBuffered(subtotalMergeProgressBar);

            SelectedPartition = null;

            Database = database;
            MergableFiles = Database.GetMergableFiles();
            foreach (string partition in MergableFiles.Keys)
            {
                partitionComboBox.Items.Add(partition);
            }
            partitionComboBox.SelectedItem = partitionComboBox.Items[0];

            // This should be called by the event from above
            // ResetMergableFilesForCurrentPartition();

            maxTempStorageUsageCheckBox.Checked = false;
            UpdateEnabledStateOfMaxStorageInput();
        }

        private void RefreshData()
        {
            MergableFiles = Database.GetMergableFiles();
            foreach (string partition in MergableFiles.Keys)
            {
                partitionComboBox.Items.Add(partition);
            }
            partitionComboBox.SelectedItem = partitionComboBox.Items[0];

            ResetMergableFilesForCurrentPartition();
        }

        private void ResetMergableFilesForCurrentPartition()
        {
            unassignedEntriesView.VirtualListSize = 0;
            entryGroupsView.VirtualListSize = 0;

            Groups = new EntryGroups();
            UnassignedEntries = new List<Entry>();
            var partition = (string)partitionComboBox.SelectedItem;
            foreach (var file in MergableFiles[partition])
            {
                UnassignedEntries.Add(new Entry(file.Name, file.Size));
            }

            RefreshListViews();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            UnassignedEntries.Add(new Entry(i.ToString(), (ulong)i*1000000));
            i += 1;

            unassignedEntriesView.VirtualListSize = UnassignedEntries.Count;
        }

        private void unassignedEntriesView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            var entry = UnassignedEntries[e.ItemIndex];
            e.Item = new ListViewItem(new string[] { entry.GetFormattedName(), entry.GetFormattedSize() });
        }

        private void entryGroupsView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            var entry = Groups.ElementAt(e.ItemIndex);
            e.Item = new ListViewItem(new string[] { entry.GetFormattedName(), entry.GetFormattedSize() });
        }

        private void CreateGroupFromUnassignedEntries(List<int> indices)
        {
            List<Entry> groupedEntries = new List<Entry>();
            foreach (int index in indices)
            {
                groupedEntries.Add(UnassignedEntries[index]);
            }

            UnassignedEntries.RemoveAll(entry => groupedEntries.Contains(entry));

            Groups.Add(groupedEntries);

            RefreshListViews();
        }

        private List<List<string>> GetMergeGroups()
        {
            return Groups.GetMergeGroups();
        }

        private List<ulong> GetMergeGroupsSizes()
        {
            return Groups.GetMergeGroupsSizes();
        }

        private void RegroupGroupedEntries(List<int> indices)
        {
            List<Entry> groupedEntries = new List<Entry>();
            foreach (int index in indices)
            {
                var entry = (Entry)Groups.ElementAt(index);
                groupedEntries.Add(entry);
            }

            foreach(var entry in groupedEntries)
            {
                entry.RemoveGroup();
            }

            Groups.Add(groupedEntries);

            RefreshListViews();
        }

        private void AddUnassignedEntriesToGroup(EntryGroup group, List<int> indices)
        {
            List<Entry> groupedEntries = new List<Entry>();
            foreach (int index in indices)
            {
                groupedEntries.Add(UnassignedEntries[index]);
            }

            UnassignedEntries.RemoveAll(entry => groupedEntries.Contains(entry));

            group.AddRange(groupedEntries);

            RefreshListViews();
        }

        private void TransferGroupedEntriesToGroup(EntryGroup group, List<int> indices)
        {
            List<Entry> groupedEntries = new List<Entry>();
            foreach (int index in indices)
            {
                var entry = (Entry)Groups.ElementAt(index);
                if (entry.Parent == group)
                {
                    // We have to guard against self assignment
                    // because it can leave the group empty
                    // and it will be deleted before we assign to it.
                    continue;
                }

                groupedEntries.Add(entry);
            }

            foreach (var entry in groupedEntries)
            {
                entry.RemoveGroup();
            }

            group.AddRange(groupedEntries);

            RefreshListViews();
        }

        private void makeGroupButton_Click(object sender, EventArgs e)
        {
            var selectedIndices = unassignedEntriesView.SelectedIndices;
            if (selectedIndices.Count < 1)
            {
                return;
            }

            List<int> indices = new List<int>();
            foreach (int index in selectedIndices)
            {
                indices.Add(index);
            }

            CreateGroupFromUnassignedEntries(indices);
        }

        private int GetNumberOfInitialFiles()
        {
            var partition = (string)partitionComboBox.SelectedItem;
            if (partition == null)
            {
                return 0;
            }

            return MergableFiles[partition].Count;
        }

        private void RefreshListViews()
        {
            var cmp = new NaturalSortComparer(true);
            UnassignedEntries.Sort((a, b) => cmp.Compare(a.Name, b.Name));

            unassignedEntriesView.VirtualListSize = UnassignedEntries.Count;
            unassignedEntriesView.Refresh();

            Groups.Sort();
            entryGroupsView.VirtualListSize = Groups.ElementCount();
            entryGroupsView.Refresh();

            unassignedEntriesView.SelectedIndices.Clear();
            entryGroupsView.SelectedIndices.Clear();

            UpdateFileCounts();
        }

        private void UpdateFileCounts()
        {
            initialNumberOfFilesLabel.Text = "Initial number of files: " + GetNumberOfInitialFiles().ToString();
            var maxStorageUsage = GetMaxStorageUsage();
            var estimatedMerged = Groups.GetEstimatedNumberOfMergedFiles(maxStorageUsage);
            var leftUnmerged = UnassignedEntries.Count;
            var total = estimatedMerged + leftUnmerged;
            estimatedNumberOfFilesAfterMergingLabel.Text = "Estimated number of files after merging: " + total.ToString();
        }

        private ulong? GetMaxStorageUsage()
        {
            if (maxTempStorageUsageCheckBox.Checked)
            {
                decimal amount = tempStorageUsageSizeNumericUpDown.Value;
                decimal unit = GetUnitFromAbbreviation((string)tempStorageUsageUnitComboBox.SelectedItem);
                decimal bytes = amount * unit;
                return (ulong)bytes;
            }
            else
            {
                return null;
            }
        }

        private decimal GetUnitFromAbbreviation(string abbr)
        {
            switch (abbr)
            {
                case "MB":
                    return 1000m * 1000m;
                case "GB":
                    return 1000m * 1000m * 1000m;
                case "TB":
                    return 1000m * 1000m * 1000m * 1000m;
            }

            throw new ArgumentException("Invalid size abbreviation.");
        }

        private void StartDragFrom(ListView view)
        {
            if (view.VirtualListSize > 0)
            {
                List<int> indices = new List<int>();
                foreach (int index in view.SelectedIndices)
                {
                    indices.Add(index);
                }

                DoDragDrop(new DraggedSelection
                {
                    Parent = view,
                    Indices = indices
                }, DragDropEffects.Move);
            }
        }

        private void entryGroupsView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            StartDragFrom(entryGroupsView);
        }

        private void unassignedEntriesView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            StartDragFrom(unassignedEntriesView);
        }

        private void unassignedEntriesView_DragDrop(object sender, DragEventArgs e)
        {
            var ds = (DraggedSelection)e.Data.GetData(typeof(DraggedSelection));
            if (ds == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            if (ds.Parent == entryGroupsView)
            {
                Ungroup(ds.Indices);
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void entryGroupsView_DragDrop(object sender, DragEventArgs e)
        {
            var ds = (DraggedSelection)e.Data.GetData(typeof(DraggedSelection));
            if (ds == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            var clientPoint = entryGroupsView.PointToClient(new Point(e.X, e.Y));
            var dropItem = entryGroupsView.GetItemAt(0, clientPoint.Y);
            if (ds.Parent == unassignedEntriesView)
            {
                if (dropItem == null)
                {
                    CreateGroupFromUnassignedEntries(ds.Indices);
                }
                else
                {
                    var idx = dropItem.Index;
                    var element = Groups.ElementAt(idx);
                    var group = element.GetGroup();
                    AddUnassignedEntriesToGroup(group, ds.Indices);
                }
                e.Effect = DragDropEffects.Move;
            }
            else if (ds.Parent == entryGroupsView)
            {
                if (dropItem == null)
                {
                    RegroupGroupedEntries(ds.Indices);
                }
                else
                {
                    var idx = dropItem.Index;
                    var element = Groups.ElementAt(idx);
                    var group = element.GetGroup();
                    TransferGroupedEntriesToGroup(group, ds.Indices);
                }
                e.Effect = DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void GroupSelectionClosure()
        {
            foreach (int index in entryGroupsView.SelectedIndices)
            {
                var element = Groups.ElementAt(index);
                if (!element.IsSelectable())
                {
                    entryGroupsView.SelectedIndices.Remove(index);
                }

                if (element.GetGroup() == element)
                {
                    for(int i = 0; i < entryGroupsView.VirtualListSize; ++i)
                    {
                        var e = Groups.ElementAt(i);
                        if (e.IsSelectable() && e.GetGroup() == element)
                        {
                            entryGroupsView.SelectedIndices.Add(i);
                        }
                    }
                }
            }
        }

        private void entryGroupsView_SelectedIndexChanged(object sender, EventArgs e)
        {
            entryGroupsView.SelectedIndexChanged -= entryGroupsView_SelectedIndexChanged;
            GroupSelectionClosure();
            entryGroupsView.SelectedIndexChanged += entryGroupsView_SelectedIndexChanged;
        }

        private void entryGroupsView_VirtualItemsSelectionRangeChanged(object sender, ListViewVirtualItemsSelectionRangeChangedEventArgs e)
        {
            entryGroupsView.VirtualItemsSelectionRangeChanged -= entryGroupsView_VirtualItemsSelectionRangeChanged;
            GroupSelectionClosure();
            entryGroupsView.VirtualItemsSelectionRangeChanged += entryGroupsView_VirtualItemsSelectionRangeChanged;
        }

        private void Ungroup(List<int> indices)
        {
            List<Entry> entriesToRemove = new List<Entry>();
            foreach (int index in indices)
            {
                var element = Groups.ElementAt(index);
                if (element is Entry entry)
                {
                    entriesToRemove.Add(entry);
                }
            }

            foreach (var entry in entriesToRemove)
            {
                entry.RemoveGroup();
                UnassignedEntries.Add(entry);
            }

            RefreshListViews();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            var selectedIndices = entryGroupsView.SelectedIndices;
            if (selectedIndices.Count < 1)
            {
                return;
            }

            List<int> indices = new List<int>();
            foreach (int index in selectedIndices)
            {
                indices.Add(index);
            }

            Ungroup(indices);
        }

        private void unassignedEntriesView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void entryGroupsView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void partitionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newPartititon = (string)partitionComboBox.SelectedItem;
            if (SelectedPartition == null || SelectedPartition != newPartititon)
            {
                SelectedPartition = newPartititon;
                ResetMergableFilesForCurrentPartition();
            }
        }

        private void tempStorageUsageSizeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (maxTempStorageUsageCheckBox.Enabled)
            {
                UpdateFileCounts();
            }
        }

        private void tempStorageUsageUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (maxTempStorageUsageCheckBox.Enabled)
            {
                UpdateFileCounts();
            }
        }

        private void maxTempStorageUsageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateEnabledStateOfMaxStorageInput();
            UpdateFileCounts();
        }

        private void UpdateEnabledStateOfMaxStorageInput()
        {
            tempStorageUsageSizeNumericUpDown.Enabled = maxTempStorageUsageCheckBox.Checked;
            tempStorageUsageUnitComboBox.Enabled = maxTempStorageUsageCheckBox.Checked;
        }

        private void setPrimaryTempFolderButton_Click(object sender, EventArgs e)
        {
            UserSelectTemporaryDirectory(primaryTempFolderTextBox);
        }

        private void setSecondaryTempFolderButton_Click(object sender, EventArgs e)
        {
            UserSelectTemporaryDirectory(secondaryTempFolderTextBox);
        }

        private void UserSelectTemporaryDirectory(TextBox primaryTempFolderTextBox)
        {
            using (FolderBrowserDialog browser = new FolderBrowserDialog())
            {
                browser.ShowNewFolderButton = true;

                if (browser.ShowDialog() == DialogResult.OK)
                {
                    primaryTempFolderTextBox.Text = browser.SelectedPath;
                }
            }
        }

        private void clearPrimaryTempFolderButton_Click(object sender, EventArgs e)
        {
            primaryTempFolderTextBox.Clear();
        }

        private void clearSecondaryTempFolderButton_Click(object sender, EventArgs e)
        {
            secondaryTempFolderTextBox.Clear();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            PerformMerges();
        }

        private void DisableInput()
        {
            filesGroupBox.Enabled = false;
            tempDirsGroupBox.Enabled = false;
        }

        private void EnableInput()
        {
            filesGroupBox.Enabled = true;
            tempDirsGroupBox.Enabled = true;
        }

        private void PerformMerges()
        {
            DisableInput();

            var temps = GetTemporaryDirectories();
            var maxSpace = GetMaxStorageUsage();

            var mergeGroups = GetMergeGroups();
            var sizes = GetMergeGroupsSizes();
            var totalSize = (ulong)sizes.Sum(s => (long)s);
            ulong processedSize = 0;

            for(int i = 0; i < mergeGroups.Count; ++i)
            {
                var size = sizes[i];
                var group = mergeGroups[i];

                PerformMerge(temps, maxSpace, group, size, i, mergeGroups.Count);

                processedSize += size;
                SetTotalProgress(processedSize, totalSize);
            }

            MessageBox.Show("Merging process finished.");

            RefreshData();

            EnableInput();
        }

        private List<string> GetTemporaryDirectories()
        {
            List<string> dirs = new List<string>();

            if (primaryTempFolderTextBox.Text != null && primaryTempFolderTextBox.Text != "")
            {
                dirs.Add(primaryTempFolderTextBox.Text);
            }

            if (secondaryTempFolderTextBox.Text != null && secondaryTempFolderTextBox.Text != "")
            {
                dirs.Add(secondaryTempFolderTextBox.Text);
            }

            return dirs;
        }

        private void PerformMerge(List<string> temps, ulong? maxSpace, List<string> names, ulong size, int mergeIndex, int totalMerges)
        {
            void callback(JsonValue progressReport)
            {
                if (progressReport["operation"] == "merge")
                {
                    SetSubProgress(
                        (int)(progressReport["overall_progress"] * 100.0),
                        names.Count,
                        size,
                        mergeIndex,
                        totalMerges
                        );
                }
                else
                {
                    return;
                }
            }

            Database.Merge(SelectedPartition, names, temps, maxSpace, callback);
        }

        private void RefreshProgress()
        {
            subtotalMergeProgressBar.Refresh();
            totalMergeProgressBar.Refresh();
            subtotalMergeProgressLabel.Refresh();
            totalMergeProgressLabel.Refresh();
            currentOperationInfoLabel.Refresh();
        }

        private void SetTotalProgress(ulong processed, ulong total)
        {
            var pct = (int)(processed * 100 / total);
            totalMergeProgressBar.Value = pct;
            totalMergeProgressLabel.Text = pct.ToString() + "%";

            if (InvokeRequired)
            {
                Invoke(new Action(RefreshProgress));
            }
            else
            {
                RefreshProgress();
            }
        }

        private void SetSubProgress(int pct, int numFiles, ulong totalSize, int mergeIndex, int totalMerges)
        {
            subtotalMergeProgressBar.Value = pct;
            subtotalMergeProgressLabel.Text = pct.ToString() + "%";
            currentOperationInfoLabel.Text = string.Format(
                "Merge {0} out of {1}: Merging {2} files with total size {3}.",
                mergeIndex + 1,
                totalMerges,
                numFiles,
                FileSizeUtil.FormatSize(totalSize)
                );

            if (InvokeRequired)
            {
                Invoke(new Action(RefreshProgress));
            }
            else
            {
                RefreshProgress();
            }
        }
    }

    class DraggedSelection
    { 
        public ListView Parent { get; set; }
        public List<int> Indices { get; set; }
    }


    class Element
    {
        private static ulong nextUniqueId = 0;

        private readonly ulong id;

        protected Element()
        {
            id = nextUniqueId++;
        }

        public override bool Equals(object obj)
        {
            return obj is Element element &&
                   id == element.id;
        }

        public virtual string GetFormattedName() { return ""; }
        public virtual string GetFormattedSize() { return ""; }
        public virtual bool IsSelectable() { return false; }
        public virtual EntryGroup GetGroup() { return null; }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }

    class EntryGroups
    {
        private List<EntryGroup> Groups { get; set; }

        public int Count { get => Groups.Count; }

        public Element this[int i]
        {
            get => Groups[i];
        }

        public EntryGroups()
        {
            Groups = new List<EntryGroup>();
        }

        public void Add(List<Entry> entries)
        {
            Groups.Add(new EntryGroup(this, entries));
        }

        public Element ElementAt(int i)
        {
            foreach (EntryGroup e in Groups)
            {
                if (i == 0)
                {
                    return e;
                }
                i -= 1;

                if (i < e.Count)
                {
                    return e[i];
                }

                i -= e.Count;
            }

            return null;
        }

        public int ElementCount()
        {
            return Groups.Sum(g => g.Count + 1);
        }

        public void RemoveGroup(EntryGroup group)
        {
            Groups.Remove(group);
        }

        public void Sort()
        {
            foreach(var group in Groups)
            {
                group.Sort();
            }
        }

        public int GetEstimatedNumberOfMergedFiles(ulong? maxStorageUsage)
        {
            return Groups.Sum(g => g.GetEstimatedNumberOfMergedFiles(maxStorageUsage));
        }

        public List<List<string>> GetMergeGroups()
        {
            List<List<string>> groups = new List<List<string>>();

            foreach(var g in Groups)
            {
                groups.Add(g.GetAllNames());
            }

            return groups;
        }

        public List<ulong> GetMergeGroupsSizes()
        {
            List<ulong> sizes = new List<ulong>();

            foreach (var g in Groups)
            {
                sizes.Add(g.Size);
            }

            return sizes;
        }
    }

    class EntryGroup : Element
    {
        private EntryGroups Parent { get; set; }

        private List<Entry> Entries { get; set; }

        public int Count { get => Entries.Count; }

        public Entry this[int i] { get => Entries[i]; }

        public ulong Size { get => (ulong)Entries.Sum(e => (double)e.Size); }


        public EntryGroup(EntryGroups parent, List<Entry> entries) :
            base()
        {
            Parent = parent;
            Entries = entries;
            foreach (var e in Entries)
            {
                e.SetParent(this);
            }
        }

        public void Add(Entry e)
        {
            Entries.Add(e);
            e.SetParent(this);
        }

        public override string GetFormattedName()
        {
            return "Merge group:";
        }

        public override string GetFormattedSize()
        {
            return FileSizeUtil.FormatSize(Size, 0);
        }

        public override EntryGroup GetGroup()
        {
            return this;
        }

        public void RemoveEntry(Entry e)
        {
            Entries.Remove(e);
            if (Entries.Count == 0 && Parent != null)
            {
                Parent.RemoveGroup(this);
            }
        }

        public void AddRange(List<Entry> groupedEntries)
        {
            foreach(var entry in groupedEntries)
            {
                entry.SetParent(this);
            }
            Entries.AddRange(groupedEntries);
        }

        public void Sort()
        {
            var cmp = new NaturalSortComparer(true);
            Entries.Sort((a, b) => cmp.Compare(a.Name, b.Name));
        }

        public int GetEstimatedNumberOfMergedFiles(ulong? maxStorageUsageOpt)
        {
            if (!maxStorageUsageOpt.HasValue)
            {
                return 1;
            }

            var maxStorageUsage = maxStorageUsageOpt.Value;

            int estimatedFiles = 0;

            // set it as if the last group is full so we create a new one.
            ulong lastGroupSizeBytes = maxStorageUsage + 1;
            foreach (var e in Entries)
            {
                if (lastGroupSizeBytes + e.Size > maxStorageUsage)
                {
                    estimatedFiles += 1;
                    lastGroupSizeBytes = 0;
                }

                lastGroupSizeBytes += e.Size;
            }

            return estimatedFiles;
        }

        public List<string> GetAllNames()
        {
            return new List<string>(Entries.Select(e => e.Name));
        }
    }

    class Entry : Element
    {
        private static readonly string indent = "└─ ";

        public EntryGroup Parent { get; private set; } 

        public string Name { get; private set; }
        public ulong Size { get; private set; }

        public Entry(string name, ulong size) :
            base()
        {
            Parent = null;
            Name = name;
            Size = size;
        }

        public override string GetFormattedName()
        {
            if (Parent != null)
            {
                return indent + Name;
            }
            else
            {
                return Name;
            }
        }

        public override string GetFormattedSize()
        {
            string s = FileSizeUtil.FormatSize(Size, 0);
            if (Parent != null)
            {
                return indent + s;
            }
            else
            {
                return s;
            }
        }

        public override bool IsSelectable()
        {
            return true;
        }

        public override EntryGroup GetGroup()
        {
            return Parent;
        }

        public void SetParent(EntryGroup e)
        {
            Parent = e;
        }

        public void RemoveGroup()
        {
            if (Parent != null)
            {
                Parent.RemoveEntry(this);
                Parent = null;
            }
        }
    }

}
