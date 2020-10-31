using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP4Lab
{
    public partial class PaintBox : Form
    {
        LinkedList Mylist;
        private Graphics g;
        private Bitmap bitmapDraw;
        public PaintBox()
        {
            InitializeComponent();
            Mylist = new LinkedList();
            bitmapDraw = new Bitmap(drawBox.Width, drawBox.Height);
            g = Graphics.FromImage(bitmapDraw);
        }

        public bool controlPressed()
        {
            return (Control.ModifierKeys & Keys.Control) == Keys.Control;
        }

        public void Draw()
        {
            g.Clear(Color.White);
            Mylist.front();
            while (!Mylist.eol())
            {
                if (Mylist.getObject().Current == true)
                    Mylist.getObject().FillDraw(bitmapDraw);
                else
                    Mylist.getObject().Draw(bitmapDraw);
                Mylist.next();
            }
            drawBox.Image = bitmapDraw;
        }

        public bool inCircle(int xPos, int yPos)
        {
            if (!controlPressed())
            {
                Mylist.front();
                while (!Mylist.eol())
                {
                    Mylist.getObject().Current = false;
                    Mylist.next();
                }
            }
            Mylist.front();
            while (!Mylist.eol())
            {
                if (Mylist.getObject().inShape(xPos, yPos))
                {
                    return true;
                }
                else
                {
                    Mylist.next();
                }
            }
            Mylist.front();
            while (!Mylist.eol())
            {
                Mylist.getObject().Current = false;
                Mylist.next();
            }
            return false;
        }

        private void circleCreate(object sender, MouseEventArgs e)
        {
            if (!inCircle(e.X, e.Y))
            {
                Mylist.push_back(new CCircle(e.X, e.Y));
                Draw();
            }
            else
            {
                Draw();
            }
        }

        private void Paint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Mylist.front();
                while (!Mylist.eol())
                {
                    if (Mylist.getObject().Current == true)
                    {
                        Mylist.erase(Mylist.getCurrent());
                    }
                    else
                        Mylist.next();
                }
                Draw();
            }
        }
    }
}
