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

        private static int Cntflag = 0; // 旗の数を数える

        private int specialstock; // スペシャルの残り使用可能回数

        private Labelspecialstock usespecialstock; //　sp回数ラベルの参照

        private int tateSpstock; // 縦スペシャルの残り使用回数

        private LabeltateSpstock usetateSpstock; // tatesp回数ラベルの参照

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int fieldTesttext = int.Parse(banmensize.Text);  // 入力された数値を保管
                if (fieldTesttext < 5 || fieldTesttext > 20)  // 数値が5～20以外をはじく
                {
                    MessageBox.Show("※フィールドサイズの設定\n" +
                        "5～20の範囲で入力してください");
                    return;
                }
                int mineTesttext = int.Parse(textBox1.Text);
                if (mineTesttext > fieldTesttext * fieldTesttext || mineTesttext < 0)
                {
                    MessageBox.Show($"※地雷の数の設定\n" +
                        $"0以上{fieldTesttext * fieldTesttext}以下で入力してください");
                    return;
                }
            }
            catch
            {
                if (banmensize.Text == "" || textBox1.Text == "")
                {
                    MessageBox.Show("値が入力されていません");
                    return;
                }
                MessageBox.Show("いずれかに使用できない物が含まれています");
                return;  // 正しく数値を受け取れなかったらはじく
            }
            FieldSize = int.Parse(banmensize.Text);  // 入力されたフィールドサイズを保管

            int minesum, notmine;

            minesum = int.Parse(textBox1.Text);  // 入力された地雷の数を保管
            notmine = FieldSize * FieldSize - minesum;

            label3.Text = ($"地雷の数 = {minesum}");
            label4.Text = ($"旗の数　　= {Cntflag}");

            button1.Visible = false;     //用済みのコントロール達を非表示
            banmensize.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            textBox1.Visible = false;

            // ランダムのインスタンス?
            Random random = new Random();

            _buttonArray = new create_field[FieldSize, FieldSize];

            int i, j, ransuu;

            specialstock = FieldSize * 2;
            tateSpstock = 1;

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

            Modeopen modeopenButton = new Modeopen(this); //　モードオープンのボタン生成

            Controls.Add(modeopenButton);

            Modepoint modepointButton = new Modepoint(this); // モード旗のボタン生成

            Controls.Add(modepointButton);

            Modespecial modespecialButton = new Modespecial(this); // モード旗のボタン生成

            Controls.Add(modespecialButton);

            ModetateSp modetateSpButton = new ModetateSp(this); // モード旗のボタン生成

            Controls.Add(modetateSpButton);

            modeopenButton.GetotherModebutton(modepointButton, modespecialButton, modetateSpButton); // モードボタン同士の参照

            modepointButton.GetotherModebutton(modeopenButton, modespecialButton, modetateSpButton);

            modespecialButton.GetotherModebutton(modeopenButton, modepointButton, modetateSpButton);

            modetateSpButton.GetotherModebutton(modeopenButton, modepointButton, modespecialButton);

            Labelspecialstock specialstocklabel = new Labelspecialstock(this, specialstock);

            usespecialstock = specialstocklabel;

            Controls.Add(specialstocklabel);

            LabeltateSpstock tateSpstocklabel = new LabeltateSpstock(this, tateSpstock);

            usetateSpstock = tateSpstocklabel;

            Controls.Add(tateSpstocklabel);
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
        public int ReturntateSpstock()
        {
            if (tateSpstock >= 0) tateSpstock--;
            if (tateSpstock >= 0) usetateSpstock.Usespecialstock(tateSpstock);
            return tateSpstock;
        }
        public void flagedCounter(bool plus)
        {
            if (plus) Cntflag++;
            else Cntflag--;
            label4.Text = ($"旗の数　　= {Cntflag}");
        }
    }
}
