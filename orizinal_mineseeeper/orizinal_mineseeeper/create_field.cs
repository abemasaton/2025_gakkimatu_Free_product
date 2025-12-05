using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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

        private int tate; // 縦の座標

        private int yoko; // 横の座標

        public bool mineflag; // 地雷の設定

        private int minesum; // 地雷の総数

        private int restFieldArea;  // 残りの地雷のないマスの数

        private Color _CloseColor = Color.LightSkyBlue;

        private Color _OpenColor = Color.White;

        private int modeflag; // 0:開ける　1:旗　2:一マス開け　3:縦一列開け　4:横一行開け

        private bool Openedmas = false; // マスが開いているかの確認

        private bool flagedflag = false; // 旗が立っているかの確認

        private int specialstock; // スペシャルの残り数

        private int tateSpstock;

        private bool perfectflag = true; // 完璧なクリアか判定

        private bool startOpen = false;

        public create_field (Form1 Form1,int FieldSize,int _tate,int _yoko,int Minesum)
        {
            // Form1の参照
            _Form1 = Form1;
            // 縦横の個数
            tateyokoSize = FieldSize;
            // 地雷のないマスの数
            restFieldArea = FieldSize * FieldSize　- Minesum;
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
            // 地雷の数の設定
            minesum = Minesum;

            Click += FirstClickEvent;
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
            BackColor = Color.OrangeRed;
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
                Image = null;
                flagedflag = false;
                _Form1.flagedCounter(false);
            }
            else
            {
                Bitmap resizedImage = new Bitmap(global::orizinal_mineseeeper.Properties.Resources.Whiteflag, new Size(50, 50));
                Image = resizedImage; // 画像を挿入
                flagedflag = true;
                _Form1.flagedCounter(true);
            }
        }

        public void specialopen()
        {
            if (mineflag)
            {
                if (Openedmas == false)
                {
                    if (flagedflag == false)
                    {
                        Bitmap resizedImage = new Bitmap(global::orizinal_mineseeeper.Properties.Resources.Whiteflag, new Size(50, 50));
                        Image = resizedImage; // 画像を挿入
                        flagedflag = true;
                        _Form1.flagedCounter(true);
                    }
                }
            }
            else
            {
                if (flagedflag)
                {
                    flagedflag = false;
                    _Form1.flagedCounter(false);
                }
                Openfield();
            }
        }

        public void ClickEvent(object sender, EventArgs e)
        {
            int i;
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
                specialstock = _Form1.Returnspecialstock();
                if (specialstock >= 0) _Form1.Getfieldbutton(tate, yoko).specialopen();
            }
            if (modeflag == 3)
            {
                tateSpstock = _Form1.ReturntateSpstock();
                if (tateSpstock >= 0)
                {
                    for (i = 0; i < tateyokoSize; i++)
                    {
                        _Form1.Getfieldbutton(i, yoko).specialopen();
                    }
                }
            }

            i = 0;
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
                        if (_Form1.Getfieldbutton(i, j).Openedmas && _Form1.Getfieldbutton(i, j).mineflag)
                        {
                            perfectflag = false;
                        }
                        else if (_Form1.Getfieldbutton(i, j).flagedflag != _Form1.Getfieldbutton(i, j).mineflag)
                        {
                            _Form1.Getfieldbutton(i, j).BackColor = Color.GreenYellow;
                            _Form1.Getfieldbutton(i, j).Click -= ClickEvent;
                            perfectflag = false;
                        }
                    }
                }
                if(perfectflag) MessageBox.Show("完璧にクリア! ");
            }
        }
        public void FirstClickEvent(object sender, EventArgs e)
        {
            // ランダムのインスタンス?
            Random random = new Random();

            int i, j;

            for (i = tate - 1;i <= tate + 1; i++)
            {
                for (j = yoko - 1; j <= yoko + 1; j++)
                {
                    if (_Form1.Getfieldbutton(i, j) != null)
                    {
                        _Form1.Getfieldbutton(i, j).mineflag = false;
                        _Form1.Getfieldbutton(i, j).deleteFirstEvent();
                        _Form1.Getfieldbutton(i, j).plusClickEvent();
                        _Form1.Getfieldbutton(i, j).startOpen = true;
                        restFieldArea--;
                    }
                }
            }
            for (i = 0; i < tateyokoSize; i++)
            {
                for (j = 0; j < tateyokoSize; j++)
                {
                    if(_Form1.Getfieldbutton(i, j).startOpen == false)
                    {
                        _Form1.Getfieldbutton(i, j).deleteFirstEvent();
                        _Form1.Getfieldbutton(i, j).plusClickEvent();

                        if (minesum <= 0) _Form1.Getfieldbutton(i, j).mineflag = false;

                        else if (restFieldArea <= 0) _Form1.Getfieldbutton(i, j).mineflag = true;

                        else if (random.Next(minesum + restFieldArea) < minesum)
                        {
                            _Form1.Getfieldbutton(i, j).mineflag = true;
                            minesum--;
                        }
                        else
                        {
                            _Form1.Getfieldbutton(i, j).mineflag = false;
                            restFieldArea--;
                        }
                    }
                }
            }
            for (i = tate - 1; i <= tate + 1; i++)
            {
                for (j = yoko - 1; j <= yoko + 1; j++)
                {
                    if (_Form1.Getfieldbutton(i, j) != null)
                    {
                        _Form1.Getfieldbutton(i, j).Openfield();
                    }
                }
            }
        }
        public void deleteFirstEvent()
        {
            Click -= FirstClickEvent;
        }
        public void plusClickEvent()
        {
            Click += ClickEvent;
        }
    }
}