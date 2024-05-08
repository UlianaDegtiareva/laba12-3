using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLaba10;

namespace laba12_3
{
    public class Program
    {
        static void Main(string[] args)
        {
            MyTree<Plants> tree = new MyTree<Plants>();
            MyTree<Plants> plant = new MyTree<Plants>();
            int answer;
            do
            {
                Console.WriteLine();
                Console.WriteLine("------------------ РАБОТА С БИНАРНЫМ ДЕРЕВОМ -----------------");
                Console.WriteLine("1. Создание идеально сбалансированного дерева");
                Console.WriteLine("2. Печать идеально сбалансированного дерева");
                Console.WriteLine("3. Вычисление средней длины названий элементов");
                Console.WriteLine("4. Создание дерева поиска из идеально сбалансированного дерева");
                Console.WriteLine("5. Печать дерева поиска");
                Console.WriteLine("6. Удаление дерева из памяти");
                Console.WriteLine("7. Выход");
                Console.WriteLine();
                answer = InputAnswer();
                Console.WriteLine();
                switch (answer)
                {
                    case 1: tree = Сreating(); break;
                    case 2: Print(tree); break;
                    case 3: AverageName(tree); break;
                    case 4:
                        {
                            if (tree.Count == 0)
                            {
                                Console.WriteLine("Перед продолжением необходимо создать идеально сбалансированное дерево");
                            }
                            else
                            {
                                plant = tree.TransformToFindTree();
                                Console.WriteLine("Дерево поиска создано");
                            }
                            break;
                        }
                    case 5: PrintFindTree(plant); break;
                    case 6:
                        if (plant.Count == 0)
                        {
                            Console.WriteLine("Дерево поиска не создано");
                        }
                        else 
                        {
                            plant.Clear();
                            Console.WriteLine("Дерево поиска удалено");
                        }
                        if (tree.Count == 0)
                        {
                            Console.WriteLine("Идеально сбалансированное дерево не создано");
                        }
                        else
                        {
                            tree.Clear();
                            Console.WriteLine("Идеально сбалансированное дерево удалено");
                        }
                        break;
                    case 7: break;
                    default:
                        {
                            Console.WriteLine("Неправильно задан пункт меню");
                            break;
                        }
                }
            } while (answer != 7);
            Console.ReadLine();
        }

        static int InputAnswer()
        {
            int answer;
            bool Ok;
            do
            {
                string buf = Console.ReadLine();
                Ok = int.TryParse(buf, out answer);
                if (!Ok)
                {
                    Console.WriteLine("Неправильно выбран пункт меню. Повторите ввод");
                }
            } while (!Ok);
            return answer;
        }
        static MyTree<Plants> Сreating()
        {
            int size;
            bool oK;
            do
            {
                Console.WriteLine("Введите размер списка:");
                string buf = Console.ReadLine();
                oK = int.TryParse(buf, out size);
                if ((!oK) || (size <= 0))
                {
                    Console.WriteLine("Неверный ввод количества элементов. Повторите ввод");
                }
            } while ((!oK) || (size <= 0));
            MyTree<Plants> tree = new MyTree<Plants>(size);
            Console.WriteLine("Дерево создано");
            return tree;
        }
        static void Print(MyTree<Plants> tree)
        {
            if (tree.Count == 0)
            {
                Console.WriteLine("Перед продолжением необходимо создать идеально сбалансированное дерево");
            }
            else 
            {
                Console.WriteLine("ИДЕАЛЬНО СБАЛАНСИРОВАННОЕ ДЕРЕВО: ");
                tree.ShowTree();
            }
        }
        static void AverageName(MyTree<Plants> tree)
        {
            if (tree.Count == 0)
            {
                Console.WriteLine("Дерево пустое, вычисление невозможно. Перед продолжением необходимо создать дерево");
            }
            else
            {
                double height = tree.CalculateAverageNameLength();
                Console.WriteLine($"Средняя длина всех названий элементов равна = {height}");
            }
        }
        static void PrintFindTree(MyTree<Plants> tree)
        {
            if (tree.Count == 0)
            {
                Console.WriteLine("Перед продолжением необходимо создать дерево поиска");
            }
            else
            {
                Console.WriteLine("ДЕРЕВО ПОИСКА: ");
                tree.ShowTree();
            }
        }

    }
}
