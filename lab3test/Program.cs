using DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataAccess
{
    public class StudentRepository
    {
        private string dataFilePath;

        public StudentRepository(string filePath)
        {
            dataFilePath = filePath;
        }

        public List<Student> ReadStudents()
        {
            if (File.Exists(dataFilePath))
            {
                List<Student> students = new List<Student>();
                string[] lines = File.ReadAllLines(dataFilePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 4)
                    {
                        string firstName = parts[0];
                        string lastName = parts[1];
                        if (int.TryParse(parts[2], out int age) && double.TryParse(parts[3], out double averageScore))
                        {
                            Student student = new Student(firstName, lastName, age, averageScore);
                            students.Add(student);
                        }
                    }
                }

                return students;
            }
            else
            {
                return new List<Student>();
            }
        }

        public void SaveStudents(List<Student> students)
        {
            using (StreamWriter writer = new StreamWriter(dataFilePath, false))
            {
                foreach (Student student in students)
                {
                    string line = $"{student.FirstName},{student.LastName},{student.Age},{student.AverageScore}";
                    writer.WriteLine(line);
                }
            }
        }
    }
}

public class Student
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int Age { get; private set; }
    public double AverageScore { get; private set; }

    public Student(string firstName, string lastName, int age, double averageScore)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Имя и фамилия студента не могут быть пустыми.");

        if (age <= 0)
            throw new ArgumentException("Возраст не может быть отрицательным.");

        if (averageScore < 0 || averageScore > 100)
            throw new ArgumentException("Средний балл должен быть в диапазоне от 0 до 100.");

        FirstName = firstName;
        LastName = lastName;
        Age = age;
        AverageScore = averageScore;
    }
}

public class University
{
    private List<Student> students = new List<Student>();
    private StudentRepository studentRepository;

    public University(StudentRepository repository)
    {
        studentRepository = repository;
        students = studentRepository.ReadStudents();
    }

    public void AddStudent(Student student)
    {
        students.Add(student);
        SaveData();
    }

    public void RemoveStudent(Student student)
    {
        students.Remove(student);
        SaveData();
    }

    public Student FindStudent(string firstName, string lastName)
    {
        return students.FirstOrDefault(student => student.FirstName == firstName && student.LastName == lastName);
    }

    private void SaveData()
    {
        studentRepository.SaveStudents(students);
    }
}

public class Program
{
    public static void Main()
    {
        // Создание репозитория для хранения данных студентов
        string dataFilePath = "C:\\Users\\HP\\Desktop\\BD.txt";
        StudentRepository studentRepository = new StudentRepository(dataFilePath);

        // Создание университета
        University university = new University(studentRepository);

        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Добавить студента");
            Console.WriteLine("2. Удалить студента");
            Console.WriteLine("3. Найти студента");
            Console.WriteLine("4. Выйти");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Введите имя студента:");
                    string firstName = Console.ReadLine();

                    Console.WriteLine("Введите фамилию студента:");
                    string lastName = Console.ReadLine();

                    Console.WriteLine("Введите возраст студента:");
                    if (int.TryParse(Console.ReadLine(), out int age))
                    {
                        Console.WriteLine("Введите средний балл студента:");
                        if (double.TryParse(Console.ReadLine(), out double averageScore))
                        {
                            Student student = new Student(firstName, lastName, age, averageScore);
                            university.AddStudent(student);

                            Console.WriteLine("Студент добавлен.");
                        }
                        else
                        {
                            Console.WriteLine("Ошибка ввода среднего балла.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ошибка ввода возраста.");
                    }
                    break;

                case "2":
                    Console.WriteLine("Введите имя студента:");
                    string removeFirstName = Console.ReadLine();
                    Console.WriteLine("Введите фамилию студента:");
                    string removeLastName = Console.ReadLine();
                    Student studentToRemove = university.FindStudent(removeFirstName, removeLastName);

                    if (studentToRemove != null)
                    {
                        university.RemoveStudent(studentToRemove);
                        Console.WriteLine("Студент удален.");
                    }
                    else
                    {
                        Console.WriteLine("Студент не найден.");
                    }
                    break;

                case "3":
                    Console.WriteLine("Введите имя студента:");
                    string findFirstName = Console.ReadLine();
                    Console.WriteLine("Введите фамилию студента:");
                    string findLastName = Console.ReadLine();
                    Student foundStudent = university.FindStudent(findFirstName, findLastName);

                    if (foundStudent != null)
                    {
                        Console.WriteLine("Найден следующий студент:");
                        Console.WriteLine($"Имя: {foundStudent.FirstName}");
                        Console.WriteLine($"Фамилия: {foundStudent.LastName}");
                        Console.WriteLine($"Возраст: {foundStudent.Age}");
                        Console.WriteLine($"Средний балл: {foundStudent.AverageScore}");
                    }
                    else
                    {
                        Console.WriteLine("Студент не найден.");
                    }
                    break;

                case "4":
                    Console.WriteLine("Выход из программы.");
                    return;

                default:
                    Console.WriteLine("Некорректный выбор. Пожалуйста, выберите снова.");
                    break;
            }
        }
    }
}

