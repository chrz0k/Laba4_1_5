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

    private static void Main(string[] args)
    {
        Validator validator = new Validator();

        int choice = 1;
        while (choice != 0)
        {
            Console.Clear();
            Console.WriteLine("=== Лабораторная работа №4 ===");
            Console.WriteLine(" 0) Выход из программы");

            Console.WriteLine(" 1) 1.9.");
            Console.WriteLine(" 2) 2.9.");
            Console.WriteLine(" 3) 3.9.");
            Console.WriteLine(" 4) 4.9.");
            Console.WriteLine(" 5) 5.9.");

            Console.WriteLine("============================");
            choice = validator.ReadInt("Выберите пункт(0-5): ", 0, 5);
            Console.WriteLine("============================");

            switch (choice)
            {
                case 1:
                    {
                        string? input = validator.ReadString("Введите элементы списка через пробел: ");

                        var elements = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (elements.Length == 0)
                        {
                            Console.WriteLine("Ошибка: Список элементов пуст.");
                            Pause();
                            break;
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
                        Console.WriteLine($"Список до вставки {elementToInsert} по бокам элемента {elementWhereInsert}: ");
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
                        Console.WriteLine($"Список после вставки {elementToInsert} по бокам элемента {elementWhereInsert}: ");
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
                        string filePath = "text.txt";

                        string text = validator.ReadString("Введите текст на русском языке: ");
                        File.WriteAllText(filePath, text ?? string.Empty, System.Text.Encoding.UTF8);

                        Console.WriteLine();

                        try
                        {
                            PrintCharactersFromEvenWords(filePath);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ошибка при обработке файла: " + ex.Message);
                        }

                        Pause();
                        break;
                    }

                case 5:
                    {
                        Console.WriteLine("1. Сгенерировать данные автоматически (в файл applicants.txt)");
                        Console.WriteLine("2. Ввести данные абитуриентов вручную с клавиатуры");
                        int input = validator.ReadInt("Выберите вариант (1 или 2): ", 1, 2);

                        string filePath = "applicants.txt";

                        if (input == 2)
                        {
                            int n = validator.ReadInt("Введите количество абитуриентов N (до 500): ", 1, 500);
                            using (StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                            {
                                writer.WriteLine(n);

                                Console.WriteLine("Формат: Фамилия Имя Балл1 Балл2 Балл3 (например: Романов Вельямин 48 39 55)");
                                for (int i = 0; i < n; i++)
                                {
                                    string line = validator.ReadString($"Абитуриент #{i+1} : ");
                                    writer.WriteLine(line);
                                }
                            }
                        }
                        else
                        {
                            int count = validator.ReadInt("Сколько всего абитуриентов: ", 1, 500);
                            GenerateApplicantsFile(filePath, count);
                            Console.WriteLine($"Файл '{filePath}' успешно создан.");
                        }

                        try
                        {
                            Console.WriteLine("\nРезультаты отбора:");
                            ProcessApplicants(filePath);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ошибка при обработке: " + ex.Message);
                        }

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