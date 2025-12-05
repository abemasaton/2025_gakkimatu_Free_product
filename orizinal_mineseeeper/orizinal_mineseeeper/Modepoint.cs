using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


namespace orizinal_mineseeeper
{
    internal class Modepoint : Button
    {
        // 有効時の色
        private Color _OnColor = Color.LightYellow;
        // 無効時の色
        private Color _OffColor = Color.DarkGray;
        // モード変更用変数
        private int Modeflag;

        private Form1 _form1;

        private Modeopen _modeopen;

        private Modespecial _modespecial;

        private ModetateSp _modetatesp;

        public Modepoint(Form1 Form1)
        {

            _form1 = Form1;

            Modeflag = 1;

            Location = new Point(_form1.Width - 250, _form1.Height - 370);

            Size = new Size(70, 70);

            BackColor = _OffColor;

            Image = global::orizinal_mineseeeper.Properties.Resources.Whiteflag;

            Bitmap resizedImage = new Bitmap(global::orizinal_mineseeeper.Properties.Resources.Whiteflag, new Size(50, 50));
            Image = resizedImage; // 画像を挿入

            Click += ClickEvent;
        }
        public void ModeOff()
        {
            BackColor = _OffColor;
        }
        public void GetotherModebutton(Modeopen modeopen, Modespecial modespecial, ModetateSp modetatesp)
        {
            _modeopen = modeopen;
            _modespecial = modespecial;
            _modetatesp = modetatesp;
        }
        public void ClickEvent(object sender, EventArgs e)
        {
            BackColor = _OnColor;
            _form1.ChangeMode(Modeflag);
            _modeopen.ModeOff();
            _modespecial.ModeOff();
            _modetatesp.ModeOff();
        }
    }
}
