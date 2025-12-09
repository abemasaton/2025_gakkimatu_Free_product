using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace orizinal_mineseeeper
{
    internal class Modespecial : Button
    {
        // 有効時の色
        private Color _OnColor = Color.LightYellow;
        // 無効時の色
        private Color _OffColor = Color.DarkGray;
        // モード変更用変数
        private int Modeflag;

        private Form1 _form1;

        private Modeopen _modeopen;

        private Modepoint _modepoint;

        private ModetateSp _modetatesp;

        public Modespecial(Form1 Form1)
        {

            _form1 = Form1;

            Modeflag = 2;

            Location = new Point(_form1.Width - 250, _form1.Height - 300);
            Size = new Size(70, 70);

            BackColor = _OffColor;

            Text = ("⇕");

            Font = new Font(this.Font.OriginalFontName, 36);

            Click += ClickEvent;
        }
        public void ModeOff()
        {
            BackColor = _OffColor;
        }
        public void GetotherModebutton(Modeopen modeopen, Modepoint modepoint, ModetateSp modetatesp)
        {
            _modeopen = modeopen;
            _modepoint = modepoint;
            _modetatesp = modetatesp;
        }
        public void ClickEvent(object sender, EventArgs e)
        {
            BackColor = _OnColor;
            _form1.ChangeMode(Modeflag);
            _modeopen.ModeOff();
            _modepoint.ModeOff();
            _modetatesp.ModeOff();
        }
    }
}
