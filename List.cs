using System.Text.RegularExpressions;

namespace Laba4_1_5;

internal class NodeList
{
    public class Node<T>
    {
        private T _value;
        private Node<T>? _next;

        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Node<T>? Next
        {
            get { return _next; }
            set { _next = value; }
        }

        public Node(T value)
        {
            _value = value;
            _next = null;
        }
    }

    public static Node<T> ReverseList<T>(Node<T> head)
    {
        Node<T>? previous = null;
        Node<T> current = head;

        while (current != null)
        {
            Node<T> nextNode = current.Next;
            current.Next = previous;
            previous = current;
            current = nextNode;
        }

        return previous!;
    }

    public static void InsertAroundElement<T>(LinkedList<T> list, T elementWhereInsert, T elementToInsert)
    {
        if (list == null)
        {
            throw new ArgumentNullException("list");
        }

        LinkedListNode<T>? currentNode = list.First;
        while (currentNode != null)
        {
            LinkedListNode<T>? nextNode = currentNode.Next;

            if (EqualityComparer<T>.Default.Equals(currentNode.Value, elementWhereInsert))
            {
                list.AddBefore(currentNode, elementToInsert);
                list.AddAfter(currentNode, elementToInsert);
            }

            currentNode = nextNode;
        }
    }

    public static void AnalyzeDiscos(IEnumerable<string> allDiscos, Dictionary<string, IEnumerable<string>> studentVisits)
    {
        HashSet<string> discoSet = new HashSet<string>(allDiscos);

        if (studentVisits == null || studentVisits.Count == 0)
        {
            Console.WriteLine("Список студентов пуст.");
            return;
        }

        HashSet<string> visitedByAll = new HashSet<string>(discoSet);
        HashSet<string> visitedBySome = new HashSet<string>();

        foreach (KeyValuePair<string, IEnumerable<string>> pair in studentVisits)
        {
            HashSet<string> currentStudentVisits = new HashSet<string>(pair.Value);
            visitedByAll.IntersectWith(currentStudentVisits);
            visitedBySome.UnionWith(currentStudentVisits);
        }

        HashSet<string> visitedByNone = new HashSet<string>(discoSet);
        visitedByNone.ExceptWith(visitedBySome);

        Console.WriteLine("1. Дискотеки, которые посетили все студенты:");
        PrintCollection(visitedByAll);

        Console.WriteLine("\n2. Дискотеки, которые посетили некоторые студенты:");
        PrintCollection(visitedBySome);

        Console.WriteLine("\n3. Дискотеки, которые не посетил ни один студент:");
        PrintCollection(visitedByNone);
    }

    private static void PrintCollection(IEnumerable<string> collection)
    {
        bool isEmpty = true;
        foreach (string item in collection)
        {
            isEmpty = false;
            Console.WriteLine($"  - {item}");
        }

        if (isEmpty)
        {
            Console.WriteLine("  (нет элементов)");
        }
    }

    public static void PrintCharactersFromEvenWords(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Файл с текстом не найден.", filePath);
        }

        string text = File.ReadAllText(filePath);

        MatchCollection matches = Regex.Matches(text, "[а-яёА-ЯЁ]+");

        HashSet<char> uniqueChars = new HashSet<char>();

        for (int i = 1; i < matches.Count; i += 2)
        {
            string word = matches[i].Value.ToLowerInvariant();

            for (int j = 0; j < word.Length; j++)
            {
                uniqueChars.Add(word[j]);
            }
        }

        List<char> sortedChars = new List<char>(uniqueChars);
        sortedChars.Sort();

        Console.WriteLine("Уникальные символы из слов с чётными номерами в алфавитном порядке:");
        for (int i = 0; i < sortedChars.Count; i++)
        {
            Console.Write(sortedChars[i] + " ");
        }
        Console.WriteLine();
    }

    public class Applicant
    {
        private string? _lastName;
        private string? _firstName;
        private int _score1;
        private int _score2;
        private int _score3;

        public string? LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string? FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public int Score1
        {
            get { return _score1; }
            set { _score1 = value; }
        }

        public int Score2
        {
            get { return _score2; }
            set { _score2 = value; }
        }

        public int Score3
        {
            get { return _score3; }
            set { _score3 = value; }
        }

        public int TotalScore
        {
            get { return _score1 + _score2 + _score3; }
        }

        public bool IsAdmitted
        {
            get
            {
                return _score1 >= 30 && _score2 >= 30 && _score3 >= 30 && TotalScore >= 140;
            }
        }
    }

    public class ApplicantComparer : IComparer<Applicant>
    {
        public int Compare(Applicant? x, Applicant? y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            int lastNameResult = string.Compare(x.LastName, y.LastName, StringComparison.CurrentCulture);
            if (lastNameResult != 0)
            {
                return lastNameResult;
            }

            return string.Compare(x.FirstName, y.FirstName, StringComparison.CurrentCulture);
        }
    }

    public static void GenerateApplicantsFile(string filePath, int applicantCount)
    {
        Random random = new Random();
        string[] lastNames = new string[] { "Иванов", "Петров", "Сидоров", "Смирнов", "Кузнецов", "Романов", "Васильев" };
        string[] firstNames = new string[] { "Алексей", "Иван", "Вельямин", "Дмитрий", "Елена", "Мария", "Анна" };

        using (StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
        {
            writer.WriteLine(applicantCount);

            for (int i = 0; i < applicantCount; i++)
            {
                string lastName = lastNames[random.Next(lastNames.Length)];
                string firstName = firstNames[random.Next(firstNames.Length)];
                int s1 = random.Next(20, 101);
                int s2 = random.Next(20, 101);
                int s3 = random.Next(20, 101);

                writer.WriteLine(lastName + " " + firstName + " " + s1 + " " + s2 + " " + s3);
            }
        }
    }

    public static void ProcessApplicants(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Файл с результатами тестирования не найден.", filePath);
        }

        using (StreamReader reader = new StreamReader(filePath, System.Text.Encoding.UTF8))
        {
            string firstLine = reader.ReadLine();

            if (string.IsNullOrEmpty(firstLine) || !int.TryParse(firstLine, out int n))
            {
                Console.WriteLine("Ошибка формата файла: неверное количество абитуриентов.");
                return;
            }

            List<Applicant> applicants = new List<Applicant>();

            for (int i = 0; i < n; i++)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 5)
                {
                    continue;
                }

                int s1 = 0;
                int s2 = 0;
                int s3 = 0;

                if (int.TryParse(parts[parts.Length - 3], out s1) &&
                    int.TryParse(parts[parts.Length - 2], out s2) &&
                    int.TryParse(parts[parts.Length - 1], out s3))
                {
                    Applicant applicant = new Applicant();
                    applicant.LastName = parts[0];
                    applicant.FirstName = parts[1];
                    applicant.Score1 = s1;
                    applicant.Score2 = s2;
                    applicant.Score3 = s3;
                    applicants.Add(applicant);
                }
            }

            List<Applicant> admittedApplicants = new List<Applicant>();
            for (int i = 0; i < applicants.Count; i++)
            {
                if (applicants[i].IsAdmitted)
                {
                    admittedApplicants.Add(applicants[i]);
                }
            }

            admittedApplicants.Sort(new ApplicantComparer());

            Console.WriteLine("Абитуриенты, допущенные к сдаче экзаменов в первом потоке:");
            if (admittedApplicants.Count == 0)
            {
                Console.WriteLine("  (нет абитуриентов, прошедших отбор)");
                return;
            }

            for (int i = 0; i < admittedApplicants.Count; i++)
            {
                Applicant app = admittedApplicants[i];
                Console.WriteLine(app.LastName + " " + app.FirstName + " (Баллы: " + app.Score1 + ", " + app.Score2 + ", " + app.Score3 + "; Сумма: " + app.TotalScore + ")");
            }
        }
    }
}
