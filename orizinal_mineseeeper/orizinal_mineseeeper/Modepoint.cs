using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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

        public Modepoint(Form1 Form1)
        {

            _form1 = Form1;

            Modeflag = 1;

            Location = new Point(_form1.Width - 250, _form1.Height - 380);

            Size = new Size(60, 60);

            BackColor = _OffColor;

            Text = ("P");

            Click += ClickEvent;
        }
        public void ModeOff()
        {
            BackColor = _OffColor;
        }
        public void GetotherModebutton(Modeopen Modeopen)
        {
            _modeopen = Modeopen;
        }
        public void ClickEvent(object sender, EventArgs e)
        {
            BackColor = _OnColor;
            _form1.ChangeMode(Modeflag);
            _modeopen.ModeOff();
        }
    }
}
