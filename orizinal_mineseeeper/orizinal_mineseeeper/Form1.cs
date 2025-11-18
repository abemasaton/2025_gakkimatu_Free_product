using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace orizinal_mineseeeper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int FieldSize = int.Parse(banmensize.Text);
                if (FieldSize < 5 || FieldSize > 20)
                {
                    MessageBox.Show("5～20の範囲で入力してください");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("正しい値を入力してください");
                return;
            }
            button1.Visible = false;
            banmensize.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
        }
    }
}
