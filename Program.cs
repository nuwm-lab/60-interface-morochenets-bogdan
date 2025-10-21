using System;

namespace OOPTask
{
    // ============================================
    // ІНТЕРФЕЙСИ
    // ============================================

    /// <summary>
    /// Інтерфейс для роботи з персональними даними людини
    /// </summary>
    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string MiddleName { get; set; }
        DateTime DateOfBirth { get; set; }

        void SetData(string firstName, string lastName, string middleName, DateTime dateOfBirth);
        int CalculateAge(DateTime currentDate);
        int CountLetterInLastName(char letter);
        void DisplayInfo();
    }

    /// <summary>
    /// Інтерфейс для студентів, розширює IPerson
    /// </summary>
    public interface IStudent : IPerson
    {
        int AdmissionYear { get; set; }
        string Specialty { get; set; }
        int CalculateCourse(DateTime currentDate);
    }

    /// <summary>
    /// Інтерфейс для виведення інформації
    /// </summary>
    public interface IPrintable
    {
        string GetFormattedInfo();
    }

    // ============================================
    // АБСТРАКТНИЙ КЛАС
    // ============================================

    /// <summary>
    /// Абстрактний клас Person - базова реалізація для всіх людей
    /// </summary>
    public abstract class Person : IPerson, IPrintable, IDisposable
    {
        // Приватні поля з префіксом '_'
        private string _firstName;
        private string _lastName;
        private string _middleName;
        private DateTime _dateOfBirth;
        private bool _disposed = false;

        /// <summary>
        /// Ім'я людини
        /// </summary>
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Ім'я не може бути порожнім");
                _firstName = value;
            }
        }

        /// <summary>
        /// Прізвище людини
        /// </summary>
        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Прізвище не може бути порожнім");
                _lastName = value;
            }
        }

        /// <summary>
        /// По-батькові людини
        /// </summary>
        public string MiddleName
        {
            get => _middleName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("По-батькові не може бути порожнім");
                _middleName = value;
            }
        }

        /// <summary>
        /// Дата народження
        /// </summary>
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Дата народження не може бути в майбутньому");
                if (value.Year < 1900)
                    throw new ArgumentException("Дата народження занадто давня");
                _dateOfBirth = value;
            }
        }

        /// <summary>
        /// Захищений конструктор для похідних класів
        /// </summary>
        protected Person()
        {
        }

        /// <summary>
        /// Захищений конструктор з параметрами
        /// </summary>
        protected Person(string firstName, string lastName, string middleName, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            DateOfBirth = dateOfBirth;
        }

        /// <summary>
        /// Задання персональних даних
        /// </summary>
        public virtual void SetData(string firstName, string lastName, string middleName, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            DateOfBirth = dateOfBirth;
        }

        /// <summary>
        /// Обчислення віку людини на задану дату
        /// </summary>
        public virtual int CalculateAge(DateTime currentDate)
        {
            if (currentDate < _dateOfBirth)
                throw new ArgumentException("Поточна дата не може бути раніше дати народження");

            int age = currentDate.Year - _dateOfBirth.Year;

            if (currentDate.Month < _dateOfBirth.Month ||
                (currentDate.Month == _dateOfBirth.Month && currentDate.Day < _dateOfBirth.Day))
            {
                age--;
            }

            return age;
        }

        /// <summary>
        /// Підрахунок кількості входжень літери в прізвище
        /// </summary>
        public virtual int CountLetterInLastName(char letter)
        {
            if (string.IsNullOrEmpty(_lastName))
                return 0;

            int count = 0;
            string lowerLastName = _lastName.ToLower();
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

        /// <summary>
        /// Абстрактний метод - тип особи
        /// </summary>
        public abstract string GetPersonType();

        /// <summary>
        /// Виведення базової інформації
        /// </summary>
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Тип особи: {GetPersonType()}");
            Console.WriteLine($"Ім'я: {_firstName}");
            Console.WriteLine($"Прізвище: {_lastName}");
            Console.WriteLine($"По-батькові: {_middleName}");
            Console.WriteLine($"Дата народження: {_dateOfBirth:dd.MM.yyyy}");
        }

        /// <summary>
        /// Отримання форматованої інформації (реалізація IPrintable)
        /// </summary>
        public virtual string GetFormattedInfo()
        {
            return $"{GetPersonType()}: {_lastName} {_firstName} {_middleName}, {_dateOfBirth:dd.MM.yyyy}";
        }

        /// <summary>
        /// Реалізація IDisposable
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Захищений метод для очищення ресурсів
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Очищення керованих ресурсів
                    Console.WriteLine($"Об'єкт {GetPersonType()} '{_lastName}' звільнено");
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// Деструктор
        /// </summary>
        ~Person()
        {
            Dispose(false);
        }
    }

    // ============================================
    // ПОХІДНІ КЛАСИ
    // ============================================

    /// <summary>
    /// Клас звичайної людини
    /// </summary>
    public class RegularPerson : Person
    {
        private string _occupation;

        /// <summary>
        /// Професія людини
        /// </summary>
        public string Occupation
        {
            get => _occupation;
            set => _occupation = value ?? "Не вказано";
        }

        /// <summary>
        /// Конструктор за замовчуванням
        /// </summary>
        public RegularPerson() : base()
        {
            _occupation = "Не вказано";
        }

        /// <summary>
        /// Конструктор з параметрами
        /// </summary>
        public RegularPerson(string firstName, string lastName, string middleName,
                            DateTime dateOfBirth, string occupation = "Не вказано")
            : base(firstName, lastName, middleName, dateOfBirth)
        {
            Occupation = occupation;
        }

        /// <summary>
        /// Реалізація абстрактного методу
        /// </summary>
        public override string GetPersonType()
        {
            return "Звичайна людина";
        }

        /// <summary>
        /// Перевизначений метод виведення інформації
        /// </summary>
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Професія: {_occupation}");
        }

        /// <summary>
        /// Перевизначений метод форматованого виводу
        /// </summary>
        public override string GetFormattedInfo()
        {
            return base.GetFormattedInfo() + $", Професія: {_occupation}";
        }
    }

    /// <summary>
    /// Клас студента
    /// </summary>
    public class Student : Person, IStudent
    {
        private int _admissionYear;
        private string _specialty;

        /// <summary>
        /// Рік вступу до ВУЗу
        /// </summary>
        public int AdmissionYear
        {
            get => _admissionYear;
            set
            {
                if (value < 1900 || value > DateTime.Now.Year + 1)
                    throw new ArgumentException("Некоректний рік вступу");
                _admissionYear = value;
            }
        }

        /// <summary>
        /// Спеціальність студента
        /// </summary>
        public string Specialty
        {
            get => _specialty;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Спеціальність не може бути порожньою");
                _specialty = value;
            }
        }

        /// <summary>
        /// Конструктор за замовчуванням
        /// </summary>
        public Student() : base()
        {
        }

        /// <summary>
        /// Конструктор з параметрами
        /// </summary>
        public Student(string firstName, string lastName, string middleName,
                      DateTime dateOfBirth, int admissionYear, string specialty)
            : base(firstName, lastName, middleName, dateOfBirth)
        {
            AdmissionYear = admissionYear;
            Specialty = specialty;
        }

        /// <summary>
        /// Реалізація абстрактного методу
        /// </summary>
        public override string GetPersonType()
        {
            return "Студент";
        }

        /// <summary>
        /// Перевизначений метод задання даних
        /// </summary>
        public override void SetData(string firstName, string lastName, string middleName, DateTime dateOfBirth)
        {
            base.SetData(firstName, lastName, middleName, dateOfBirth);
        }

        /// <summary>
        /// Додатковий метод задання даних студента
        /// </summary>
        public void SetStudentData(string firstName, string lastName, string middleName,
                                  DateTime dateOfBirth, int admissionYear, string specialty)
        {
            base.SetData(firstName, lastName, middleName, dateOfBirth);
            AdmissionYear = admissionYear;
            Specialty = specialty;
        }

        /// <summary>
        /// Обчислення курсу навчання
        /// </summary>
        public int CalculateCourse(DateTime currentDate)
        {
            if (currentDate.Year < _admissionYear)
                return 0;

            int course = currentDate.Year - _admissionYear;

            if (currentDate.Month < 9)
            {
                course--;
            }

            return Math.Max(0, course);
        }

        /// <summary>
        /// Перевизначений метод виведення інформації
        /// </summary>
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Рік вступу: {_admissionYear}");
            Console.WriteLine($"Спеціальність: {_specialty}");
        }

        /// <summary>
        /// Перевизначений метод форматованого виводу
        /// </summary>
        public override string GetFormattedInfo()
        {
            return base.GetFormattedInfo() + $", Рік вступу: {_admissionYear}, Спеціальність: {_specialty}";
        }
    }

    // ============================================
    // ГОЛОВНА ПРОГРАМА
    // ============================================

    /// <summary>
    /// Головний клас програми
    /// </summary>
    class Program
    {
        /// <summary>
        /// Безпечне зчитування цілого числа з консолі
        /// </summary>
        static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out result))
                {
                    if (result >= min && result <= max)
                        return result;
                    Console.WriteLine($"Число має бути в діапазоні від {min} до {max}");
                }
                else
                {
                    Console.WriteLine("Некоректне введення. Спробуйте ще раз.");
                }
            }
        }

        /// <summary>
        /// Безпечне зчитування дати з консолі
        /// </summary>
        static DateTime ReadDate(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                int day = ReadInt("День (1-31): ", 1, 31);
                int month = ReadInt("Місяць (1-12): ", 1, 12);
                int year = ReadInt("Рік: ", 1900, DateTime.Now.Year + 1);

                try
                {
                    return new DateTime(year, month, day);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Некоректна дата. Спробуйте ще раз.\n");
                }
            }
        }

        /// <summary>
        /// Головний метод програми
        /// </summary>
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║   Робота з класами 'Людина' та 'Студент'              ║");
            Console.WriteLine("║   Демонстрація інтерфейсів та абстрактних класів      ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝\n");

            try
            {
                // Створення об'єкта звичайної людини
                using (RegularPerson person = new RegularPerson("Іван", "Петренко", "Миколайович",
                                                                new DateTime(1980, 6, 15), "Інженер"))
                {
                    Console.WriteLine("--- Інформація про людину ---");
                    person.DisplayInfo();
                    Console.WriteLine();

                    // Введення поточної дати
                    DateTime currentDate = ReadDate("\nВведіть поточну дату для обчислення віку:");

                    // Обчислення віку людини
                    int personAge = person.CalculateAge(currentDate);
                    Console.WriteLine($"\n✓ Вік людини станом на {currentDate:dd.MM.yyyy}: {personAge} років");

                    // Створення об'єкта студента
                    using (Student student = new Student("Олена", "Коваленко", "Петрівна",
                                                        new DateTime(2004, 9, 20), 2022, "Комп'ютерні науки"))
                    {
                        Console.WriteLine("\n--- Інформація про студента ---");
                        student.DisplayInfo();

                        // Визначення віку та курсу студента
                        int studentAge = student.CalculateAge(currentDate);
                        int course = student.CalculateCourse(currentDate);
                        Console.WriteLine($"\n✓ Вік студента станом на {currentDate:dd.MM.yyyy}: {studentAge} років");
                        Console.WriteLine($"✓ Курс навчання: {course}");

                        // Підрахунок літери в прізвищі людини
                        Console.Write("\nВведіть літеру для підрахунку в прізвищі людини: ");
                        char letter = Console.ReadKey().KeyChar;
                        Console.WriteLine();

                        int countPerson = person.CountLetterInLastName(letter);
                        Console.WriteLine($"✓ Кількість літери '{letter}' в прізвищі '{person.LastName}': {countPerson}");

                        // Підрахунок літери в прізвищі студента
                        Console.Write("\nВведіть літеру для підрахунку в прізвищі студента: ");
                        char letterStudent = Console.ReadKey().KeyChar;
                        Console.WriteLine();

                        int countStudent = student.CountLetterInLastName(letterStudent);
                        Console.WriteLine($"✓ Кількість літери '{letterStudent}' в прізвищі '{student.LastName}': {countStudent}");

                        // Демонстрація SetStudentData
                        Console.WriteLine("\n\n--- Демонстрація методу SetStudentData ---");
                        student.SetStudentData("Марія", "Шевченко", "Іванівна",
                                             new DateTime(2003, 3, 10), 2021, "Інформаційні технології");
                        student.DisplayInfo();

                        int newAge = student.CalculateAge(currentDate);
                        Console.WriteLine($"✓ Вік студента: {newAge} років");

                        // Демонстрація поліморфізму через інтерфейс IPerson
                        Console.WriteLine("\n\n--- Демонстрація роботи через інтерфейс IPerson ---");
                        IPerson[] people = new IPerson[] { person, student };

                        foreach (IPerson p in people)
                        {
                            Console.WriteLine($"\n✓ Тип: {((Person)p).GetPersonType()}");
                            Console.WriteLine($"  ПІБ: {p.LastName} {p.FirstName} {p.MiddleName}");
                            Console.WriteLine($"  Вік: {p.CalculateAge(currentDate)} років");
                        }

                        // Демонстрація IPrintable
                        Console.WriteLine("\n\n--- Демонстрація інтерфейсу IPrintable ---");
                        IPrintable[] printables = new IPrintable[] { person, student };

                        foreach (IPrintable printable in printables)
                        {
                            Console.WriteLine($"✓ {printable.GetFormattedInfo()}");
                        }
                    }
                }

                Console.WriteLine("\n\n╔════════════════════════════════════════════════════════╗");
                Console.WriteLine("║   Програма успішно завершена!                         ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Помилка: {ex.Message}");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }
    }
}
