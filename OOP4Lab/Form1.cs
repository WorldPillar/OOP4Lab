using System;
using System.IO;
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
        MyLinkedList Mylist;
        //Создаём объект класса graphics для рисования
        private Graphics g;
        //Буфер для bitmap изображения
        private Bitmap bitmapDraw;
        private string action;

        private Model model;
        public PaintBox()
        {
            InitializeComponent();
            //Инициализируем список
            Mylist = new MyLinkedList();
            //Инициализируем объект bitmap, копируем размер drawBox в него
            bitmapDraw = new Bitmap(drawBox.Width, drawBox.Height);
            //Инициализация g
            g = Graphics.FromImage(bitmapDraw);
            drawBox.Image = bitmapDraw;

            createMenu.BackColor = Color.Coral;
            action = "add";

            model = new Model(chooseColor, g);

            if (!File.Exists(@"D:\GitHub\OOP4Lab\shapes.txt"))
                File.CreateText(@"D:\GitHub\OOP4Lab\shapes.txt");
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

        //Обнуление текущего значения у всех объектов
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
            if (action == "add")
                allFalse();

            //проверка, находился ли курсор в круге
            Mylist.back();
            while (!Mylist.eol())
            {
                if (Mylist.getObject().inShape(xPos, yPos))
                {
                    return true;
                }
                else
                    Mylist.prev();
            }

            //Обнуляем значения при создании нового объекта
            if (action == "add")
                allFalse();
            return false;
        }

        private void drawBoxClick(object sender, MouseEventArgs e)
        {
            //если курсор не был в уже созданном круге,
            //создасться новый круг и изображение обновится
            if (e.Button == MouseButtons.Left)
            {
                if (!inShape(e.X, e.Y))
                {
                    if (action == "add")
                    {
                        AddShape(e);
                        Draw();
                    }
                }
                else
                {
                    Draw();
                }
            }
        }

        private void AddShape(MouseEventArgs e)
        {
            //Добавление круга
            if (circleMenu.Checked)
            {
                Mylist.push_back(new CCircle(e.X, e.Y));
            }
            //Добавление квадрата
            else if (squareMenu.Checked)
            {
                Mylist.push_back(new CRectangle(e.X, e.Y));
            }
            //Добавление треугольника
            else if (triangleMenu.Checked)
            {
                Mylist.push_back(new CPolygon(e.X, e.Y, 3));
            }
            //Добавление пятиугольника
            else if (fiveMenu.Checked)
            {
                Mylist.push_back(new CPolygon(e.X, e.Y, 5));
            }
            //Добавление шестиугольника
            else if (sixMenu.Checked)
            {
                Mylist.push_back(new CPolygon(e.X, e.Y, 6));
            }
            //Добавление звезды
            else if (starMenu.Checked)
            {
                Mylist.push_back(new CStar(e.X, e.Y));
            }

            if (!Mylist.isEmpty())
                if (!Mylist.getTail().TryMove(0, 0, g))
                    Mylist.erase(Mylist.back());
        }

        //Событие при нажатии клавиши
        private void Paint_KeyDown(object sender, KeyEventArgs e)
        {
            if (action == "group")
            {
                return;
            }
            //Выбор первого выделенного объекта
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
                Mylist.erase(Mylist.getCurrent());
                //Вырисовывает
                Draw();
            }

            //Вызов Модели для выполнения действий над выбранной фигурой
            if (!Mylist.eol() && Mylist.getObject() != null)
                model.ShapeAct(e.KeyCode, Mylist.getObject());

            Draw();
        }

        //Обновление меню. Снятие выбранности у предыдущего элемента
        private void ToolStripMenueItem_Click(object sender, EventArgs e)
        {
            var thisTsmi = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem tsmi in thisTsmi.GetCurrentParent().Items)
            {
                tsmi.Checked = thisTsmi == tsmi;
            }
        }

        private void ActiveActionChange(object sender, EventArgs e)
        {
            groupMenu.BackColor = Color.MediumTurquoise;
            createMenu.BackColor = Color.MediumTurquoise;
            allFalse();
            Draw();

            if (sender.Equals(groupMenu))
            {
                action = "group";
                groupMenu.BackColor = Color.Coral;

                MakeGroup.Visible = true;
                DeleteGroupMenu.Enabled = false;

                return;
            }
            else if (sender.Equals(createMenu))
            {
                action = "add";
                createMenu.BackColor = Color.Coral;

                DeleteGroupMenu.Enabled = true;
                MakeGroup.Visible = false;
                return;
            }
        }

        private void MakeGroup_Click(object sender, EventArgs e)
        {
            CGroup group = new CGroup();

            Mylist.front();
            while (!Mylist.eol())
            {
                if (Mylist.getObject().Current)
                {
                    group.Add(Mylist.getObject());
                    Mylist.erase(Mylist.getCurrent());
                }
                else
                    Mylist.next();
            }

            if (group.getList().size > 0)
                Mylist.push_back(group);

            ActiveActionChange(createMenu, null);
        }

        private void DeleteGroupMenu_Click(object sender, EventArgs e)
        {
            CGroup group = new CGroup();

            Mylist.front();
            while (!Mylist.eol())
            {
                if (Mylist.getObject().Current)
                {
                    group = Mylist.getObject() as CGroup;
                    if (group != null)
                    {
                        break;
                    }
                    else
                        return;
                }
                else
                    Mylist.next();
            }

            MyLinkedList CopyList = group.getList();

            CopyList.front();
            while (!CopyList.eol())
            {
                Mylist.push_back(CopyList.getObject());

                CopyList.next();
            }
            group = null;
            
            ActiveActionChange(createMenu, null);
        }

        private void SaveMenu_Click(object sender, EventArgs e)
        {
            using (File.Create(@"D:\GitHub\OOP4Lab\shapes.txt"))
            {; }

            string writePath = @"D:\GitHub\OOP4Lab\shapes.txt";

            StreamWriter st = new StreamWriter(writePath, true);

            st.WriteLine(Mylist.size);
            Mylist.front();
            while (!Mylist.eol())
            {
                Mylist.getObject().Save(st);
                Mylist.next();
            }

            st.Close();
            LoadMenu.Enabled = true;
            Mylist.clear();
            Draw();
        }

        private void LoadMenu_Click(object sender, EventArgs e)
        {
            StreamReader file = new StreamReader(@"D:\GitHub\OOP4Lab\shapes.txt");

            Mylist.loadShapes(file);

            Draw();

            file.Close();
            LoadMenu.Enabled = false;
        }
    }

    //Класс Model, который по заданному параметру изменяет фигуру
    class Model
    {
        int sizeChange;
        int move;
        ColorDialog colorChoose;
        Graphics g;
        public Model(ColorDialog color, Graphics g)
        {
            sizeChange = 5;
            move = 5;
            colorChoose = color;
            this.g = g;
        }
        public void ShapeAct(Keys key, AbstractShape shape)
        {
            switch (key)
            {
                //Уменьшает фигуру
                case Keys.OemMinus:
                    {
                        if (shape.TryResize(-sizeChange, g))
                            shape.Resize(-sizeChange);

                        break;
                    }
                //Увеличиваем фигуру
                case Keys.Oemplus:
                    {
                        if (shape.TryResize(sizeChange, g))
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
                //Меняем цвет у фигуры
                case Keys.C:
                    {
                        //Сохраняем цвет фигуры
                        colorChoose.Color = shape.hBrush.BackgroundColor;
                        //Вызываем color dialog
                        colorChoose.ShowDialog();

                        //Задаём кисть с выбранным цветом
                        shape.ColorChange(new HatchBrush(HatchStyle.Cross,
                        Color.PaleVioletRed, colorChoose.Color));
                        break;
                    }
            }
        }
    }

}