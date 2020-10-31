using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP4Lab
{
    class LinkedList
    {
        Node root;
        Node tail;
        Node current;
        private int count = 0;
        public LinkedList()
        {
            root = null;
            tail = root;
            current = root;
        }
        public int size
        {
            get => count;
        }
        public bool isEmpty() { return root == null; }
        public bool eol() { return current == null; }
        public void front() { current = root; }
        public void back() { current = tail; }
        public void next() { current = current.nextNode; }
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
        }
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
        public void erase(Node it)
        {
            --count;
            Node prevIt = it.prevNode;
            Node nextIt = it.nextNode;
            if (prevIt != null)
                prevIt.nextNode = it.nextNode;
            else
                root = nextIt;
            if (nextIt != null)
                nextIt.prevNode = it.prevNode;
            else
                tail = prevIt;
            it = null;
        }
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
        public Shape getObject()
        {
            return current.getObj;
        }
    }
    class Node
    {
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
    }
    abstract class Shape
    {
        protected Color color;

        public abstract void Move(int x, int y);
    }
    class CCircle : Shape
    {
        private int x;
        private int y;
        private int r = 5;
        public CCircle()
        {
            x = 0;
            y = 0;
            r = 0;
        }
        public CCircle(int x, int y)
        {
            this.x = x;
            this.y = y;
            color = Color.Black;
        }
        public override void Move(int dx, int dy)
        {
            x += dx;
            y += dy;
        }
        public int X
        {
            get => x;
        }
        public int Y
        {
            get => y;
        }
        ~CCircle()
        {
            x = 0;
            y = 0;
            r = 0;
            color = Color.Empty;
        }
    }
}
