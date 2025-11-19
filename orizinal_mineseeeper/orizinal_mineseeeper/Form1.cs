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
                int Testtext = int.Parse(banmensize.Text);  // 入力された数値を保管
                if (Testtext < 5 || Testtext > 20)  // 数値が5～20以外をはじく
                {
                    MessageBox.Show("5～20の範囲で入力してください");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("正しい値を入力してください");  
                return;  // 正しく数値を受け取れなかったらはじく
            }
            int FieldSize = int.Parse(banmensize.Text);  // 入力された数値を保管
            button1.Visible = false;     //用済みのコントロール達を非表示
            banmensize.Visible = false;
            label1.Visible = false;
            label2.Visible = false;



            int i, j;
            for (i = 0; i < FieldSize; i++)
            {
                for (j = 0; j < FieldSize; j++)
                {
                    create_field minefield = new create_field(
                        this, FieldSize, i, j);

                    Controls.Add(minefield);
                }
            }
        }
    }
}
