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

        private create_field[,] _buttonArray;

        private int FieldSize;

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
            FieldSize = int.Parse(banmensize.Text);  // 入力された数値を保管
            button1.Visible = false;     //用済みのコントロール達を非表示
            banmensize.Visible = false;
            label1.Visible = false;
            label2.Visible = false;

            // ランダムのインスタンス?
            Random random = new Random();

            _buttonArray = new create_field[FieldSize, FieldSize];

            int i, j, minesum, notmine, ransuu;

            minesum = FieldSize * FieldSize / 3;
            notmine = FieldSize * FieldSize - minesum;

            bool mineflag;

            for (i = 0; i < FieldSize; i++)
            {
                for (j = 0; j < FieldSize; j++)
                {
                    ransuu = random.Next(3);
                    if (minesum == 0) mineflag = false;

                    else if (notmine == 0) mineflag = true;

                    else if (ransuu == 0)
                    {
                        mineflag = true;
                        minesum = minesum - 1;
                    }
                    else
                    {
                        mineflag = false;
                        notmine = notmine - 1;
                    }

                        create_field minefield = new create_field(
                            this, FieldSize, i, j, mineflag);

                    Controls.Add(minefield);
                }
            }

        }
        
    }
}
