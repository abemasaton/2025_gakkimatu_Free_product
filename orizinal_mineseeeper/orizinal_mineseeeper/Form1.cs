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

        private static int modeflag = 0; // 0:開ける　1:旗　2:一マス開け　3:縦一列開け　4:横一行開け

        public static int specialstock; // スペシャルの残り使用可能回数

        private Labelspecialstock usespecialstock; //　ラベルの参照

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

            specialstock = FieldSize * 2;

            minesum = FieldSize * FieldSize / 3;
            notmine = FieldSize * FieldSize - minesum;

            bool mineflag;

            for (i = 0; i < FieldSize; i++)
            {
                for (j = 0; j < FieldSize; j++)
                {
                    ransuu = random.Next(minesum + notmine);
                    if (minesum == 0) mineflag = false;

                    else if (notmine == 0) mineflag = true;

                    else if (ransuu <= minesum)
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

                    // 配列にボタンの参照を追加
                    _buttonArray[i, j] = minefield;

                    Controls.Add(minefield);
                }
            }
            label3.Text = ($"地雷の数 = {FieldSize * FieldSize / 3}");

            Modeopen modeopenButton = new Modeopen(this); //　モードオープンのボタン生成

            Controls.Add(modeopenButton);

            Modepoint modepointButton = new Modepoint(this); // モード旗のボタン生成

            Controls.Add(modepointButton);

            Modespecial modespecialButton = new Modespecial(this); // モード旗のボタン生成

            Controls.Add(modespecialButton);

            modeopenButton.GetotherModebutton(modepointButton, modespecialButton);

            modepointButton.GetotherModebutton(modeopenButton, modespecialButton);

            modespecialButton.GetotherModebutton(modeopenButton, modepointButton);

            Labelspecialstock specialstocklabel = new Labelspecialstock(this, specialstock);

            usespecialstock = specialstocklabel;

            Controls.Add(specialstocklabel);
        }
        internal create_field Getfieldbutton(int x, int y)
        {
            if (x < 0 || x >= FieldSize) return null;
            if (y < 0 || y >= FieldSize) return null;
            return _buttonArray[x, y];
        }
        public void ChangeMode(int x)
        {
            modeflag = x;
        }
        public static int ReturnMode()
        {
            return modeflag;
        }
        public int Returnspecialstock()
        {
            if(specialstock >= 0) specialstock--;
            if (specialstock >= 0) usespecialstock.Usespecialstock(specialstock);
            return specialstock;
        }
    }
}
