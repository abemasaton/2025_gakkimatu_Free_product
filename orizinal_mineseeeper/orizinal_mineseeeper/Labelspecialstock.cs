using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace orizinal_mineseeeper
{
    internal class Labelspecialstock : Label
    {
        private Form1 _form1;

        private string _text;

        public Labelspecialstock(Form1 Form1, int stock)
        {
            _form1 = Form1;

            Location = new Point(_form1.Width - 180, _form1.Height - 250);
            Size = new Size(150, 60);

            _text = stock.ToString();

            Text = ($"×{_text}");

            Font = new Font(this.Font.OriginalFontName, 30);
        }
        public void Usespecialstock(int stock)
        {
            _text = stock.ToString();

            Text = ($"×{_text}");
        }
    }
}
