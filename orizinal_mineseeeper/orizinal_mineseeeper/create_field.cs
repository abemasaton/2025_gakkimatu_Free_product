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

        private Color _CloseColor = Color.LightSkyBlue; // ひらいていない色

        private Color _OpenColor = Color.White; // ひらいた色

        private int modeflag; // 0:開ける　1:旗　2:一マス開け　3:縦一列開け　4:横一行開け

        private bool Openedmas = false; // マスが開いているかの確認

        private bool flagedflag = false; // 旗が立っているかの確認

        private int specialstock; // つるはしの残り回数

        private int tateSpstock; // ⇕の残り回数

        private bool perfectflag = true; // 完璧なクリアか判定

        private bool startOpen = false; // 最初に開けたマスのフラグ

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
        /// <summary>
        /// 地雷のないマスを開ける動作
        /// </summary>
        public void Openfield()
        {
            int Cnt = 0;
            Cnt = countaroundmine(Cnt);
            Text = Cnt.ToString(); // 周りの地雷数表示
            BackColor = _OpenColor;
            Openedmas = true;
            Click -= ClickEvent; // 左クリックイベントの削除
            MouseDown -= RightMouseClick;  // 右クリックイベントの削除

            if (Cnt == 0) // 周囲に地雷が0なら
            {
                for (int i = 0; i < _CheckData.Length; i++)
                {
                    var data = _CheckData[i];
                    var button = _Form1.Getfieldbutton(tate + data[0], yoko + data[1]);

                    if (button != null && button.Openedmas == false)
                    {
                        button.Openfield(); // ひらく
                    }
                }
            }
        }
        /// <summary>
        /// 周囲８マスの地雷を数える
        /// </summary>
        /// <param name="C"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 地雷を開けた動作
        /// </summary>
        public void Openmine() 
        {
            Text = ("M");
            MessageBox.Show("あなたは地雷を踏みました lol");
            BackColor = Color.OrangeRed;
            Openedmas = true;
            Click -= ClickEvent;
            MouseDown -= RightMouseClick;  // クリックイベントの削除
        }
        /// <summary>
        /// 地雷があるマスかないマスか判定し別の処理を行う
        /// </summary>
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
        /// <summary>
        /// 周囲８マスを表す変数
        /// </summary>
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
        /// <summary>
        /// 旗を立てるか消す動作
        /// </summary>
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
                Bitmap resizedImage = new Bitmap(global::orizinal_mineseeeper.Properties.Resources.Whiteflag,
                    new Size((_Form1.Height - 40) / tateyokoSize - 5, (_Form1.Height - 40) / tateyokoSize - 5));
                Image = resizedImage; // 画像を挿入
                flagedflag = true;
                _Form1.flagedCounter(true);
            }
        }
        /// <summary>
        /// つるはしを使う動作
        /// </summary>
        public void turuhasiopen()
        {
            if (mineflag)
            {
                if (Openedmas == false)
                {
                    if (flagedflag == false)
                    {
                        Bitmap resizedImage = new Bitmap(global::orizinal_mineseeeper.Properties.Resources.Whiteflag,
                            new Size((_Form1.Height - 40) / tateyokoSize - 5, (_Form1.Height - 40) / tateyokoSize - 5));
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
        /// <summary>
        /// 通常のクリック動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            if (modeflag == 2) // モードつるはし
            {
                specialstock = _Form1.Returnspecialstock();
                if (specialstock >= 0) _Form1.Getfieldbutton(tate, yoko).turuhasiopen();
            }
            if (modeflag == 3) // モード⇕
            {
                tateSpstock = _Form1.ReturntateSpstock();
                if (tateSpstock >= 0)
                {
                    for (i = 0; i < tateyokoSize; i++)
                    {
                        _Form1.Getfieldbutton(i, yoko).turuhasiopen();
                    }
                }
            }
            Checkfinish();
        }
        /// <summary>
        /// すべてのマスが、ひらくか旗が立っているか確認し終了を判定
        /// </summary>
        private void Checkfinish()
        {
            int i = 0;
            bool finishflag = true;
            while (i < tateyokoSize && finishflag)
            {
                for (int j = 0; j < tateyokoSize; j++)
                {
                    if (_Form1.Getfieldbutton(i, j).Openedmas == false &&
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
                if (perfectflag) MessageBox.Show("完璧にクリア! ");
            }
        }
        /// <summary>
        /// 最初にマスを開く動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        _Form1.Getfieldbutton(i, j).plusRightClick();

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
            _Form1.Getfieldbutton(tate, yoko).Openfield();
        }
        public void deleteFirstEvent()
        {
            Click -= FirstClickEvent;
        }
        public void plusClickEvent()
        {
            Click += ClickEvent;
        }
        /// <summary>
        /// 右クリックで旗を動かす動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _Form1.Getfieldbutton(tate, yoko).flagPoint();
            }
            Checkfinish();
        }
        private void plusRightClick()
        {
            MouseDown += RightMouseClick;
        }
    }
}