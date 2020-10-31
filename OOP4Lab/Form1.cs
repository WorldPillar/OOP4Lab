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
        public PaintBox()
        {
            InitializeComponent();
            Mylist = new LinkedList();
        }

        public bool inCircle(int xPos, int yPos)
        {
            Mylist.front();
            while (!Mylist.eol())
            {
                if (Mylist.getObject().inShape(xPos, yPos))
                    return true;
                else
                    Mylist.next();
            }
            return false;
        }

        private void circleCreate(object sender, MouseEventArgs e)
        {
            if (!inCircle(e.X, e.Y))
            {
                Mylist.push_front(new CCircle(e.X, e.Y));
                Mylist.getObject().FillDraw(drawBox.Handle);
            }
            else
            {

            }
        }
    }
}
