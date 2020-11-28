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
        private string shapeCreate;

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
            shapeCreate = "circleMenu";

            model = new Model(chooseColor, g);

            if (!File.Exists(@"D:\GitHub\OOP4Lab\shapes.txt"))
                File.CreateText(@"D:\GitHub\OOP4Lab\shapes.txt");
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
            switch (shapeCreate)
            {
                //Добавление круга
                case "circleMenu":
                    Mylist.push_back(new CCircle(e.X, e.Y));
                    break;
                //Добавление треугольника
                case "triangleMenu":
                    Mylist.push_back(new CPolygon(e.X, e.Y, 3));
                    break;
                //Добавление квадрата
                case "squareMenu":
                    Mylist.push_back(new CRectangle(e.X, e.Y));
                    break;
                //Добавление пятиугольника
                case "fiveMenu":
                    Mylist.push_back(new CPolygon(e.X, e.Y, 5));
                    break;
                //Добавление шестиугольника
                case "sixMenu":
                    Mylist.push_back(new CPolygon(e.X, e.Y, 6));
                    break;
                //Добавление звезды
                case "starMenu":
                    Mylist.push_back(new CStar(e.X, e.Y));
                    break;
            }

            if (!Mylist.isEmpty())
                if (!Mylist.getTail().Move(0, 0, g))
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
            model.ShapeAct(e.KeyCode, Mylist.getObject());

            Draw();
        }

        //Обновление меню. Снятие выбранности у предыдущего элемента
        private void ToolStripMenueItem_Click(object sender, EventArgs e)
        {
            var thisTsmi = (ToolStripMenuItem)sender;
            shapeCreate = thisTsmi.Name;
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

        //Создаётся группа
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

        //Удаляется группа
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

        //Сохраняются фигуры в файл
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

        //Фигуры выгружаются в хранилище из файла
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
        Stack<Command> history;
        Dictionary<Keys, Command> commands;

        public Model(ColorDialog color, Graphics g)
        {
            sizeChange = 5;
            move = 5;
            colorChoose = color;
            this.g = g;

            history = new Stack<Command>();
            commands = new Dictionary<Keys, Command>();

            //Уменьшает фигуру
            commands[Keys.OemMinus] = new ReSizeCommand(-sizeChange, g);
            //Увеличиваем фигуру
            commands[Keys.Oemplus] = new ReSizeCommand(sizeChange, g);
            //Сдвиг фигуры вниз
            commands[Keys.Down] = new MoveCommand(0, move, g);
            //Сдвиг фигуры вверх
            commands[Keys.Up] = new MoveCommand(0, -move, g);
            //Сдвиг фигуры влево
            commands[Keys.Left] = new MoveCommand(-move, 0, g);
            //Сдвиг фигуры вправо
            commands[Keys.Right] = new MoveCommand(move, 0, g);
            //Меняем цвет фигуры
            commands[Keys.C] = new ColorCommand(colorChoose);
        }
        public void ShapeAct(Keys key, AbstractShape selection)
        {
            switch (key)
            {
                case Keys.Back:
                    {
                        if (!history.Any())
                            return;

                        Command lastcommand = history.Pop();

                        lastcommand.undo();

                        break;
                    }
            }

            if (selection != null && commands.ContainsKey(key))
            {
                Command command = commands[key];

                Command newcommand = command.clone();

                newcommand.execute(selection);

                history.Push(newcommand);
            }
        }
    }

    abstract class Command
    {
        abstract public void execute(AbstractShape sel);

        abstract public void undo();

        abstract public Command clone();
    }

    class MoveCommand : Command
    {
        AbstractShape selection;

        int dx, dy;
        Graphics g;

        public MoveCommand(int dx, int dy, Graphics g)
        {
            this.dx = dx;
            this.dy = dy;
            this.g = g;
        }

        public override void execute(AbstractShape sel)
        {
            selection = sel;

            if (selection != null)
            {
                selection.Move(dx, dy, g);
            }
        }

        public override void undo()
        {
            if (selection != null)
            {
                selection.Move(-dx, -dy, g);
            }
        }

        public override Command clone()
        {
            return new MoveCommand(dx, dy, g);
        }
    }

    class ReSizeCommand : Command
    {
        AbstractShape selection;

        int size;
        Graphics g;

        public ReSizeCommand(int size, Graphics g)
        {
            this.size = size;
            this.g = g;
        }

        public override void execute(AbstractShape sel)
        {
            selection = sel;

            if (selection != null)
            {
                selection.Resize(size, g);
            }
        }

        public override void undo()
        {
            if (selection != null)
            {
                selection.Resize(-size, g);
            }
        }

        public override Command clone()
        {
            return new ReSizeCommand(size, g);
        }
    }

    class ColorCommand : Command
    {
        AbstractShape selection;

        ColorDialog colorChoose;
        Color backcolor;

        public ColorCommand(ColorDialog colorChoose)
        {
            this.colorChoose = colorChoose;
        }

        public override void execute(AbstractShape sel)
        {
            selection = sel;
            if (selection != null)
            {
                backcolor = selection.hBrush.BackgroundColor;

                //Сохраняем цвет фигуры
                colorChoose.Color = backcolor;
                //Вызываем color dialog
                colorChoose.ShowDialog();
                //Задаём кисть с выбранным цветом
                selection.ColorChange(new HatchBrush(HatchStyle.Cross,
                            Color.PaleVioletRed, colorChoose.Color));
            }
        }

        public override void undo()
        {
            selection.ColorChange(new HatchBrush(HatchStyle.Cross,
                        Color.PaleVioletRed, backcolor)); ;
        }

        public override Command clone()
        {
            return new ColorCommand(colorChoose);
        }
    }
}