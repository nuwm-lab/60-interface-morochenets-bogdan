using System;

namespace OOPTask
{
    // Інтерфейс IPerson - визначає контракт для роботи з персональними даними
    public interface IPerson
    {
        // Властивості
        string FirstName { get; set; }
        string LastName { get; set; }
        string MiddleName { get; set; }
        int Day { get; set; }
        int Month { get; set; }
        int Year { get; set; }

        // Методи (тільки сигнатури)
        void SetData(string firstName, string lastName, string middleName, 
                    int day, int month, int year);
        int CalculateAge(DateTime currentDate);
        int CountLetterInLastName(char letter);
        void DisplayInfo();
    }

    // Абстрактний клас Person - реалізує інтерфейс IPerson
    public abstract class Person : IPerson
    {
        // Захищені поля
        protected string firstName;
        protected string lastName;
        protected string middleName;
        protected int day;
        protected int month;
        protected int year;

        // Реалізація властивостей з інтерфейсу
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string MiddleName
        {
            get { return middleName; }
            set { middleName = value; }
        }

        public int Day
        {
            get { return day; }
            set { day = value; }
        }

        public int Month
        {
            get { return month; }
            set { month = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        // Конструктор за замовчуванням
        protected Person()
        {
        }

        // Конструктор з параметрами
        protected Person(string firstName, string lastName, string middleName, 
                        int day, int month, int year)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.middleName = middleName;
            this.day = day;
            this.month = month;
            this.year = year;
        }

        // Реалізація методу з інтерфейсу
        public virtual void SetData(string firstName, string lastName, string middleName, 
                                   int day, int month, int year)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.middleName = middleName;
            this.day = day;
            this.month = month;
            this.year = year;
        }

        // Реалізація методу з інтерфейсу
        public virtual int CalculateAge(DateTime currentDate)
        {
            int age = currentDate.Year - year;

            // Перевірка, чи вже минув день народження в цьому році
            if (currentDate.Month < month || 
                (currentDate.Month == month && currentDate.Day < day))
            {
                age--;
            }

            return age;
        }

        // Реалізація методу з інтерфейсу
        public virtual int CountLetterInLastName(char letter)
        {
            int count = 0;
            string lowerLastName = lastName.ToLower();
            char lowerLetter = char.ToLower(letter);

            foreach (char c in lowerLastName)
            {
                if (c == lowerLetter)
                {
                    count++;
                }
            }

            return count;
        }

        // Абстрактний метод - повинен бути реалізований у похідних класах
        public abstract string GetPersonType();

        // Реалізація методу з інтерфейсу
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Тип особи: {GetPersonType()}");
            Console.WriteLine($"Ім'я: {firstName}");
            Console.WriteLine($"Прізвище: {lastName}");
            Console.WriteLine($"По-батькові: {middleName}");
            Console.WriteLine($"Дата народження: {day:D2}.{month:D2}.{year}");
        }
    }

    // Похідний клас RegularPerson (Звичайна людина) - наслідує абстрактний клас Person
    public class RegularPerson : Person
    {
        private string occupation; // Професія

        public string Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }

        // Конструктор за замовчуванням
        public RegularPerson() : base()
        {
        }

        // Конструктор з параметрами
        public RegularPerson(string firstName, string lastName, string middleName,
                            int day, int month, int year, string occupation = "Не вказано")
            : base(firstName, lastName, middleName, day, month, year)
        {
            this.occupation = occupation;
        }

        // Реалізація абстрактного методу
        public override string GetPersonType()
        {
            return "Звичайна людина";
        }

        // Перевантажений метод виведення інформації
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Професія: {occupation}");
        }
    }

    // Інтерфейс IStudent - розширює функціонал для студентів
    public interface IStudent : IPerson
    {
        int AdmissionYear { get; set; }
        string Specialty { get; set; }
        int CalculateCourse(DateTime currentDate);
    }

    // Похідний клас Student (Студент) - наслідує абстрактний клас Person і реалізує IStudent
    public class Student : Person, IStudent
    {
        // Додаткові поля класу
        protected int admissionYear;
        protected string specialty;

        // Реалізація властивостей з інтерфейсу IStudent
        public int AdmissionYear
        {
            get { return admissionYear; }
            set { admissionYear = value; }
        }

        public string Specialty
        {
            get { return specialty; }
            set { specialty = value; }
        }

        // Конструктор за замовчуванням
        public Student() : base()
        {
        }

        // Конструктор з параметрами
        public Student(string firstName, string lastName, string middleName,
                      int day, int month, int year, int admissionYear, string specialty)
            : base(firstName, lastName, middleName, day, month, year)
        {
            this.admissionYear = admissionYear;
            this.specialty = specialty;
        }

        // Реалізація абстрактного методу
        public override string GetPersonType()
        {
            return "Студент";
        }

        // Перевантажений метод задання даних з додатковими полями студента
        public void SetData(string firstName, string lastName, string middleName,
                           int day, int month, int year, int admissionYear, string specialty)
        {
            base.SetData(firstName, lastName, middleName, day, month, year);
            this.admissionYear = admissionYear;
            this.specialty = specialty;
        }

        // Реалізація методу з інтерфейсу IStudent - обчислення курсу навчання
        public int CalculateCourse(DateTime currentDate)
        {
            int course = currentDate.Year - admissionYear;
            
            // Якщо поточний місяць до вересня, то це попередній курс
            if (currentDate.Month < 9)
            {
                course--;
            }

            return Math.Max(0, course);
        }

        // Перевантажений метод виведення інформації про студента
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Рік вступу: {admissionYear}");
            Console.WriteLine($"Спеціальність: {specialty}");
        }
    }

    // Головний клас програми
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Робота з класами 'Людина' та 'Студент' ===");
            Console.WriteLine("=== Демонстрація інтерфейсів та абстрактних класів ===\n");

            // Створення об'єкта класу "Звичайна людина" (похідний від абстрактного класу Person)
            RegularPerson person = new RegularPerson("Іван", "Петренко", "Миколайович", 
                                                     15, 6, 1980, "Інженер");
            
            Console.WriteLine("--- Інформація про людину ---");
            person.DisplayInfo();
            Console.WriteLine();

            // Введення поточної дати
            Console.WriteLine("Введіть поточну дату для обчислення віку:");
            Console.Write("День: ");
            int currentDay = int.Parse(Console.ReadLine());
            Console.Write("Місяць: ");
            int currentMonth = int.Parse(Console.ReadLine());
            Console.Write("Рік: ");
            int currentYear = int.Parse(Console.ReadLine());
            
            DateTime currentDate = new DateTime(currentYear, currentMonth, currentDay);

            // Обчислення віку людини
            int personAge = person.CalculateAge(currentDate);
            Console.WriteLine($"\nВік людини станом на {currentDate.ToShortDateString()}: {personAge} років");

            // Створення об'єкта класу "Студент" (реалізує інтерфейс IStudent)
            Student student = new Student("Олена", "Коваленко", "Петрівна", 
                                         20, 9, 2004, 2022, "Комп'ютерні науки");
            
            Console.WriteLine("\n--- Інформація про студента ---");
            student.DisplayInfo();
            
            // Визначення віку студента
            int studentAge = student.CalculateAge(currentDate);
            Console.WriteLine($"\nВік студента станом на {currentDate.ToShortDateString()}: {studentAge} років");
            
            // Визначення курсу студента
            int course = student.CalculateCourse(currentDate);
            Console.WriteLine($"Курс навчання: {course}");

            // Підрахунок кількості входжень літери в прізвище людини
            Console.Write("\nВведіть літеру для підрахунку в прізвищі людини: ");
            char letter = Console.ReadKey().KeyChar;
            Console.WriteLine();
            
            int countPerson = person.CountLetterInLastName(letter);
            Console.WriteLine($"Кількість літери '{letter}' в прізвищі '{person.LastName}': {countPerson}");

            // Підрахунок кількості входжень літери в прізвище студента
            Console.Write("\nВведіть літеру для підрахунку в прізвищі студента: ");
            char letterStudent = Console.ReadKey().KeyChar;
            Console.WriteLine();
            
            int countStudent = student.CountLetterInLastName(letterStudent);
            Console.WriteLine($"Кількість літери '{letterStudent}' в прізвищі '{student.LastName}': {countStudent}");

            // Демонстрація методу SetData
            Console.WriteLine("\n\n--- Демонстрація методу SetData ---");
            student.SetData("Марія", "Шевченко", "Іванівна", 
                           10, 3, 2003, 2021, "Інформаційні технології");
            student.DisplayInfo();
            
            int newAge = student.CalculateAge(currentDate);
            Console.WriteLine($"Вік студента: {newAge} років");

            // Демонстрація поліморфізму через інтерфейс
            Console.WriteLine("\n\n--- Демонстрація роботи через інтерфейс IPerson ---");
            IPerson[] people = new IPerson[] { person, student };
            
            foreach (IPerson p in people)
            {
                Console.WriteLine($"\nТип: {((Person)p).GetPersonType()}");
                Console.WriteLine($"ПІБ: {p.LastName} {p.FirstName} {p.MiddleName}");
                Console.WriteLine($"Вік: {p.CalculateAge(currentDate)} років");
            }

            Console.WriteLine("\n\nНатисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }
    }
}
