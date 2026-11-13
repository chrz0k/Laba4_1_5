using System.Runtime.CompilerServices;
using static Laba4_1_5.NodeList;

namespace Laba4_1_5;

internal class Program
{
    public static void Pause()
    {
        Console.WriteLine("============================");
        Console.Write("Нажмите любую кнопку для продолжения...");
        Console.ReadKey();
    }

    static void Main(string[] args)
    {
        Validator validator = new Validator();

        int choice = 1;
        while (choice != 0)
        {
            Console.Clear();
            Console.WriteLine("=== Лабораторная работа № ===");
            Console.WriteLine(" 0) Выход из программы");

            Console.WriteLine(" 1) 1.9.");
            Console.WriteLine(" 2) 2.9.");
            Console.WriteLine(" 3) 3.9.");
            Console.WriteLine(" 4) 4.9.");
            Console.WriteLine(" 5) 5.9.");

            Console.WriteLine("============================");
            choice = validator.ReadInt("Выберите пункт(0-7): ", 0, 7);
            Console.WriteLine("============================");

            switch (choice)
            {
                case 1:
                    {
                        string? input = validator.ReadString("Введите элементы списка через пробел: ");

                        var elements = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (elements.Length == 0)
                        {
                            return;
                        }

                        NodeList.Node<string>? head = null;
                        NodeList.Node<string>? tail = null;

                        foreach (var el in elements)
                        {
                            Node<string>? newNode = new NodeList.Node<string>(el);
                            if (tail == null)
                            {
                                head = newNode;
                                tail = newNode;
                            }
                            else
                            {
                                tail.Next = newNode;
                                tail = newNode;
                            }
                        }

                        Console.WriteLine();
                        Console.WriteLine("Список до переворота: ");
                        var current = head;
                        while (current != null)
                        {
                            if (current.Next == null)
                            {
                                Console.Write($"{current.Value}");
                            }
                            else
                            {
                                Console.Write($"{current.Value} ");
                            }

                            current = current.Next;
                        }
                        Console.WriteLine();

                        var reversedHead = NodeList.ReverseList(head!);

                        Console.WriteLine();
                        Console.WriteLine("Список после переворота: ");
                        current = reversedHead;
                        while (current != null)
                        {
                            if (current.Next == null)
                            {
                                Console.Write($"{current.Value}");
                            }
                            else
                            {
                                Console.Write($"{current.Value} ");
                            }

                            current = current.Next;
                        }
                        Console.WriteLine();

                        Pause();
                        break;
                    }

                case 2:
                    {
                        string? input = validator.ReadString("Введите элементы списка через пробел: ");

                        var list = new LinkedList<string>(input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

                        Console.WriteLine();
                        string elementWhereInsert = validator.ReadString("Введите элемент E (у которого вставить): ");
                        string elementToInsert = validator.ReadString("Введите элемент F (который вставить): ");

                        Console.WriteLine();
                        Console.WriteLine($"Список до вставки {elementWhereInsert} по бокам элемента {elementToInsert}: ");
                        var current = list.First;
                        while (current != null)
                        {
                            if (current.Next == null)
                            {
                                Console.Write($"{current.Value}");
                            }
                            else
                            {
                                Console.Write($"{current.Value} ");
                            }

                            current = current.Next;
                        }
                        Console.WriteLine();

                        Console.WriteLine();
                        Console.WriteLine($"Список после вставки {elementWhereInsert} по бокам элемента {elementToInsert}: ");
                        NodeList.InsertAroundElement(list, elementWhereInsert, elementToInsert);
                        current = list.First;
                        while (current != null)
                        {
                            if (current.Next == null)
                            {
                                Console.Write($"{current.Value}");
                            }
                            else
                            {
                                Console.Write($"{current.Value} ");
                            }

                            current = current.Next;
                        }

                        Console.WriteLine();

                        Pause();
                        break;
                    }

                case 3:
                    {
                        string discoInput = validator.ReadString("Введите список дискотек города через пробел: ");

                        string[] allDiscos = discoInput.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        int studentCount = validator.ReadInt("Введите количество студентов в группе: ", 1, 100);
                        Dictionary<string, IEnumerable<string>> studentVisits = new Dictionary<string, IEnumerable<string>>();

                        for (int i = 0; i < studentCount; i++)
                        {
                            string name = validator.ReadString($"Введите имя студента №{i+1}: ");

                            string visitsInput = validator.ReadString($"Введите какие дискотеки посетил {name} (через пробел): ");
                            string[] visits = visitsInput.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                            studentVisits[name] = visits;
                        }

                        Console.WriteLine("\nРезультаты анализа:");
                        NodeList.AnalyzeDiscos(allDiscos, studentVisits);

                        Pause();
                        break;
                    }

                case 4:
                    {
                        Console.WriteLine("Хз");

                        Pause();
                        break;
                    }

                case 5:
                    {
                        Console.WriteLine("Хз");

                        Pause();
                        break;
                    }

                case 6:
                    {
                        Console.WriteLine("Хз");

                        Pause();
                        break;
                    }

                case 7:
                    {
                        Console.WriteLine("Хз");

                        Pause();
                        break;
                    }

                case 0:
                    Console.WriteLine("Выход из программы.");
                    break;

                default:
                    Console.WriteLine("Ошибка! Введено неверное значение.");
                    Pause();
                    break;
            }
        }
    }
}