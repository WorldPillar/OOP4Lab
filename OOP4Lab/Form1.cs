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
        //Создаём список
        LinkedList Mylist;
        //Создаём объект класса graphics для рисования
        private Graphics g;
        //Буфер для bitmap изображения
        private Bitmap bitmapDraw;
        public PaintBox()
        {
            InitializeComponent();
            //Инициализируем список
            Mylist = new LinkedList();
            //Инициализируем объект bitmap, копируем размер drawBox в него
            bitmapDraw = new Bitmap(drawBox.Width, drawBox.Height);
            //Инициализация g
            g = Graphics.FromImage(bitmapDraw);
            g.Clear(Color.White);
        }

        public bool controlPressed()
        {
            //Зажат ли control
            return (Control.ModifierKeys & Keys.Control) == Keys.Control;
        }

        public void Draw()
        {
            //Очищает 
            g.Clear(Color.White);
            //Рисуем все круги
            Mylist.front();
            while (!Mylist.eol())
            {
                //Если данный объект выделен, то он будет закрашен
                if (Mylist.getObject().Current == true)
                    Mylist.getObject().FillDraw(bitmapDraw);
                //иначе не будет закрашен
                else
                    Mylist.getObject().Draw(bitmapDraw);
                Mylist.next();
            }
            //Копируем изображение из буфера в drawBox
            drawBox.Image = bitmapDraw;
        }

        private void allFalse()
        {
            Mylist.front();
            while (!Mylist.eol())
            {
                Mylist.getObject().Current = false;
                Mylist.next();
            }
        }

        public bool inCircle(int xPos, int yPos)
        {
            //Если control не зажат, объекты перестанут быть текущими
            if (!controlPressed())
                allFalse();

            //проверка, находился ли курсор при нажатии в круге
            Mylist.front();
            while (!Mylist.eol())
            {
                if (Mylist.getObject().inShape(xPos, yPos))
                    return true;
                else
                    Mylist.next();
            }

            //Если control был зажат, но объекты не были выделены, то
            //создасться новый объект, а остальные перестанут быть выделеными
            allFalse();
            return false;
        }

        private void circleCreate(object sender, MouseEventArgs e)
        {
            //если курсор не был в уже созданном круге, создасться уже новый круг
            //И изображение обновится
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

        //Событие при нажатии клавиши
        private void Paint_KeyDown(object sender, KeyEventArgs e)
        {
            //Проверяет, нажата ли клавиша Delete
            if (e.KeyCode == Keys.Delete)
            {
                //Удаляет все объекты со значение current = true
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
                //Вырисовывает
                Draw();
            }
        }
    }
}
