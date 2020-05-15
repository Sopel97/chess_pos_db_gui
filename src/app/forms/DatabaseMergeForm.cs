using chess_pos_db_gui.src.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_pos_db_gui.src.app.forms
{
    // TODO: support only consecutive merging
    //       Currently there is no such databases, but there may in the future.
    //       It's harder to implement and actually define.
    public partial class DatabaseMergeForm : Form
    {
        int i = 0;
        private EntryGroups Groups { get; set; }
        private List<Entry> UnassignedEntries { get; set; }

        public DatabaseMergeForm()
        {
            InitializeComponent();

            Groups = new EntryGroups();
            UnassignedEntries = new List<Entry>();

            unassignedEntriesView.Columns.Add("Name");
            unassignedEntriesView.Columns.Add("Size");
            unassignedEntriesView.VirtualListSize = 0;
            WinFormsControlUtil.MakeDoubleBuffered(unassignedEntriesView);

            entryGroupsView.Columns.Add("Name");
            entryGroupsView.Columns.Add("Size");
            entryGroupsView.VirtualListSize = 0;
            WinFormsControlUtil.MakeDoubleBuffered(entryGroupsView);

        }

        private List<Element> GetElementsByIndices(List<int> indices)
        {
            return null;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            UnassignedEntries.Add(new Entry(i.ToString(), (ulong)i));
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

        private void makeGroupButton_Click(object sender, EventArgs e)
        {
            var selectedIndices = unassignedEntriesView.SelectedIndices;
            if (selectedIndices.Count < 1)
            {
                return;
            }

            List<Entry> groupedEntries = new List<Entry>();
            foreach (int index in selectedIndices)
            {
                groupedEntries.Add(UnassignedEntries[index]);
            }

            UnassignedEntries.RemoveAll(entry => groupedEntries.Contains(entry));

            Groups.Add(groupedEntries);

            RefreshListViews();

            unassignedEntriesView.SelectedIndices.Clear();
        }

        private void RefreshListViews()
        {
            unassignedEntriesView.VirtualListSize = UnassignedEntries.Count;
            unassignedEntriesView.Refresh();

            entryGroupsView.VirtualListSize = Groups.ElementCount();
            entryGroupsView.Refresh();
        }

        private void entriesView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            /*
            if (Entries.Count > 1)
            {
                List<int> indices = new List<int>();
                foreach (int index in entriesView.SelectedIndices)
                {
                    indices.Add(index);
                }

                this.DoDragDrop(indices, DragDropEffects.Move);
            }
            */
        }

        private void entriesView_DragDrop(object sender, DragEventArgs e)
        {
            /*
            if (e.Data is List<int>)
            {
                var indices = e.Data as List<int>;

                var clientPoint = entriesView.PointToClient(new Point(e.X, e.Y));
                var dropItem = entriesView.GetItemAt(0, clientPoint.Y);
                if (dropItem != null)
                {
                    var index = dropItem.Index;
                    var entry = Entries[index];
                    if (entry.IsGroupHeader)
                    {

                    }
                }
            }
            */
        }

        private void DeselectGroups()
        {
            foreach (int index in entryGroupsView.SelectedIndices)
            {
                var element = Groups.ElementAt(index);
                if (!element.IsSelectable())
                {
                    entryGroupsView.SelectedIndices.Remove(index);
                    return;
                }
            }
        }

        private void entryGroupsView_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeselectGroups();
        }

        private void entryGroupsView_VirtualItemsSelectionRangeChanged(object sender, ListViewVirtualItemsSelectionRangeChangedEventArgs e)
        {
            DeselectGroups();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            var selectedIndices = entryGroupsView.SelectedIndices;
            if (selectedIndices.Count < 1)
            {
                return;
            }

            List<Entry> entriesToRemove = new List<Entry>();
            foreach (int index in selectedIndices)
            {
                var element = Groups.ElementAt(index);
                if (element is Entry entry)
                {
                    entriesToRemove.Add(entry);
                }
            }

            foreach(var entry in entriesToRemove)
            {
                entry.RemoveGroup();
                UnassignedEntries.Add(entry);
            }

            RefreshListViews();
        }
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

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }

    class EntryGroups
    {
        private List<EntryGroup> Groups { get; set; }

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
        }

        public override string GetFormattedName()
        {
            return "GROUP";
        }

        public override string GetFormattedSize()
        {
            return FileSizeUtil.FormatSize(Size, 0);
        }

        public void RemoveEntry(Entry e)
        {
            Entries.Remove(e);
            if (Entries.Count == 0 && Parent != null)
            {
                Parent.RemoveGroup(this);
            }
        }
    }

    class Entry : Element
    {
        private static readonly string indent = "    ";

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
