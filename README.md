# Распопов Егор ИТ-12 Лабораторная №4

# Задание 1

## Задача 9

### Текст задачи

1.9. Составить программу, которая переворачивает список L, т.е. изменяет ссылки в этом списке так, чтобы его элементы оказались расположенными в обратном порядке.

### Алгоритм решения

1. В классе `NodeList` определяется вложенный обобщенный класс узла односвязного списка `Node<T>` с закрытыми полями `_value` и `_next`, соответствующими свойствами и конструктором.
2. Статический метод `ReverseList<T>(Node<T> head)` принимает ссылку на голову исходного списка.
3. С помощью трех указателей (`previous`, `current`, `nextNode`) выполняется последовательный обход списка за один проход: ссылка `Next` каждого текущего узла перенаправляется на предыдущий узел.
4. После завершения цикла метод возвращает новую голову списка (`previous`).

Реализация класса узла и метода переворота списка:

```csharp
public class Node<T>
{
    private T _value;
    private Node<T>? _next;

    public T Value
    {
        get { return _value; }
        set { _value = value; }
    }

    public Node<T> Next
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
```

### Тестирование

<img width="612" height="229" alt="image" src="https://github.com/user-attachments/assets/f0aec168-14ed-488e-be3c-8f99bd73c334" />

# Задание 2

## Задача 9

### Текст задачи

2.9. В списке L справа и слева от элемента E вставить элемент F.

### Алгоритм решения

1. Статический метод `InsertAroundElement<T>` принимает двусвязный список `LinkedList<T>`, искомый элемент `elementWhereInsert` ($E$) и вставляемый элемент `elementToInsert` ($F$).
2. Выполняется последовательный проход по узлам списка начиная с `list.First`. Перед обработкой текущего узла ссылка на следующий узел сохраняется в переменную `nextNode`, чтобы избежать зацикливания при вставке новых элементов.
3. При совпадении значения текущего узла с искомым элементом (проверяется через `EqualityComparer<T>.Default.Equals`) вызываются встроенные методы `list.AddBefore` и `list.AddAfter` для вставки элемента $F$ слева и справа соответственно.

Реализация метода:

```csharp
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
```

### Тестирование

<img width="616" height="284" alt="image" src="https://github.com/user-attachments/assets/34927256-925e-43fa-a990-25aa40360a93" />

# Задание 3

## Задача 9

### Текст задачи

3.9. Есть перечень дискотек города. Студенты группы любят посещать дискотеки. Известно для каждого студента, в каких дискотеках он побывал. Определить:
- в какие дискотеки из перечня ходили все студенты группы;
- в какие дискотеки из перечня ходили некоторые студенты группы;
- в какие дискотеки из перечня не ходил ни один из студентов группы?

### Алгоритм решения

1. Метод `AnalyzeDiscos` принимает полный перечень дискотек `allDiscos` и словарь `studentVisits`, где ключом является имя студента, а значением — коллекция посещенных им дискотек.
2. Для решения применяются операции над множествами (`HashSet<string>`):
   - множество `visitedByAll` инициализируется полным перечнем дискотек и на каждой итерации пересекается с дискотеками текущего студента (`IntersectWith`);
   - множество `visitedBySome` объединяет дискотеки всех студентов (`UnionWith`);
   - множество `visitedByNone` вычисляется как разность полного перечня дискотек и объединения `visitedBySome` (`ExceptWith`).
3. Результаты выводятся на экран с помощью вспомогательного закрытого метода `PrintCollection`.

Реализация метода:

```csharp
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
```

### Тестирование

<img width="581" height="415" alt="image" src="https://github.com/user-attachments/assets/2bb70e4c-9130-4283-8c88-5deae5e09458" />

# Задание 4

## Задача 9

### Текст задачи

4.9. Файл содержит текст на русском языке. Напечатать в алфавитном порядке символы, которые встречаются хотя бы однажды в словах с чётными номерами (нумерацию вести с 1).

### Алгоритм решения

1. Метод `PrintCharactersFromEvenWords` проверяет существование файла и считывает его содержимое с помощью `File.ReadAllText`.
2. С помощью регулярного выражения `Regex.Matches(text, "[а-яёА-ЯЁ]+")` из текста извлекаются все слова на русском языке без знаков препинания.
3. Так как нумерация слов начинается с 1, словам с чётными номерами соответствуют нечётные индексы в коллекции (`i = 1, 3, 5...`). Символы этих слов приводятся к нижнему регистру и добавляются в множество `HashSet<char>` для исключения дубликатов.
4. Уникальные символы копируются в список `List<char>`, сортируются по алфавиту методом `Sort()` и выводятся на экран.

Реализация метода:

```csharp
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
```

### Тестирование

<img width="616" height="176" alt="image" src="https://github.com/user-attachments/assets/1f564c1a-c10c-4e75-976e-440a84f37051" />

# Задание 5

## Задача 9

### Текст задачи

5.9. В некотором вузе абитуриенты проходили предварительное тестирование, по результатам которого они могут быть допущены к сдаче вступительных экзаменов в первом потоке. Тестирование проводится по трём предметам, по каждому предмету абитуриент может набрать от 0 до 100 баллов. При этом к сдаче экзаменов в первом потоке допускаются абитуриенты, набравшие по результатам тестирования не менее 30 баллов по каждому из трёх предметов, причём сумма баллов должна быть не менее 140. На вход программы подаются сведения о результатах предварительного тестирования. Известно, что общее количество участников тестирования не превосходит 500.
В первой строке вводится количество абитуриентов, принимавших участие в тестировании, N.
Далее следуют N строк, имеющих следующий формат:
`<Фамилия><Имя><Баллы>`
Здесь `<Фамилия>` – строка, состоящая не более чем из 20 символов; `<Имя>` – строка, состоящая не более чем из 15 символов, `<Баллы>` – строка, содержащая три целых числа, разделенных пробелом – баллы, полученные на тестировании по каждому из трёх предметов. При этом `<Фамилия>` и `<Имя>`, `<Имя>` и `<Баллы>` разделены одним пробелом. Пример входной строки:
`Романов Вельямин 48 39 55`
Напишите программу, которая будет выводить на экран фамилии и имена абитуриентов, допущенных к сдаче экзаменов в первом потоке. При этом фамилии должны выводиться в алфавитном порядке.

### Алгоритм решения

1. Для хранения данных об абитуриенте создан класс `Applicant` со свойствами `LastName`, `FirstName`, `Score1`, `Score2`, `Score3`, вычисляемым свойством суммы баллов `TotalScore` и логическим свойством допуска `IsAdmitted` (проверяющим условие: каждый балл $\ge 30$ и сумма $\ge 140$).
2. Отдельный метод `GenerateApplicantsFile` заполняет исходный текстовый файл данными о $N$ абитуриентах в требуемом формате.
3. Метод `ProcessApplicants` построчно считывает данные из файла с помощью `StreamReader`, формирует список объектов `Applicant` и отбирает прошедших по конкурсу в список `admittedApplicants`.
4. Для сортировки допущенных абитуриентов в алфавитном порядке без использования LINQ реализован класс-компаратор `ApplicantComparer : IComparer<Applicant>`, который передается в метод `admittedApplicants.Sort(...)`.

Реализация классов и методов:

```csharp
public class Applicant
{
    private string _lastName;
    private string _firstName;
    private int _score1;
    private int _score2;
    private int _score3;

    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    public string FirstName
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
    public int Compare(Applicant x, Applicant y)
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
```

### Тестирование

<img width="558" height="325" alt="image" src="https://github.com/user-attachments/assets/f48adfc0-003a-43b2-830f-b6f6c1f40fc3" />
