using System;
using System.Collections.Generic;
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

        private int tateyokoSize;  // 縦の長さ

        private int FieldArea;  // マスの数

        private int tate; // 縦の座標

        private int yoko; // 横の座標

        private bool mineflag; // 地雷の設定

        private int minesum; // 地雷の総数

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
            // 初期の色設定
            BackColor = Color.LightSkyBlue;
            // 地雷の設定
            mineflag = onmine;

        }
    }
}
