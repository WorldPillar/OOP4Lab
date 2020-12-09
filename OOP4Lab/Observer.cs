using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace OOP4Lab
{
    class TreeViewer
    {
        TreeView treeviewer;

        LinkedList observer;
        public TreeViewer(TreeView newTree)
        {
            treeviewer = newTree;
        }

        //Данный метод вызывается при изменении хранилища
        public void OnSubjectChanged(LinkedList list)
        {
            treeviewer.BeginUpdate();
            treeviewer.Nodes[0].Nodes.Clear();
            int i = 0;

            //Добавляем в дерево каждый элемент хранилища
            list.front();
            while (list.eol() == false)
            {
                TreeNode newNode = new TreeNode();
                treeviewer.Nodes[0].Nodes.Add(newNode);
                if (list.getObject().Current)
                    treeviewer.SelectedNode = newNode;

                processNode(treeviewer.Nodes[0].Nodes[i++], list.getObject());
                list.next();
            }

            treeviewer.EndUpdate();
        }

        //Добавление очередного узла дерева
        void processNode (TreeNode tn, AbstractShape leaf)
        {
            //Добавляем в дерево название объекта
            tn.Text = leaf.ToString().Substring(8);

            //Пробуем преобразовать объект хранилища в группу
            CGroup group = leaf as CGroup;
            if (group != null)
            {
                LinkedList list = group.getList();
                int i = 0;

                //Вызываем для каждого узла группы функцию добавления в дерево
                list.front();
                while (list.eol() == false)
                {
                    tn.Nodes.Add(new TreeNode());

                    processNode(tn.Nodes[i++], list.getObject());
                    list.next();
                }
            }
        }

        public void addObserver(LinkedList list)
        {
            observer = list;
        }

        //Сообщает подписчикам об изменении
        private void notify()
        {
            if (observer != null)
                observer.OnSubjectChanged(this);
        }

        public void SelectedChanged()
        {
            notify();
        }

        public TreeView TreeView
        {
            get => treeviewer;
        }
    }
}
