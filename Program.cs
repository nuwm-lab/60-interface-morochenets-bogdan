using System;

namespace OOPTask
{
    // Базовий клас "Людина"
    public class Person
    {
        // Поля класу
        protected string firstName;
        protected string lastName;
        protected string middleName;
        protected int day;
        protected int month;
        protected int year;

        // Властивості
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
        public Person()
        {
        }

        // Конструктор з параметрами
        public Person(string firstName, string lastName, string middleName, 
                     int day, int month, int year)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.middleName = middleName;
            this.day = day;
            this.month = month;
            this.year = year;
        }

        // Метод задання відповідних даних
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

        // Метод визначення віку людини за поточною датою
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

        // Метод обчислення кількості входжень літери в прізвище
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

        // Метод виведення інформації про людину
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Ім'я: {firstName}");
            Console.WriteLine($"Прізвище: {lastName}");
            Console.WriteLine($"По-батькові: {middleName}");
            Console.WriteLine($"Дата народження: {day:D2}.{month:D2}.{year}");
        }
    }

    // Похідний клас "Студент"
    public class Student : Person
    {
        // Додаткові поля класу
        protected int admissionYear;
        protected string specialty;

        // Властивості
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

        // Перевантажений метод задання відповідних даних
        public override void SetData(string firstName, string lastName, string middleName,
                                    int day, int month, int year)
        {
            base.SetData(firstName, lastName, middleName, day, month, year);
        }

        // Перевантажений метод задання даних з додатковими полями студента
        public void SetData(string firstName, string lastName, string middleName,
                           int day, int month, int year, int admissionYear, string specialty)
        {
            base.SetData(firstName, lastName, middleName, day, month, year);
            this.admissionYear = admissionYear;
            this.specialty = specialty;
        }

        // Перевантажений метод визначення віку студента
        public override int CalculateAge(DateTime currentDate)
        {
            return base.CalculateAge(currentDate);
        }

        // Перевантажений метод обчислення кількості входжень літери в прізвище
        public override int CountLetterInLastName(char letter)
        {
            return base.CountLetterInLastName(letter);
        }

        // Метод обчислення курсу навчання
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

            Console.WriteLine("=== Робота з класами 'Людина' та 'Студент' ===\n");

            // Створення об'єкта класу "Людина"
            Person person = new Person("Іван", "Петренко", "Миколайович", 15, 6, 1980);
            
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

            // Створення об'єкта класу "Студент"
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

            Console.WriteLine("\n\nНатисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }
    }
}
