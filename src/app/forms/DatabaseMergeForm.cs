using chess_pos_db_gui.src.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        private List<Entry> Entries { get; set; }

        public DatabaseMergeForm()
        {
            InitializeComponent();

            Entries = new List<Entry>();

            entriesView.Columns.Add("one");
            entriesView.Columns.Add("two");

            entriesView.VirtualListSize = 0;

            WinFormsControlUtil.MakeDoubleBuffered(entriesView);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Entries.Add(Entry.MakeEntry(i.ToString(), (ulong)i));
            i += 1;

            entriesView.VirtualListSize = Entries.Count;
        }

        private void entriesView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            var entry = Entries[e.ItemIndex];
            e.Item = new ListViewItem(new string[] { entry.GetFormattedName(), entry.GetFormattedSize() });
        }

        private void MakeGroup(List<int> indices)
        {
            int insertIndex = Entries.Count;
            Entry groupHeader = Entry.MakeGroupHeader();
            List<Entry> groupedEntries = new List<Entry>();
            foreach (int index in indices)
            {
                var entry = Entries[index];
                if (entry.IsGroupHeader)
                {
                    continue;
                }

                // We look for the minimal index
                insertIndex = Math.Min(index, insertIndex);

                entry.SetParent(groupHeader);
                groupedEntries.Add(entry);
            }

            Entries.RemoveAll(entry => entry.Parent == groupHeader);

            // We may have stolen something from a different group. 
            // We have to find the end of it to insert the new group.
            if (insertIndex < Entries.Count && Entries[insertIndex].Parent != null)
            {
                var baseParent = Entries[insertIndex].Parent;
                while (insertIndex < Entries.Count && Entries[insertIndex].Parent == baseParent)
                    ++insertIndex;
            }

            Entries.InsertRange(insertIndex, groupedEntries);
            Entries.Insert(insertIndex, groupHeader);

            RemoveEmptyGroups(Entries);

            entriesView.VirtualListSize = Entries.Count;

            entriesView.SelectedIndices.Clear();

            entriesView.Refresh();
        }

        private void makeGroupButton_Click(object sender, EventArgs e)
        {
            var selectedIndices = entriesView.SelectedIndices;
            if (selectedIndices.Count < 1)
            {
                return;
            }

            List<int> indices = new List<int>();
            foreach(int i in selectedIndices)
            {
                indices.Add(i);
            }

            MakeGroup(indices);
        }

        private void RemoveEmptyGroups(List<Entry> entries)
        {
            HashSet<Entry> usedGroups = new HashSet<Entry>();
            foreach (var entry in entries)
            {
                if (entry.IsGroupHeader || entry.Parent == null)
                {
                    continue;
                }

                usedGroups.Add(entry.Parent);
            }

            entries.RemoveAll(e => e.IsGroupHeader && !usedGroups.Contains(e));
        }

        private void entriesView_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemoveSelectionOnGroups();
        }

        private void entriesView_VirtualItemsSelectionRangeChanged(object sender, ListViewVirtualItemsSelectionRangeChangedEventArgs e)
        {
            RemoveSelectionOnGroups();
        }

        private void RemoveSelectionOnGroups()
        {
            foreach(int index in entriesView.SelectedIndices)
            {
                if (Entries[index].IsGroupHeader)
                {
                    entriesView.SelectedIndices.Remove(index);
                    break;
                }
            }
        }

        private List<List<Entry>> GetGroupedEntries(List<Entry> entries)
        {
            var groups = new List<List<Entry>>();

            foreach(Entry e in entries)
            {
                if (e.IsGroupHeader)
                {
                    groups.Add(new List<Entry>());
                }
                else if (e.Parent != null)
                {
                    groups.Last().Add(e);
                }
            }

            return groups;
        }

        private void entriesView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (Entries.Count > 1)
            {
                List<int> indices = new List<int>();
                foreach (int index in entriesView.SelectedIndices)
                {
                    indices.Add(index);
                }

                this.DoDragDrop(indices, DragDropEffects.Move);
            }
        }

        private void entriesView_DragDrop(object sender, DragEventArgs e)
        {
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
        }
    }

    class Entry
    {
        private static readonly string indent = "    ";
        private static ulong nextUniqueId = 0;

        private ulong UniqueId { get; set; }

        public bool IsGroupHeader { get; private set; }
        public Entry Parent { get; private set; } 

        public string Name { get; private set; }
        public ulong Size { get; private set; }

        public static Entry MakeGroupHeader()
        {
            return new Entry
            {
                UniqueId = nextUniqueId++,
                IsGroupHeader = true,
                Parent = null,
                Name = "GROUP",
                Size = 0
            };
        }

        public static Entry MakeEntry(string name, ulong size)
        {
            return new Entry
            {
                UniqueId = nextUniqueId++,
                IsGroupHeader = false,
                Parent = null,
                Name = name,
                Size = size
            };
        }

        private Entry()
        {

        }

        public string GetFormattedName()
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

        public string GetFormattedSize()
        {
            if (Parent != null)
            {
                return indent + Size.ToString();
            }
            else
            {
                return Size.ToString();
            }
        }

        public void SetParent(Entry e)
        {
            Parent = e;
        }

        public override bool Equals(object obj)
        {
            return obj is Entry entry &&
                   UniqueId == entry.UniqueId;
        }

        public override int GetHashCode()
        {
            return -401120461 + UniqueId.GetHashCode();
        }
    }

}
