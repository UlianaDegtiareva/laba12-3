using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLaba10;

namespace laba12_3
{
    public class MyTree<T> where T : IInit, IComparable, ICloneable, new()
    {
        Point<T>? root = null;

        public Point<T>? Root
        {
            get { return root; }
            set { root = value; }
        }

        int count = 0;

        public int Count => count;

        public MyTree(int lenght)
        { 
            count = lenght;
            root = MakeTree(lenght);
        }

        public MyTree() { }

        public void ShowTree()
        {
            Show(root);
        }

        //ИСД
        Point<T> MakeTree(int length)
        {
            if (length == 0)
            {
                return null;
            }

            // Определение разделения элементов на левое и правое поддеревья
            int nleft = (length - 1) / 2;
            int nright = length - nleft - 1;

            // Создание нового корневого элемента
            T data = new T();
            data.RandomInit();
            Point<T> newItem = new Point<T>(data);

            // Рекурсивное создание левого и правого поддеревьев
            newItem.Left = MakeTree(nleft);
            newItem.Right = MakeTree(nright);

            return newItem;
        }

        // Рекурсивный метод для вывода дерева на консоль
        void Show(Point<T>? point, int spaces = 5)
        {
            if (point != null)
            {
                Show(point.Left, spaces + 5);
                for (int i = 0; i < spaces; i++) 
                    Console.Write("  ");
                Console.WriteLine(point.Data);
                Show(point.Right, spaces + 5);
            }
        }

        //дерево поиска
        public void AddPoint(T dataold)
        {
            T data = (T)dataold.Clone();
            Point<T>? point = root;
            Point<T>? current = null;
            bool isEx = false;

            while (point != null && !isEx)
            {
                current = point;

                if (point.Data.CompareTo(data) == 0) // Проверка наличия элемента
                {
                    isEx = true;
                }
                else
                {
                    if (point.Data.CompareTo(data) > 0)
                    {
                        point = point.Left;
                    }
                    else
                    {
                        point = point.Right;
                    }
                }
            }

            // Если элемент уже существует, просто выходим
            if (isEx) return;

            Point<T> newPoint = new Point<T>(data);
            if (current.Data.CompareTo(data) > 0) current.Left = newPoint;
            else current.Right = newPoint;
           
            count++;
        }

        // Преобразование дерева в массив для последующего поиска
        public void TransformToArray(Point<T>? point, T[] arr, ref int current)
        {
            if (point != null)
            {
                TransformToArray(point.Left, arr, ref current);
                arr[current++] = point.Data; // Увеличиваем current после присвоения
                TransformToArray(point.Right, arr, ref current);
            }
        }

        public MyTree<T> TransformToFindTree()
        {
            if (root == null || count == 0)
            {
                throw new Exception("Дерево пустое. Перед продолжением сгенерируйте идеально сбалансированное дерево");
            }
            else
            {
                // Создание нового дерева для поиска
                T[] arr = new T[count];
                int current = 0;
                TransformToArray(root, arr, ref current);

                // Создание нового дерева поиска с корнем из идеально сбалансированного дерева
                MyTree<T> tree = new MyTree<T>();
                int mid = (arr.Length - 1) / 2;
                tree.Root = new Point<T>(arr[mid]);
                tree.count = 1;

                for (int i = 1; i < arr.Length; i++)
                {
                    tree.AddPoint(arr[i]);
                }
                return tree;
            }
        }



        public void Clear()
        {
            ClearMemory(root);
            root = null;
            count = 0;
        }

        void ClearMemory(Point<T>? node)
        {
            if (node == null)
            {
                return;
            }

            ClearMemory(node.Left);
            ClearMemory(node.Right);

            // Удаление узла из памяти
            node.Data = default; // Сброс данных узла
            node.Left = null;
            node.Right = null;
        }

        public double CalculateAverageNameLength()
        {

            int totalLength = 0;
            int totalCount = 0;

            CalculateNameLength(Root, ref totalLength, ref totalCount);

            if (totalCount == 0)
            {
                return 0;
            }

            return (double)totalLength / totalCount;
        }

        public void CalculateNameLength(dynamic node, ref int totalLength, ref int totalCount)
        {
            if (node != null)
            {
                if (node.Data != null && node.Data.Name != null)
                {
                    string name = node.Data.Name;
                    totalLength += name.Length;
                    totalCount++;
                }

                CalculateNameLength(node.Left, ref totalLength, ref totalCount);
                CalculateNameLength(node.Right, ref totalLength, ref totalCount);
            }
        }
    }
}
