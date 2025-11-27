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
    internal class create_field : Button
    {
        

        private Form1 _Form1;

        private int tateyokoSize;  // 縦の数

        private int FieldArea;  // マスの数

        private int tate; // 縦の座標

        private int yoko; // 横の座標

        public bool mineflag; // 地雷の設定

        private int minesum; // 地雷の総数

        private Color _CloseColor = Color.LightSkyBlue;

        private Color _OpenColor = Color.White;

        private int modeflag; // 0:開ける　1:旗　2:一マス開け　3:縦一列開け　4:横一行開け

        private bool Openedmas = false; // マスが開いているかの確認

        private bool flagedflag = false; // 旗が立っているかの確認

        private int specialstock; // スペシャルの残り数

        private bool perfectflag = true; // 完璧なクリアか判定

        public create_field (Form1 Form1,int FieldSize,int _tate,int _yoko, bool onmine)
        {
            // Form1の参照
            _Form1 = Form1;
            // 縦横の個数
            tateyokoSize = FieldSize;
            // フィールド全体のマスの数
            FieldArea = FieldSize * FieldSize;
            // 縦の場所の参照
            tate = _tate;
            // 横の場所の参照
            yoko = _yoko;
            // ボタンのサイズ設定
            Size = new Size((_Form1.Height - 40) / tateyokoSize, (_Form1.Height - 40) / tateyokoSize);
            // ボタンの位置設定
            Location = new Point(this.Size.Width * _yoko, this.Size.Height * _tate);
            // 文字サイズ設定
            Font = new Font(this.Font.OriginalFontName, (_Form1.Height - 40) / tateyokoSize / 2);
            // 初期の色設定
            BackColor = _CloseColor;
            // 地雷の設定
            mineflag = onmine;



            Click += ClickEvent;

        }
        public void Openfield()
        {
            int Cnt = 0;
            Cnt = countaroundmine(Cnt);
            Text = Cnt.ToString();
            BackColor = _OpenColor;
            Openedmas = true;
            this.Click -= ClickEvent; // クリックイベントの削除
        }
        public int countaroundmine(int C)
        {
            for (int i = 0; i < _CheckData.Length; i++)
            {
                var data = _CheckData[i];
                var button = _Form1.Getfieldbutton(tate + data[0], yoko + data[1]);

                if (button != null && button.mineflag)
                {
                    C++;
                }
            }
            return C;
        }
        public void Openmine()
        {
            Text = ("M");
            MessageBox.Show("あなたは地雷を踏みました lol");
            BackColor = _OpenColor;
            Openedmas = true;
            this.Click -= ClickEvent; // クリックイベントの削除
        }
        public void mineCheck() // 共通のオープン処理
        {
            if (mineflag)
            {
                Openmine();
            }
            else
            {
                Openfield();
            }
            
        }

        private int[][] _CheckData =
        {
            new int[]{ -1, -1},
            new int[]{ -1,  0},
            new int[]{ -1,  1},
            new int[]{  0,  1},
            new int[]{  0, -1},
            new int[]{  1, -1},
            new int[]{  1,  0},
            new int[]{  1,  1},
        };

        public void flagPoint()
        {
            if (flagedflag)
            {
                Text = ("");
                flagedflag = false;
            }
            else
            {
                Text = ("P");
                flagedflag = true;
            }
        }

        public void specialopen()
        {
            if (mineflag)
            {
                Text = ("P");
                flagedflag = true;
            }
            else
            {
                Openfield();
            }
        }

        public void ClickEvent(object sender, EventArgs e)
        {
            modeflag = Form1.ReturnMode();
            if (modeflag == 0 && flagedflag == false) // モードオープン
            {
                _Form1.Getfieldbutton(tate, yoko).mineCheck();
            }
            if (modeflag == 1) // モード旗
            {
                _Form1.Getfieldbutton(tate, yoko).flagPoint();
            }
            if (modeflag == 2)
            {
                specialstock = Form1.Returnspecialstock();
                if(specialstock >= 0) _Form1.Getfieldbutton(tate, yoko).specialopen();
            }

            int i = 0;
            bool finishflag = true;
            while (i < tateyokoSize && finishflag)
            {
                for (int j = 0; j < tateyokoSize; j++)
                {
                    if(_Form1.Getfieldbutton(i, j).Openedmas == false &&
                        _Form1.Getfieldbutton(i, j).flagedflag == false)
                    {
                        finishflag = false;
                    }
                }
                i++;
            }
            if (finishflag)
            {
                MessageBox.Show("終了　リザルトを表示します");
                for (i = 0; i < tateyokoSize; i++)
                {
                    for (int j = 0; j < tateyokoSize; j++)
                    {
                        if (_Form1.Getfieldbutton(i, j).Openedmas == _Form1.Getfieldbutton(i, j).mineflag
                            || _Form1.Getfieldbutton(i, j).flagedflag != _Form1.Getfieldbutton(i, j).mineflag)
                        {
                            _Form1.Getfieldbutton(i, j).BackColor = Color.OrangeRed;
                            _Form1.Getfieldbutton(i, j).Click -= ClickEvent;
                            perfectflag = false;
                        }
                    }
                }
                if(perfectflag) MessageBox.Show("完璧にクリア! ");
            }
        }
    }
}