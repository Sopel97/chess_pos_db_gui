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
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Entries.Add(Entry.MakeEntry(i.ToString(), (ulong)i));
            i += 1;

            entriesView.VirtualListSize = Entries.Count;
        }

        private void entriesView_DragDrop(object sender, DragEventArgs e)
        {
            Console.WriteLine("dragdrop");
        }

        private void entriesView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            var entry = Entries[e.ItemIndex];
            e.Item = new ListViewItem(new string[] { entry.GetFormattedName(), entry.GetFormattedSize() });
        }

        private void makeGroupButton_Click(object sender, EventArgs e)
        {
            var selectedIndices = entriesView.SelectedIndices;
            if (selectedIndices.Count < 1)
            {
                return;
            }

            int insertIndex = Entries.Count;
            Entry groupHeader = Entry.MakeGroupHeader();
            List<Entry> groupedEntries = new List<Entry>();
            foreach (int index in selectedIndices)
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
