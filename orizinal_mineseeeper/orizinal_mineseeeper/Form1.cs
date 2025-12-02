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
                if (mineTesttext > fieldTesttext * fieldTesttext - 9 || mineTesttext < 0)
                {
                    MessageBox.Show($"※地雷の数の設定\n" +
                        $"0以上{fieldTesttext * fieldTesttext - 9}以下で入力してください");
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

            button2.Visible = true;

            _buttonArray = new create_field[FieldSize, FieldSize];

            int i, j;

            specialstock = FieldSize;
            tateSpstock = 1;

            for (i = 0; i < FieldSize; i++)
            {
                for (j = 0; j < FieldSize; j++)
                {
                    create_field minefield = new create_field(
                            this, FieldSize, i, j, minesum);

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

        private void textBox1_Click(object sender, EventArgs e)
        {

            try
            {
                int testtext = int.Parse(banmensize.Text);
                label6.Text = ($"({testtext * testtext - 9}以下 半角数字)");
            }
            catch
            {
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("   かんたんな説明\n\n" +
                "１.左に並んだマスは地雷の混ざったフィールド\n\n" +
                "　 最初にクリックしたマスと周囲８マスは地雷のないマスとしてひらかれる\n\n" +
                "２.右の４つのボタンはモード変更ボタン\n\n" +
                "　 黄色くなっているボタンが現在のモード\n\n" +
                "　 「O」のボタンはマスをひらくモード\n\n" +
                "　 「P」のボタンは旗を立てるモード\n\n" +
                "　 「S」のボタンは地雷があるなら旗をたてる\n\n" +
                "　 　 　　　　　　地雷がないならひらく　※回数制限あり\n\n" +
                "　 「⇕」のボタンはクリックしたマスの上下一列に「S」を使う\n\n" +
                "　 　 　　　　　　　　　　　　　　　　　※回数制限１回\n\n" +
                "３.すべてのマスがひらくか旗が立つと終了\n\n" +
                "　 ひらいた地雷のマスは赤く　地雷のないマスに立っていた旗は緑になる\n\n" +
                "　 赤と緑のマスが０個を目指そう");
        }
    }
}
