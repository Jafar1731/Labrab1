//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;

//public class Person
//{
//    public string FirstName { get; set; }
//    public string LastName { get; set; }
//    public DateTime DateOfBirth { get; set; }
//    public int PersonalNumber { get; }

//    private static int lastPersonalNumber = 0;

//    public Person(string firstName, string lastName, DateTime dateOfBirth)
//    {
//        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
//            throw new ArgumentNullException("Поля имени не должно быть пустым");
//        FirstName = firstName;
//        LastName = lastName;
//        DateOfBirth = dateOfBirth;
//        PersonalNumber = ++lastPersonalNumber;
//    }
//}

//public static class PeopleRepository
//{
//    private static Dictionary<int, Person> peopleData = new Dictionary<int, Person>();

//    public static void AddPerson(Person person)
//    {
//        peopleData[person.PersonalNumber] = person;
//    }

//    public static Person GetPersonByPersonalNumber(int personalNumber)
//    {
//        return peopleData.TryGetValue(personalNumber, out var person) ? person : null;
//    }
//}

//public class Student : Person
//{
//    public int StudentId { get; set; }
//    private double AverageScore { get; set; }

//    public Student(string firstName, string lastName, DateTime dateOfBirth, double averageScore, int studentId)
//        : base(firstName, lastName, dateOfBirth)
//    {
//        if (averageScore <= 0 && averageScore > 100)
//            throw new AggregateException("Введите корректный формат среднего бала студента");
//        StudentId = studentId;
//        AverageScore = averageScore;
//    }

//    public double GetAverageScore()
//    {
//        return AverageScore;
//    }
//}

//public class StudentRepository
//{
//    private string dataFilePath;

//    public StudentRepository(string filePath)
//    {
//        dataFilePath = filePath;
//    }

//    public List<Student> ReadStudents()
//    {
//        if (File.Exists(dataFilePath))
//        {
//            List<Student> students = new List<Student>();
//            string[] lines = File.ReadAllLines(dataFilePath);

//            foreach (string line in lines)
//            {
//                string[] parts = line.Split(',');
//                if (parts.Length == 5)
//                {
//                    string firstName = parts[0];
//                    string lastName = parts[1];
//                    if (int.TryParse(parts[2], out int age) && double.TryParse(parts[3], out double averageScore) &&
//                        int.TryParse(parts[4], out int studentId))
//                    {
//                        // Определяем дату рождения по возрасту (возможно, это не лучший способ)
//                        DateTime dateOfBirth = DateTime.Now.AddYears(-age);

//                        Student student = new Student(firstName, lastName, dateOfBirth, averageScore, studentId);
//                        students.Add(student);
//                    }
//                }
//            }

//            return students;
//        }
//        else
//        {
//            return new List<Student>();
//        }
//    }

//    public void SaveStudents(List<Student> students)
//    {
//        using (StreamWriter writer = new StreamWriter(dataFilePath, false))
//        {
//            foreach (Student student in students)
//            {
//                string line = $"{student.FirstName},{student.LastName},{student.DateOfBirth:yyyy-MM-dd},{student.GetAverageScore()},{student.StudentId}";
//                writer.WriteLine(line);
//            }
//        }
//    }
//}

//public class University
//{
//    private List<Student> students = new List<Student>();
//    private StudentRepository studentRepository;
//    private int lastStudentId = 0;

//    public University(StudentRepository repository)
//    {
//        studentRepository = repository;
//        students = studentRepository.ReadStudents();
//        if (students.Count > 0)
//        {
//            lastStudentId = students.Max(s => s.StudentId);
//        }
//    }

//    public void AddStudent(Student student)
//    {
//        student.StudentId = ++lastStudentId;
//        students.Add(student);
//        SaveData();
//    }

//    public void RemoveStudent(int studentId)
//    {
//        Student studentToRemove = students.FirstOrDefault(student => student.StudentId == studentId);
//        if (studentToRemove != null)
//        {
//            students.Remove(studentToRemove);
//            SaveData();
//        }
//    }

//    public Student FindStudentByStudentId(int studentId)
//    {
//        return students.FirstOrDefault(student => student.StudentId == studentId);
//    }

//    private void SaveData()
//    {
//        studentRepository.SaveStudents(students);
//    }
//}

//public class Program
//{
//    public static void Main()
//    {
//        string dataFilePath = "C:\\Users\\HP\\Desktop\\BD.txt";
//        StudentRepository studentRepository = new StudentRepository(dataFilePath);

//        University university = new University(studentRepository);

//        while (true)
//        {
//            Console.WriteLine("Выберите действие:");
//            Console.WriteLine("1. Добавить студента");
//            Console.WriteLine("2. Удалить студента");
//            Console.WriteLine("3. Найти студента");
//            Console.WriteLine("4. Выйти");

//            string choice = Console.ReadLine();

//            switch (choice)
//            {
//                case "1":
//                    Console.WriteLine("Введите имя студента:");
//                    string firstName = Console.ReadLine();

//                    Console.WriteLine("Введите фамилию студента:");
//                    string lastName = Console.ReadLine();

//                    Console.WriteLine("Введите дату рождения студента (гггг-мм-дд):");
//                    if (DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth))
//                    {
//                        Console.WriteLine("Введите средний балл студента:");
//                        if (double.TryParse(Console.ReadLine(), out double averageScore))
//                        {
//                            Student student = new Student(firstName, lastName, dateOfBirth, averageScore, 0);
//                            university.AddStudent(student);

//                            Console.WriteLine($"Студент добавлен, id студента {student.StudentId} ");
//                        }
//                        else
//                        {
//                            Console.WriteLine("Ошибка ввода среднего балла.");
//                        }
//                    }
//                    else
//                    {
//                        Console.WriteLine("Ошибка ввода даты рождения.");
//                    }
//                    break;

//                case "2":
//                    Console.WriteLine("Введите номер зачётной книжки студента:");
//                    if (int.TryParse(Console.ReadLine(), out int studentId))
//                    {
//                        Student studentToRemove = university.FindStudentByStudentId(studentId);
//                        if (studentToRemove != null)
//                        {
//                            university.RemoveStudent(studentId);
//                            Console.WriteLine("Студент удален.");
//                        }
//                        else
//                        {
//                            Console.WriteLine("Студент не найден.");
//                        }
//                    }
//                    else
//                    {
//                        Console.WriteLine("Ошибка ввода номера зачётной книжки.");
//                    }
//                    break;

//                case "3":
//                    Console.WriteLine("Введите номер зачётной книжки студента:");
//                    if (int.TryParse(Console.ReadLine(), out int findStudentId))
//                    {
//                        Student foundStudent = university.FindStudentByStudentId(findStudentId);
//                        if (foundStudent != null)
//                        {
//                            Console.WriteLine("Найден следующий студент:");
//                            Console.WriteLine($"Имя: {foundStudent.FirstName}");
//                            Console.WriteLine($"Фамилия: {foundStudent.LastName}");
//                            Console.WriteLine($"Дата рождения: {foundStudent.DateOfBirth:yyyy-MM-dd}");
//                            Console.WriteLine($"Средний балл: {foundStudent.GetAverageScore()}");
//                        }
//                        else
//                        {
//                            Console.WriteLine("Студент не найден.");
//                        }
//                    }
//                    else
//                    {
//                        Console.WriteLine("Ошибка ввода номера зачётной книжки.");
//                    }
//                    break;

//                case "4":
//                    Console.WriteLine("Выход из программы.");
//                    return;

//                default:
//                    Console.WriteLine("Некорректный выбор. Пожалуйста, выберите снова.");
//                    break;
//            }
//        }
//    }
//}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using DataAccess;
using Л2;

namespace Л2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            StusientRepository student1 = new StusientRepository();
            student1.Writeonfile();

        }
    }

    class Человек
    {
        private static int numpeople = 0001;

        private string family, name;
        private double agePeople;


        public string FamilyPeople { get => family; set => family = value; }
        public string NamePeople { get => name; set => name = value; }
        public double AgePeople { get => agePeople; set => agePeople = value; }
        public int NumPeople
        {
            get => numpeople;
            set => numpeople = value;
        }


    }


    static class AllPeople
    {


        // static public List<string> allpeoples = new List<string>();

        static public List<string> peoplesfamily = new List<string>();
        static public List<string> peoplesname = new List<string>();
        static public List<double> peoplesdate = new List<double>();
        static public List<string> peoplesnum = new List<string>();

        public static void InfoPeopleFamily(Человек p1) => peoplesfamily.Add(p1.FamilyPeople);
        static public List<string> WritePeopleFamily() => peoplesfamily;


        static public void InfoPeopleName(Человек p1) => peoplesname.Add(p1.NamePeople);
        static public List<string> WritePeopleName() => peoplesname;


        static public void InfoPeopleAge(Человек p1) => peoplesdate.Add(p1.AgePeople);
        static public List<double> WritePeopleAge() => peoplesdate;


        static public void InfoPeopleNum(Человек p1) => peoplesnum.Add(p1.NumPeople.ToString());
        static public List<string> WritePeopleNum() => peoplesnum;

    }





    class Студент : Человек
    {
        private float age, a_score;



        public float AScoreStudent
        {
            get => a_score;
            set => a_score = value;
        }

    }





    class Университет
    {

        private string university;



        List<string> allnumstud = new List<string>();
        List<string> alluniver = new List<string>();
        List<float> allascorestud = new List<float>();

        private List<string> peoples = new List<string>();
        private List<string> students = new List<string>();

        private int numzach = 0001;


        public void InfoAScore(Студент s1) => allascorestud.Add(s1.AScoreStudent);
        public List<float> WriteAScore() => allascorestud;


        public void InfoAllUniver(Университет u1) => alluniver.Add(u1.University);
        public List<string> WriteAllUnever() => alluniver;


        public void InfoNumStud(Университет u1, int i) => allnumstud.Add(u1.NumZach.ToString());
        public List<string> WriteNumStud() => allnumstud;


        public int NumZach
        {
            get => numzach;
            set => numzach = value;
        }
        public string University
        {
            get => university;
            set => university = value;
        }
        public List<string> Addlist
        {
            get => students;
            set => students = value;
        }
        //public List<string> AddPeoples(Человек people) {
        // peoples.Add($"{people.FamilyPeople} {people.NamePeople} {people.AgePeople} ");
        // return peoples;
        //}
        public List<string> AddPeole(Человек people)
        {


            for (int i = 0; i < AllPeople.WritePeopleFamily().Count(); i++)
            {

                peoples.Add($"Фамилия: {AllPeople.WritePeopleFamily()[i]}, имя: {AllPeople.WritePeopleName()[i]}, возраст: {AllPeople.WritePeopleAge()[i]}, личный номер: {AllPeople.WritePeopleNum()[i]}");
            }
            return peoples;

        }

        public List<string> AddStudent(Студент student, Университет un1, List<string> mass)
        {

            for (int i = 0; i < mass.Count(); i++)
            {

                students.Add($"Фамилия: {AllPeople.WritePeopleFamily()[Convert.ToInt32(mass[i])]}, имя: {AllPeople.WritePeopleName()[Convert.ToInt32(mass[i])]}, возраст: {AllPeople.WritePeopleAge()[Convert.ToInt32(mass[i])]}, личный номер: {AllPeople.WritePeopleNum()[Convert.ToInt32(mass[i])]}, средний балл: {un1.WriteAScore()[i]}, университет: {un1.WriteAllUnever()[i]}, номер зачётной книжки: {un1.WriteNumStud()[i]} ");
            }
            //students.Add($"{student.FamilyPeople} {student.NamePeople} {student.AgePeople} {student.AScoreStudent}");
            return students;
        }
        public void DeleteStudent(int i)
        {
            students.RemoveAt(i);

        }


        //public void SearchStudent(string i)
        //{
        // int a = students.IndexOf(i);
        // var s = students[a];
        // Console.WriteLine("\nСтудент: " + s);

        //}



    }//class
}




namespace DataAccess
{
    class StusientRepository
    {
        string read;
        int schetchick = 0;

        List<string> mass = new List<string>(); // для хранения порядка студентов

        List<string> s1 = new List<string>(); // peoples
        List<string> s2 = new List<string>(); // для сохранения в файл students

        Человек people = new Человек();
        Университет un1 = new Университет();
        Студент student = new Студент();

        string filepath = "C:\\Users\\HP\\Desktop\\BD.txt";

        public void Writeonfile()
        {


            while (true)
            {
                Console.WriteLine($"\nДобавить человека: D " +
                $"\nДобавить студента Ds " +
                $"\nУдалить человека: De" +
                $"\nУдалить студента: Des" +
                $"\nНайти человека: Se" +
                $"\nНайти студента SeS " +
                $"\nВывести список на экран: Op " +
                $"\nЗаписать полученный список в файл: Save " +
                $"\nОткрыть файл со списком: Open\n");

                read = Console.ReadLine().Trim();
                if (read == "D")
                {
                    try { Dowonloadp(); }
                    catch (FormatException)
                    {

                        Console.WriteLine("\nВы ввели не число ");
                        continue;
                    }
                }
                else if (read == "Ds")
                {
                    Dowonloads();
                }
                else if (read == "Des")
                {
                    DeleteS();
                }
                else if (read == "De")
                {
                    try { Delete(); }
                    catch (FormatException)
                    {

                        Console.WriteLine("\nВы ввели не число ");
                        continue;
                    }
                }
                else if (read == "Se")
                {
                    try { Search(); }
                    catch (ArgumentOutOfRangeException)
                    {

                        Console.WriteLine("\nВы ввели не все данные");
                        continue;
                    }

                }
                else if (read == "SeS" || read == "Ses")
                {
                    SearchS();

                }
                else if (read == "Op")
                {


                    Console.WriteLine($"Список всех людей.");
                    for (int i = 0; i < AllPeople.WritePeopleFamily().Count(); i++)
                    {

                        Console.WriteLine($"Фамилия: {AllPeople.WritePeopleFamily()[i]}, имя: {AllPeople.WritePeopleName()[i]}, возраст: {AllPeople.WritePeopleAge()[i]}, личный номер: {AllPeople.WritePeopleNum()[i]}");

                    }
                    Console.WriteLine($"\nСписок студентов.");
                    for (int i = 0; i < mass.Count(); i++)
                    {

                        // Console.WriteLine(s2);
                        Console.WriteLine($"Фамилия: {AllPeople.WritePeopleFamily()[Convert.ToInt32(mass[i])]}, имя: {AllPeople.WritePeopleName()[Convert.ToInt32(mass[i])]}, возраст: {AllPeople.WritePeopleAge()[Convert.ToInt32(mass[i])]}, личный номер: {AllPeople.WritePeopleNum()[Convert.ToInt32(mass[i])]}, средний балл: {un1.WriteAScore()[i]}, университет: {un1.WriteAllUnever()[i]}, номер зачётной книжки: {un1.WriteNumStud()[i]} ");
                    }
                }
                else if (read == "Save")
                {
                    File.WriteAllLines(filepath, s1.Concat(s2).ToList());
                }
                else if (read == "Open")
                {

                    un1.Addlist = File.ReadLines(filepath).ToList();

                    Console.WriteLine("\nСписок студентов: ");
                    foreach (string line in un1.Addlist)
                    {
                        Console.WriteLine(line);
                    }

                }
                else { break; };

            }
        }

        public void Dowonloadp()
        {
            Console.Write("Фамилия: ");
            people.FamilyPeople = Console.ReadLine().Trim();
            if (people.FamilyPeople == null)
            {
                Console.Write("Фамилия не может быть пустой, введите ещё раз: ");
                people.FamilyPeople = Console.ReadLine();
            }
            AllPeople.InfoPeopleFamily(people);

            Console.Write("Имя: ");
            people.NamePeople = Console.ReadLine().Trim();
            if (people.NamePeople == null)
            {
                Console.Write("Имя не может быть пустым, введите ещё раз: ");
                people.NamePeople = Console.ReadLine();
            }
            AllPeople.InfoPeopleName(people);

            Console.Write("Возраст: ");
            people.AgePeople = float.Parse(Console.ReadLine());
            if (people.AgePeople < 0 || people.AgePeople == null)
            {
                Console.Write("Возраст введён некорректно, введите ещё раз: ");
                people.AgePeople = float.Parse(Console.ReadLine());
            }
            AllPeople.InfoPeopleAge(people);


            AllPeople.InfoPeopleNum(people);



            schetchick++;
            people.NumPeople++;

            s1 = un1.AddPeole(people);


        }


        int i = 0;
        private void Dowonloads()
        {

            Dowonloadp();
            schetchick--;

            Console.Write("Средний балл: ");
            student.AScoreStudent = float.Parse(Console.ReadLine());
            if (student.AScoreStudent == null || student.AScoreStudent < 0)
            {
                Console.Write("Средний балл введён некорректно, введите ещё раз: ");
                student.AScoreStudent = float.Parse(Console.ReadLine());
            }
            un1.InfoAScore(student);

            Console.WriteLine("Университет: ");
            un1.University = Console.ReadLine();
            un1.InfoAllUniver(un1);


            un1.InfoNumStud(un1, schetchick);
            mass.Add(schetchick.ToString());

            i++;
            schetchick++;
            un1.NumZach++;

            s2 = un1.AddStudent(student, un1, mass);
        }

        private void Delete()
        {

            Console.Write($"Введите номер человека для удаления: ");
            string num = Console.ReadLine();
            AllPeople.WritePeopleFamily().RemoveAt(AllPeople.WritePeopleNum().IndexOf(num));
            AllPeople.WritePeopleName().RemoveAt(AllPeople.WritePeopleNum().IndexOf(num));
            AllPeople.WritePeopleAge().RemoveAt(AllPeople.WritePeopleNum().IndexOf(num));

            if (FindForDelete(num))
            {

                un1.WriteAScore().RemoveAt(mass.IndexOf(AllPeople.WritePeopleNum().IndexOf(num).ToString()));
                un1.WriteAllUnever().RemoveAt(mass.IndexOf(AllPeople.WritePeopleNum().IndexOf(num).ToString()));
                un1.WriteNumStud().RemoveAt(mass.IndexOf(AllPeople.WritePeopleNum().IndexOf(num).ToString()));
                mass.RemoveAt(mass.IndexOf(AllPeople.WritePeopleNum().IndexOf(num).ToString()));
            }
            int a = AllPeople.WritePeopleNum().IndexOf(num);
            AllPeople.WritePeopleNum().RemoveAt(a);




            //s1.RemoveAt(s2.IndexOf(num));


        }
        private bool FindForDelete(string num)
        {
            foreach (string item in mass)
            {
                if (item == AllPeople.WritePeopleNum().IndexOf(num).ToString())
                {
                    return true;
                }
            }
            return false;
        }


        private void DeleteS()
        {
            Console.Write($"Введите номер зачётной книжки студента для удаления: ");
            string num = Console.ReadLine();
            AllPeople.WritePeopleFamily().RemoveAt(Convert.ToInt32(mass[un1.WriteNumStud().IndexOf(num)]));
            AllPeople.WritePeopleName().RemoveAt(Convert.ToInt32(mass[un1.WriteNumStud().IndexOf(num)]));
            AllPeople.WritePeopleAge().RemoveAt(Convert.ToInt32(mass[un1.WriteNumStud().IndexOf(num)]));
            AllPeople.WritePeopleNum().RemoveAt(Convert.ToInt32(mass[un1.WriteNumStud().IndexOf(num)]));
            un1.WriteAScore().RemoveAt(un1.WriteNumStud().IndexOf(num));
            un1.WriteAllUnever().RemoveAt(un1.WriteNumStud().IndexOf(num));
            mass.RemoveAt(un1.WriteNumStud().IndexOf(num));
            int a = un1.WriteNumStud().IndexOf(num);
            un1.WriteNumStud().RemoveAt(a);


            //s2.RemoveAt(s2.IndexOf(num));

            // un1.DeleteStudent(str);
            //s1.RemoveAt(str);
        }
        private void Search()
        {

            Console.Write($"Введите номер человека: ");
            string num = Console.ReadLine();

            Console.WriteLine($"Человек: \nФамилия: {AllPeople.WritePeopleFamily()[AllPeople.WritePeopleNum().IndexOf(num)]}, имя: {AllPeople.WritePeopleName()[AllPeople.WritePeopleNum().IndexOf(num)]}, возраст: {AllPeople.WritePeopleAge()[AllPeople.WritePeopleNum().IndexOf(num)]}, личный номер: {AllPeople.WritePeopleNum()[AllPeople.WritePeopleNum().IndexOf(num)]}");


        }
        private void SearchS()
        {
            Console.Write($"Введите номер зачётной книжки: ");
            string num = Console.ReadLine();
            // Console.WriteLine($"Фамилия: {AllPeople.WritePeopleFamily()[un1.WriteNumStud().IndexOf(num)]}, имя: {AllPeople.WritePeopleName()[un1.WriteNumStud().IndexOf(num)]}, возраст: {AllPeople.WritePeopleAge()[un1.WriteNumStud().IndexOf(num)]}, личный номер: {AllPeople.WritePeopleNum()[un1.WriteNumStud().IndexOf(num)]}, средний балл: {un1.WriteAScore()[un1.WriteNumStud().IndexOf(num)]}, университет: {un1.WriteAllUnever()[un1.WriteNumStud().IndexOf(num)]}, номер зачётной книжки: {un1.WriteNumStud()[un1.WriteNumStud().IndexOf(num)]} ");

            Console.WriteLine($"Фамилия: {AllPeople.WritePeopleFamily()[Convert.ToInt32(mass[un1.WriteNumStud().IndexOf(num)])]}, " +
                $"имя: {AllPeople.WritePeopleName()[Convert.ToInt32(mass[un1.WriteNumStud().IndexOf(num)])]}," +
                $" возраст: {AllPeople.WritePeopleAge()[Convert.ToInt32(mass[un1.WriteNumStud().IndexOf(num)])]}," +
                $" личный номер: {AllPeople.WritePeopleNum()[Convert.ToInt32(mass[un1.WriteNumStud().IndexOf(num)])]}," +
                $" средний балл: {un1.WriteAScore()[un1.WriteNumStud().IndexOf(num)]}," +
                $" университет: {un1.WriteAllUnever()[un1.WriteNumStud().IndexOf(num)]}," +
                $" номер зачётной книжки: {un1.WriteNumStud()[un1.WriteNumStud().IndexOf(num)]}");

            // un1.SearchStudent(Console.ReadLine().Trim());
        }



    }//class




}