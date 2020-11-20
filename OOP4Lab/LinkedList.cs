using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP4Lab
{
    class LinkedList
    {
        //Начало списка
        Node root;
        //Конец списка
        Node tail;
        //Текущий элемент
        Node current;
        //Счётчик элементов
        private int count = 0;

        //Конструктор по умолчанию
        public LinkedList()
        {
            root = null;
            tail = root;
            current = root;
        }
        //Получение размера списка
        public int size
        {
            get => count;
        }
        //Пустой ли список
        public bool isEmpty() { return root == null; }
        //Является ли текущий элемент концом списка
        public bool eol() { return current == null; }
        //Устанавливает текущий элемент в начало списка
        public Node front() { return current = root; }
        //Устанавливает текущий элемент в конец списка
        public Node back() { return current = tail; }
        //Устанавливает текущее значение на следующее
        public void next()
        {
            if (current.nextNode != null)
                current = current.nextNode;
            else
                current = null;
        }
        //Устанавливает текущее значение на предыдущее
        public void prev()
        {
            if (current.prevNode != null)
                current = current.prevNode;
            else
                current = null;
        }
        //Добавляет объект в начало списка
        public void push_front(Shape newObj)
        {
            ++count;
            if (isEmpty())
            {
                root = new Node(newObj);
                tail = root;
            }
            else
            {
                Node newNode = new Node(newObj);
                newNode.nextNode = root;
                root.prevNode = newNode;
                root = newNode;
            }
            current = root;
        }
        //Добавляет объект в конец списка
        public void push_back(Shape newObj)
        {
            ++count;
            if (isEmpty())
            {
                root = new Node(newObj);
                tail = root;
            }
            else
            {
                Node newNode = new Node(newObj);
                newNode.prevNode = tail;
                tail.nextNode = newNode;
                tail = newNode;
            }
        }
        //Вставляет объект после it
        public void insert(Node it, Shape newObj)
        {
            ++count;
            Node newNode = new Node(newObj);
            Node afterIt = it.nextNode;
            newNode.prevNode = it;
            newNode.nextNode = afterIt;
            it.nextNode = newNode;
            if (afterIt != null)
                afterIt.prevNode = newNode;
            else
                tail = newNode;
        }
        //Удаляет объект it
        public void erase(Node it)
        {
            --count;
            Node prevIt = it.prevNode;
            Node nextIt = it.nextNode;
            if (prevIt != null)
            {
                prevIt.nextNode = nextIt;
            }
            else
            {
                root = nextIt;
            }
            if (nextIt != null)
            {
                nextIt.prevNode = prevIt;
                current = nextIt;
            }
            else
            {
                tail = prevIt;
                current = tail;
            }
            it = null;
        }
        //Полностью очищает список
        public void clear()
        {
            if (root == null)
                return;
            Node delRoot = root;
            do
            {
                Node delThis = delRoot;
                delRoot = delRoot.nextNode;
                delThis = null;
            } while (delRoot != null);
            count = 0;
        }
        public Shape getRoot()
        {
            if (root == null)
                return null;
            return root.getObj;
        }
        public Shape getTail()
        {
            if (tail == null)
                return null;
            return tail.getObj;
        }
        //Возвращает текущий объект, хранящийся в Node
        public Shape getObject()
        {
            if (current == null)
                return null;
            return current.getObj;
        }
        //Возвращает текущий объект Node
        public Node getCurrent()
        {
            return current;
        }
    }

    //Узел для хранения объектов в списке
    class Node
    {
        //Объект Shape для доступа к методам
        private Shape obj;
        private Node next;
        private Node prev;
        public Node(Shape obj)
        {
            next = null;
            prev = null;
            this.obj = obj;
        }
        public Node nextNode
        {
            get => next;
            set => next = value;
        }
        public Node prevNode
        {
            get => prev;
            set => prev = value;
        }
        public Shape getObj
        {
            get => obj;
            set => obj = value;
        }
        ~Node()
        {
            nextNode = null;
            prevNode = null;
            obj = null;
        }
    }

    //Абстрактный класс для хранения различных объектов
    abstract class Shape
    {
        protected HatchBrush brush;

        //геттеры и сеттеры для кисти
        public abstract HatchBrush hBrush { set; get; }
        //Получение размера
        public abstract int Size { get; }
        //Получение центра
        public abstract Point getCentre();
        public abstract bool Current { get; set; }
        //Смещение фигуры на заданные координаты
        public abstract void Move(int x, int y);
        //Изменение фигуры на заданный размер
        public abstract void Resize(int size);
        //Попытка двигать фигуру в форме
        public abstract bool TryMove(int dx, int dy, Graphics g);
        //Попытка изменить размер фигуры в форме
        public abstract bool TryResize(int d, Graphics g);
        //Проверка нахождения курсора при нажатии в форме
        public abstract bool inShape(int x, int y);
        //Отрисовка фигуры
        public abstract void Draw(Graphics g);
    }
    class CCircle : Shape
    {
        protected int x;
        protected int y;
        protected int r = 30;
        protected bool current;
        protected GraphicsPath graph;
        protected CCircle()
        {
            graph = new GraphicsPath();
            x = 0;
            y = 0;
            r = 0;
            current = false;
            brush = new HatchBrush(
                HatchStyle.Cross,
                Color.PaleVioletRed,
                Color.Black);
        }
        public CCircle(int x, int y)
        {
            graph = new GraphicsPath();
            this.x = x;
            this.y = y;
            current = true;
            brush = new HatchBrush(
                HatchStyle.Cross,
                Color.PaleVioletRed,
                Color.Black);
        }

        public override void Move(int dx, int dy)
        {
            x += dx;
            y += dy;
            Update();
        }
        public override bool inShape(int x, int y)
        {
            if (graph.IsVisible(x, y))
            {
                current = true;
                return true;
            }
            return false;
        }
        public override void Draw(Graphics g)
        {
            Update();
            if (current)
                g.FillPath(brush, graph);
            else
                g.FillPath(new SolidBrush(brush.BackgroundColor), graph);
        }
        public override void Resize(int size)
        {
            r += size;
            Update();
        }
        //Обновление фигуры в graphicpath
        protected virtual void Update()
        {
            graph.Reset();
            graph.AddEllipse(this.x - r, this.y - r, r * 2, r * 2);
        }
        public override bool TryMove(int dx, int dy, Graphics g)
        {
            PointF[] region = new PointF[4];
            region[0] = new PointF(x + dx, y - r + dy);
            region[1] = new PointF(x - r + dx, y + dy);
            region[2] = new PointF(x + dx, y + r + dy);
            region[3] = new PointF(x + r + dx, y + dy);
            foreach (var it in region)
                if (it.X < 0 || it.Y < 0
                    || it.X > g.VisibleClipBounds.Width
                    || it.Y > g.VisibleClipBounds.Height)
                    return false;
            return true;
        }
        public override bool TryResize(int d, Graphics g)
        {
            if (r + d <= 0)
                return false;
            else
            {
                int y0 = y - r - d;
                int y1 = y + r + d;
                int x0 = x - r - d;
                int x1 = x + r + d;
                if (x0 < 0 || y0 < 0 || x1 > g.VisibleClipBounds.Width
                    || y1 > g.VisibleClipBounds.Height)
                    return false;
            }
            return true;
        }
        public override int Size
        {
            get => r;
        }
        public override Point getCentre()
        {
            return new Point(x, y);
        }
        public override HatchBrush hBrush
        {
            set => brush = value;
            get => brush;
        }
        public override bool Current
        {
            get => current;
            set => current = value;
        }
        ~CCircle()
        {
            x = 0;
            y = 0;
            r = 0;
        }
    }

    class CRectangle : Shape
    {
        private int x;
        private int y;
        private int hWidth = 30;
        private bool current;
        private CRectangle()
        {
            x = 0;
            y = 0;
            hWidth = 0;
            current = false;
            brush = new HatchBrush(
                HatchStyle.Cross,
                Color.White,
                Color.White);
        }
        public CRectangle(int x, int y)
        {
            this.x = x;
            this.y = y;
            current = true;
            brush = new HatchBrush(
                HatchStyle.Cross,
                Color.PaleVioletRed,
                Color.Black);
        }

        public override void Move(int dx, int dy)
        {
            x += dx;
            y += dy;
        }
        public override bool inShape(int x, int y)
        {
            if (Math.Abs(this.x - x) <= hWidth
                && Math.Abs(this.y - y) <= hWidth)
            {
                current = true;
                return true;
            }
            return false;
        }

        public override void Draw(Graphics g)
        {
            if (current)
                g.FillRectangle(brush, x - hWidth,
                    y - hWidth, hWidth * 2, hWidth * 2);
            else
                g.FillRectangle(new SolidBrush(brush.BackgroundColor),
                    x - hWidth, y - hWidth, hWidth * 2, hWidth * 2);
        }

        public override void Resize(int size)
        {
            hWidth += size;
        }

        public override bool TryMove(int dx, int dy, Graphics g)
        {
            //Попытка двигать прямоугольник относительно 4-ёх его точек
            PointF[] region = new PointF[4];
            region[0] = new PointF(x + dx, y + hWidth + dy);
            region[1] = new PointF(x - hWidth + dx, y + dy);
            region[2] = new PointF(x + dx, y - hWidth + dy);
            region[3] = new PointF(x + hWidth + dx, y + dy);
            foreach (var it in region)
                if (it.X < 0 || it.Y < 0
                    || it.X > g.VisibleClipBounds.Width
                    || it.Y > g.VisibleClipBounds.Height)
                    return false;
            return true;
        }

        public override bool TryResize(int d, Graphics g)
        {
            //Если фигура меньше нуля, изменение размера запрещается
            if (hWidth + d <= 0)
                return false;
            else
            {
                //Если точка фигуры выходит на поле, изменение запрещается
                int y0 = y - hWidth - d;
                int y1 = y + hWidth + d;
                int x0 = x - hWidth - d;
                int x1 = x + hWidth + d;
                if (x0 < 0 || y0 < 0 || x1 > g.VisibleClipBounds.Width
                    || y1 > g.VisibleClipBounds.Height)
                    return false;
            }
            return true;
        }

        public override int Size
        {
            get => hWidth;
        }
        public override bool Current
        {
            get => current;
            set => current = value;
        }
        public override Point getCentre()
        {
            return new Point(x, y);
        }
        public override HatchBrush hBrush
        {
            set => brush = value;
            get => brush;
        }
        ~CRectangle()
        {
            x = 0;
            y = 0;
            hWidth = 0;
        }
    }

    class CPolygon : CCircle
    {
        //Вершины многоугольника
        protected PointF[] points;

        public CPolygon(int x, int y, int amount) : base(x, y)
        {
            points = new PointF[amount];

            CreatPolygon();
        }

        //создание многоугольника
        protected virtual void CreatPolygon()
        {
            //Угол для вершины
            double angle = Math.PI * 2 / points.Length;

            //Добавление вершины в круге
            for (int i = 0; i < points.Length; ++i)
            {
                points[i] = new PointF((float)Math.Cos((-Math.PI / 2) + angle * i) * r + x,
                    (float)Math.Sin((-Math.PI / 2) + angle * i) * r + y);
            }
        }

        public override void Move(int dx, int dy)
        {
            x += dx;
            y += dy;

            for (int i = 0; i < points.Length; ++i)
            {
                points[i].X += dx;
                points[i].Y += dy;
            }
            Update();
        }

        public override void Resize(int size)
        {
            r += size;

            //Каждая точка перемещается относительно своего угла.
            double angle = Math.PI * 2 / points.Length;

            for (int i = 0; i < points.Length; ++i)
            {
                points[i].X = (float)Math.Cos((-Math.PI / 2) + angle * i) * r + x;
                points[i].Y = (float)Math.Sin((-Math.PI / 2) + angle * i) * r + y;
            }
            Update();
        }

        protected override void Update()
        {
            graph.Reset();
            graph.AddPolygon(points);
        }

        public override bool TryMove(int dx, int dy, Graphics g)
        {
            //Проверка выхода каждой точки из поля
            foreach (var it in points)
            {
                if (it.X + dx < 0 || it.Y + dy < 0
                    || it.X + dx > g.VisibleClipBounds.Width
                    || it.Y + dy > g.VisibleClipBounds.Height)
                    return false;
            }
            return true;
        }

        public override bool TryResize(int d, Graphics g)
        {
            if (r + d <= 0)
                return false;
            else
            {
                foreach (var it in points)
                {
                    //Если точка фигуры выходит на поле, изменение запрещается
                    float y0 = it.Y - d;
                    float y1 = it.Y + d;
                    float x0 = it.X - d;
                    float x1 = it.X + d;
                    if (x0 < 0 || y0 < 0 || x1 > g.VisibleClipBounds.Width
                        || y1 > g.VisibleClipBounds.Height)
                        return false;
                }
            }
            return true;
        }
    }

    class CStar : CPolygon
    {
        //Создание звезды
        public CStar(int x, int y) : base(x, y, 10)
        {
            graph = new GraphicsPath();
        }

        //Переопределение создания многоугольника
        protected override void CreatPolygon()
        {
            double angle = Math.PI * 2 / 10;

            for (int i = 0; i < 10; ++i)
            {
                //Если чётная вершина, то она лежит на окружности
                if (i % 2 == 0)
                    points[i] = new PointF((float)Math.Cos((-Math.PI / 2) + angle * i) * r + x,
                    (float)Math.Sin((-Math.PI / 2) + angle * i) * r + y);
                //Иначе лежит в окружности
                else
                    points[i] = new PointF((float)Math.Cos((-Math.PI / 2) + angle * i) * (r / 2) + x,
                    (float)Math.Sin((-Math.PI / 2) + angle * i) * (r / 2) + y);
            }
        }

        public override void Resize(int size)
        {
            r += size;

            double angle = Math.PI * 2 / points.Length;

            for (int i = 0; i < points.Length; ++i)
            {
                //Если чётная вершина, то смещаем относительно радиуса
                //Иначе смещаем относительно половины радиуса
                float rad = r;
                if (i % 2 == 0)
                    rad = r;
                else rad = r / 2;

                points[i].X = (float)Math.Cos((-Math.PI / 2) + angle * i) * rad + x;
                points[i].Y = (float)Math.Sin((-Math.PI / 2) + angle * i) * rad + y;
            }
            Update();
        }
    }
}
