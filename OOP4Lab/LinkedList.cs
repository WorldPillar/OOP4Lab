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
        public void push_front(AbstractShape newObj)
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
        public void push_back(AbstractShape newObj)
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
        public void insert(Node it, AbstractShape newObj)
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
        public AbstractShape getRoot()
        {
            if (root == null)
                return null;
            return root.getObj;
        }
        public AbstractShape getTail()
        {
            if (tail == null)
                return null;
            return tail.getObj;
        }
        //Возвращает текущий объект, хранящийся в Node
        public AbstractShape getObject()
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
        private AbstractShape obj;
        private Node next;
        private Node prev;
        public Node(AbstractShape obj)
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
        public AbstractShape getObj
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
    abstract class AbstractShape
    {
        protected HatchBrush brush;
        //геттеры и сеттеры для кисти
        public abstract HatchBrush hBrush { get; }
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

        public abstract void ColorChange(HatchBrush hatch);
    }

    class CGroup : AbstractShape
    {
        LinkedList shapes;
        protected bool current;

        public CGroup()
        {
            shapes = new LinkedList();
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

        public LinkedList getList()
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

            current = false;
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
        public override void Move(int dx, int dy)
        {
            shapes.front();
            while (!shapes.eol())
            {
                shapes.getObject().Move(dx, dy);

                shapes.next();
            }
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
        public override void Resize(int size)
        {
            shapes.front();
            while (!shapes.eol())
            {
                shapes.getObject().Resize(size);

                shapes.next();
            }
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