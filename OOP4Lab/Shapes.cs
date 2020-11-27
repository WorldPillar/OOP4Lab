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
    class CCircle : AbstractShape
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
