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
            get { return this._value; }
            set { this._value = value; }
        }

        public Node<T> Next
        {
            get { return this._next; }
            set { this._next = value; }
        }

        public Node(T value)
        {
            this._value = value;
            this._next = null;
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
}
