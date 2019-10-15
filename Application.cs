using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_pos_db_gui
{
    public partial class Application : Form
    {
        public Application()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1["No", 0].Value = "1";
            dataGridView1["Move", 0].Value = "123321";
            dataGridView1["Count", 0].Value = "543";
            dataGridView1["WinCount", 0].Value = "432";
            dataGridView1["DrawCount", 0].Value = "321";
            dataGridView1["LossCount", 0].Value = "123";
            dataGridView1["Perf", 0].Value = "15%";
            dataGridView1["DrawPct", 0].Value = "10%";
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
