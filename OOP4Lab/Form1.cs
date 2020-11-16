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

        private Model model;
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
            drawBox.Image = bitmapDraw;

            model = new Model(drawBox.Width, drawBox.Height);
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

        public bool inShape(int xPos, int yPos)
        {
            //Если control не зажат,
            //объекты перестанут быть текущими
            if (!controlPressed())
                allFalse();

            //проверка, находился ли курсор в круге
            Mylist.back();
            while (!Mylist.eol())
            {
                if (Mylist.getObject().inShape(xPos, yPos))
                    return true;
                else
                    Mylist.prev();
            }

            //Обнуляем значения при создании нового объекта
            allFalse();
            return false;
        }

        private void ShapeCreate(object sender, MouseEventArgs e)
        {
            //если курсор не был в уже созданном круге,
            //создасться новый круг и изображение обновится
            if (e.Button == MouseButtons.Left)
            {
                if (!inShape(e.X, e.Y))
                {
                    if (circleMenu.Checked)
                    {
                        Mylist.push_back(new CCircle(e.X, e.Y));
                    }
                    else if (squareMenu.Checked)
                    {
                        Mylist.push_back(new CRectangle(e.X, e.Y));
                    }
                    Draw();
                }
                else
                {
                    Draw();
                }
            }
        }

        //Событие при нажатии клавиши
        private void Paint_KeyDown(object sender, KeyEventArgs e)
        {
            Mylist.front();
            while (!Mylist.eol())
            {
                if (Mylist.getObject().Current)
                    break;
                else
                    Mylist.next();
            };

            //Проверяет, нажата ли клавиша Delete
            if (e.KeyCode == Keys.Delete)
            {
                //Удаляет все объекты со значение current = true
                Mylist.front();
                while (!Mylist.eol())
                {
                    if (Mylist.getObject().Current)
                    {
                        Mylist.erase(Mylist.getCurrent());
                    }
                    else
                        Mylist.next();
                }
                //Вырисовывает
                Draw();
            }

            if (!Mylist.eol() && Mylist.getObject() != null)
                model.ShapeAct(e.KeyCode, Mylist.getObject());

            Draw();
        }
    }

    class Model
    {
        int width;
        int height;
        public Model(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        public void ShapeAct(Keys key, Shape shape)
        {
            switch (key)
            {
                case Keys.OemMinus:
                    {
                        if (shape.minSize <= shape.Size - 5)
                            shape.Resize(-5);
                        break;
                    }
                case Keys.Oemplus:
                    {
                        int y0 = shape.getCentre().Y - shape.Size - 5;
                        int y1 = shape.getCentre().Y + shape.Size + 5;
                        int x0 = shape.getCentre().X - shape.Size - 5;
                        int x1 = shape.getCentre().X + shape.Size + 5;
                        if (x0 < 0 || y0 < 0 || x1 > width || y1 > height)
                            return;
                        shape.Resize(5);
                        break;
                    }
                case Keys.Down:
                    {
                        int y = shape.getCentre().Y + shape.Size;
                        if (y + 5 >= height)
                        {
                            return;
                        }

                        shape.Move(0, 5);
                        break;
                    }
                case Keys.Up:
                    {
                        int y = shape.getCentre().Y - shape.Size;
                        if (y - 5 < 0)
                            return;
                        shape.Move(0, -5);
                        break;
                    }
                case Keys.Left:
                    {
                        int x = shape.getCentre().X - shape.Size;
                        if (x - 5 < 0)
                            return;
                        shape.Move(-5, 0);
                        break;
                    }
                case Keys.Right:
                    {
                        int x = shape.getCentre().X + shape.Size;
                        if (x + 5 > width)
                        {
                            return;
                        }

                        shape.Move(5, 0);
                        break;
                    }
            }
        }

    }

}
