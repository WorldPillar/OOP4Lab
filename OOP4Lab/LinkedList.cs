using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        protected int count = 0;
        //Список наблюдателей
        List<CObserver> observers;
        //Конструктор по умолчанию
        public LinkedList()
        {
            root = null;
            tail = root;
            current = root;
            observers = new List<CObserver>();
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

            notifyEveryone();
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

            notifyEveryone();
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

            notifyEveryone();
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

            notifyEveryone();
        }
        //Полностью очищает список
        public void clear()
        {
            if (root == null)
                return;
            do
            {
                Node delThis = root;
                root = root.nextNode;
                delThis = null;
            } while (root != null);
            tail = null;
            count = 0;

            notifyEveryone();
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

        protected virtual AbstractShape createShapes(string code)
        {
            return null;
        }

        public void loadShapes(StreamReader file)
        {
            int count = int.Parse(file.ReadLine());

            for (int i = 0; i < count; ++i)
            {
                string code;

                while ((code = file.ReadLine()) != null)
                {
                    AbstractShape shape = createShapes(code);

                    if (shape != null)
                    {
                        shape.Load(file);

                        push_back(shape);
                    }
                }
            }
        }

        public void saveShapes(StreamWriter file)
        {
            file.WriteLine(size);
            front();
            while (!eol())
            {
                getObject().Save(file);
                next();
            }
        }

        public void addObserver(CObserver newObs)
        {
            observers.Add(newObs);
        }

        private void notifyEveryone()
        {
            foreach (var it in observers)
            {
                it.OnSubjectChanged(this);
            }
        }
    }

    class MyLinkedList : LinkedList
    {
        protected override AbstractShape createShapes(string code)
        {
            AbstractShape shape = null;
            switch (code)
            {
                case "Circle":
                    shape = new CCircle();
                    break;
                case "Rectangle":
                    shape = new CRectangle();
                    break;
                case "Polygon":
                    shape = new CPolygon();
                    break;
                case "Star":
                    shape = new CStar();
                    break;
                case "Group":
                    shape = new CGroup();
                    break;
            }

            return shape;
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
        public abstract bool Move(int x, int y, Graphics g);
        //Изменение фигуры на заданный размер
        public abstract bool Resize(int size, Graphics g);
        //Попытка двигать фигуру в форме
        public abstract bool TryMove(int dx, int dy, Graphics g);
        //Попытка изменить размер фигуры в форме
        public abstract bool TryResize(int d, Graphics g);
        //Проверка нахождения курсора при нажатии в форме
        public abstract bool inShape(int x, int y);
        //Отрисовка фигуры
        public abstract void Draw(Graphics g);
        public abstract void ColorChange(HatchBrush hatch);
        public abstract void Save(StreamWriter sw);
        public abstract void Load(StreamReader sw);
    }
}