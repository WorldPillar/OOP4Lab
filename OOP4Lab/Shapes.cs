using System;
using System.IO;
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
    class CCircle : AbstractShape
    {
        protected int x;
        protected int y;
        protected int r = 30;
        protected bool current;
        protected GraphicsPath graph;
        public CCircle()
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
        public override bool Move(int dx, int dy, Graphics g)
        {
            if (!TryMove(dx, dy, g))
                return false;

            x += dx;
            y += dy;
            Update();

            return true;
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
        public override bool Resize(int size, Graphics g)
        {
            if (!TryResize(size, g))
                return false;

            r += size;
            Update();

            return true;
        }
        //Обновление фигуры в graphicpath
        protected virtual void Update()
        {
            graph.Reset();
            graph.AddEllipse(this.x - r, this.y - r, r * 2, r * 2);
        }
        public override void ColorChange(HatchBrush hatch)
        {
            brush = hatch;
        }
        public override HatchBrush hBrush
        {
            get => brush;
        }
        public override bool Current
        {
            get => current;
            set => current = value;
        }

        public override void Save(StreamWriter sw)
        {
            sw.WriteLine("Circle");
            sw.WriteLine(x + " " + y + " " + r + " " + brush.BackgroundColor.ToArgb());
        }

        public override void Load(StreamReader sw)
        {
            string line;

            line = sw.ReadLine();

            int[] a = line.Split(' ').Select(int.Parse).ToArray();

            x = a[0]; y = a[1]; r = a[2];

            brush = new HatchBrush(
                HatchStyle.Cross,
                Color.PaleVioletRed,
                Color.FromArgb(a[3]));
        }

        ~CCircle()
        {
            x = 0;
            y = 0;
            r = 0;
        }
    }

    class CRectangle : AbstractShape
    {
        private int x;
        private int y;
        private int hWidth = 30;
        private bool current;
        public CRectangle()
        {
            x = 0;
            y = 0;
            hWidth = 0;
            current = false;
            brush = new HatchBrush(
                HatchStyle.Cross,
                Color.PaleVioletRed,
                Color.Black);
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
        public override bool Move(int dx, int dy, Graphics g)
        {
            if (!TryMove(dx, dy, g))
                return false;

            x += dx;
            y += dy;

            return true;
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
        public override bool Resize(int size, Graphics g)
        {
            if (!TryResize(size, g))
                return false;

            hWidth += size;

            return true;
        }
        public override bool Current
        {
            get => current;
            set => current = value;
        }
        public override void ColorChange(HatchBrush hatch)
        {
            brush = hatch;
        }
        public override HatchBrush hBrush
        {
            get => brush;
        }

        public override void Save(StreamWriter sw)
        {
            sw.WriteLine("Rectangle");
            sw.WriteLine(x + " " + y + " " + hWidth + " " + brush.BackgroundColor.ToArgb());
        }

        public override void Load(StreamReader sw)
        {
            string line;

            line = sw.ReadLine();

            int[] a = line.Split(' ').Select(int.Parse).ToArray();

            x = a[0]; y = a[1]; hWidth = a[2];

            brush = new HatchBrush(
                HatchStyle.Cross,
                Color.PaleVioletRed,
                Color.FromArgb(a[3]));
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

        public CPolygon() : base()
        {
            points = new PointF[0];
        }

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

        public override bool Move(int dx, int dy, Graphics g)
        {
            if (!TryMove(dx, dy, g))
                return false;

            x += dx;
            y += dy;

            for (int i = 0; i < points.Length; ++i)
            {
                points[i].X += dx;
                points[i].Y += dy;
            }
            Update();

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

        public override bool Resize(int size, Graphics g)
        {
            if (!TryResize(size, g))
                return false;

            r += size;

            //Каждая точка перемещается относительно своего угла.
            double angle = Math.PI * 2 / points.Length;

            for (int i = 0; i < points.Length; ++i)
            {
                points[i].X = (float)Math.Cos((-Math.PI / 2) + angle * i) * r + x;
                points[i].Y = (float)Math.Sin((-Math.PI / 2) + angle * i) * r + y;
            }
            Update();

            return true;
        }

        protected override void Update()
        {
            graph.Reset();
            graph.AddPolygon(points);
        }

        public override void Save(StreamWriter sw)
        {
            sw.WriteLine("Polygon");
            sw.WriteLine(x + " " + y + " " + r + " " + points.Length
                + " " + brush.BackgroundColor.ToArgb());
        }

        public override void Load(StreamReader sw)
        {
            string line;

            line = sw.ReadLine();

            int[] a = line.Split(' ').Select(int.Parse).ToArray();

            x = a[0]; y = a[1]; r = a[2]; points = new PointF[a[3]];

            brush = new HatchBrush(
                HatchStyle.Cross,
                Color.PaleVioletRed,
                Color.FromArgb(a[4]));

            CreatPolygon();
        }
    }

    class CStar : CPolygon
    {
        //Создание звезды
        public CStar() : base()
        {
            points = new PointF[10];
        }

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
                    points[i] = new PointF((float)Math.Cos((-Math.PI / 2) + angle * i) * (float)(r / 2.5) + x,
                    (float)Math.Sin((-Math.PI / 2) + angle * i) * (float)(r / 2.5) + y);
            }
        }

        public override bool Resize(int size, Graphics g)
        {
            if (!TryResize(size, g))
                return false;

            r += size;

            double angle = Math.PI * 2 / points.Length;

            for (int i = 0; i < points.Length; ++i)
            {
                //Если чётная вершина, то смещаем относительно радиуса
                //Иначе смещаем относительно половины радиуса
                float rad = r;
                if (i % 2 == 0)
                    rad = r;
                else rad = (float)(r / 2.5);

                points[i].X = (float)Math.Cos((-Math.PI / 2) + angle * i) * rad + x;
                points[i].Y = (float)Math.Sin((-Math.PI / 2) + angle * i) * rad + y;
            }
            Update();

            return true;
        }

        public override void Save(StreamWriter sw)
        {
            sw.WriteLine("Star");
            sw.WriteLine(x + " " + y + " " + r + " " + brush.BackgroundColor.ToArgb());
        }

        public override void Load(StreamReader sw)
        {
            string line;

            line = sw.ReadLine();

            int[] a = line.Split(' ').Select(int.Parse).ToArray();

            x = a[0]; y = a[1]; r = a[2];

            brush = new HatchBrush(
                HatchStyle.Cross,
                Color.PaleVioletRed,
                Color.FromArgb(a[3]));

            CreatPolygon();
        }
    }

    class CGroup : AbstractShape
    {
        MyLinkedList shapes;
        protected bool current;

        public CGroup()
        {
            shapes = new MyLinkedList();
            current = true;

            brush = new HatchBrush(
                HatchStyle.Cross,
                Color.PaleVioletRed,
                Color.Black);
        }

        public void Add(AbstractShape newLeaf)
        {
            shapes.push_back(newLeaf);
        }

        public MyLinkedList getList()
        {
            return shapes;
        }

        public override bool inShape(int x, int y)
        {
            shapes.front();
            while (!shapes.eol())
            {
                if (shapes.getObject().inShape(x, y))
                {
                    shapes.front();
                    while (!shapes.eol())
                    {
                        shapes.getObject().Current = true;

                        shapes.next();
                    }
                    current = true;
                    return true;
                }
                shapes.next();
            }

            return false;
        }
        public override bool TryMove(int dx, int dy, Graphics g)
        {
            shapes.front();
            while (!shapes.eol())
            {
                if (shapes.getObject().TryMove(dx, dy, g) == false)
                    return false;

                shapes.next();
            }

            return true;
        }
        public override bool Move(int dx, int dy, Graphics g)
        {
            if (!TryMove(dx, dy, g))
                return false;

            shapes.front();
            while (!shapes.eol())
            {
                shapes.getObject().Move(dx, dy, g);

                shapes.next();
            }

            return true;
        }
        public override bool TryResize(int d, Graphics g)
        {
            shapes.front();
            while (!shapes.eol())
            {
                if (shapes.getObject().TryResize(d, g) == false)
                    return false;

                shapes.next();
            }

            return true;
        }
        public override bool Resize(int size, Graphics g)
        {
            if (!TryResize(size, g))
                return false;

            shapes.front();
            while (!shapes.eol())
            {
                shapes.getObject().Resize(size, g);

                shapes.next();
            }

            return true;
        }
        public override void Draw(Graphics g)
        {
            shapes.front();
            while (!shapes.eol())
            {
                shapes.getObject().Draw(g);

                shapes.next();
            }
        }

        public override void ColorChange(HatchBrush hatch)
        {
            brush = hatch;

            shapes.front();
            while (!shapes.eol())
            {
                shapes.getObject().ColorChange(brush);

                shapes.next();
            }
        }

        public override bool Current
        {
            get => current;
            set
            {
                current = value;

                shapes.front();
                while (!shapes.eol())
                {
                    shapes.getObject().Current = current;

                    shapes.next();
                }
            }
        }

        public override void Save(StreamWriter sw)
        {
            sw.WriteLine("Group");
            sw.WriteLine(shapes.size);

            shapes.front();
            while (!shapes.eol())
            {
                shapes.getObject().Save(sw);

                shapes.next();
            }
        }

        public override void Load(StreamReader sw)
        {
            shapes.loadShapes(sw);
        }

        public override HatchBrush hBrush
        {
            get => brush;
        }

        ~CGroup()
        {
            shapes.clear();
        }
    }
}
