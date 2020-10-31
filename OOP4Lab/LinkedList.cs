using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
        public void front() { current = root; }
        //Устанавливает текущий элемент в конец списка
        public void back() { current = tail; }
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
        //Возвращает текущий объект, хранящийся в Node
        public Shape getObject()
        {
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
        public abstract bool Current { get; set; }
        public abstract void Move(int x, int y);
        public abstract bool inShape(int x, int y);
        public abstract void Draw(Bitmap bitmapDraw);
        public abstract void FillDraw(Bitmap bitmapDraw);
    }
    class CCircle : Shape
    {
        private int x;
        private int y;
        private int r = 30;
        private bool current;
        public CCircle()
        {
            x = 0;
            y = 0;
            r = 0;
            current = false;
        }
        public CCircle(int x, int y)
        {
            this.x = x;
            this.y = y;
            current = true;
        }

        public override void Move(int dx, int dy)
        {
            x += dx;
            y += dy;
        }
        public override bool inShape(int x, int y)
        {
            if (Math.Abs(this.x - x) <= r && Math.Abs(this.y - y) <= r)
            {
                current = true;
                return true;
            }
            return false;
        }
        public override void Draw(Bitmap bitmapDraw)
        {
            Graphics g = Graphics.FromImage(bitmapDraw);

            g.DrawEllipse(new Pen(Color.Black), x - r, y - r, r * 2, r * 2);
        }
        public override void FillDraw(Bitmap bitmapDraw)
        {
            Graphics g = Graphics.FromImage(bitmapDraw);

            g.FillEllipse(new SolidBrush(Color.Black), x - r, y - r, r * 2, r * 2);
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
}
