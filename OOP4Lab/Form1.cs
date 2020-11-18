using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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

            model = new Model(drawBox.Width, drawBox.Height, chooseColor, g);
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
                Mylist.getObject().Draw(g);
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
            //if (!controlPressed())
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
                    else if (triangleMenu.Checked)
                    {
                        Mylist.push_back(new CPolygon(e.X, e.Y, 3));
                    }
                    else if (fiveMenu.Checked)
                    {
                        Mylist.push_back(new CPolygon(e.X, e.Y, 5));
                    }
                    else if (sixMenu.Checked)
                    {
                        Mylist.push_back(new CPolygon(e.X, e.Y, 6));
                    }

                    if (!Mylist.getTail().TryMove(0, 0, g))
                        Mylist.erase(Mylist.back());

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

    //Класс Model, который по заданному параметру изменяет фигуру
    class Model
    {
        int width;
        int height;
        int sizeChange;
        int move;
        ColorDialog colorChoose;
        Graphics g;
        public Model(int width, int height, ColorDialog color, Graphics g)
        {
            this.width = width;
            this.height = height;
            sizeChange = 5;
            move = 5;
            colorChoose = color;
            this.g = g;
        }
        public void ShapeAct(Keys key, Shape shape)
        {
            if (key == Keys.C)
            {
                //Сохраняем цвет фигуры
                colorChoose.Color = shape.hBrush.BackgroundColor;
                //Вызываем color dialog
                colorChoose.ShowDialog();

                //Задаём кисть с выбранным цветом
                shape.hBrush = new HatchBrush(HatchStyle.Cross,
                Color.PaleVioletRed, colorChoose.Color);

                return;
            }
            //Сохраняем координаты, которые будут сравниваться с
            //рамками drawbox
            int y0 = shape.getCentre().Y - shape.Size;
            int y1 = shape.getCentre().Y + shape.Size;
            int x0 = shape.getCentre().X - shape.Size;
            int x1 = shape.getCentre().X + shape.Size;
            switch (key)
            {
                //Уменьшает фигуру
                case Keys.OemMinus:
                    {
                        if (shape.Size - sizeChange > 0)
                            shape.Resize(-sizeChange);

                        break;
                    }
                //Увеличиваем фигуру
                case Keys.Oemplus:
                    {
                        if (shape.TryMove(sizeChange, sizeChange, g))
                            shape.Resize(sizeChange);

                        break;
                    }
                //Двигаем фигуру вниз
                case Keys.Down:
                    {
                        if(shape.TryMove(0, move, g))
                            shape.Move(0, move);

                        break;
                    }
                //Двигаем фигуру вверх
                case Keys.Up:
                    {
                        if (shape.TryMove(0, -move, g))
                            shape.Move(0, -move);

                        break;
                    }
                //Двигаем фигуру влево
                case Keys.Left:
                    {
                        if (shape.TryMove(-move, 0, g))
                            shape.Move(-move, 0);

                        break;
                    }
                //Двигаем фигуру вправо
                case Keys.Right:
                    {
                        if (shape.TryMove(move, 0, g))
                            shape.Move(move, 0);

                        break;
                    }
            }
        }
    }

}
