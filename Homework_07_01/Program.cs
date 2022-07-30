using System;
using System.Runtime.CompilerServices;

namespace Homework_07_01
{
    class Program
    {
        /// <summary>
        /// Метод, создающий ежедневник
        /// </summary>
        /// <returns>Ежедневник</returns>
        static Notebook CreateNB(out string pathNB)
        {
            Console.WriteLine("Выберете путь для хранения ежедневника");
            pathNB = Console.ReadLine();
            Notebook notebook = new Notebook(pathNB);
            return notebook;
        }
        /// <summary>
        /// Метод, проверяющий переданное значение
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="flag">Отождествление числовому типу</param>
        /// <returns></returns>
        static int CheckValue(string value, out bool flag)
        {
            flag = int.TryParse(value, out int number);     //Попытка преобразования строчного значения в целое число
            return number;
        }
        /// <summary>
        /// Метод, запускающий цикл проверки введеного значения
        /// </summary>
        /// <returns>Значение</returns>
        static int CheckChoice()
        {
            int choice = 0;
            bool flag = false;
            while (!flag)
            {
                choice = CheckValue(Console.ReadLine(), out flag);
                if (!flag)
                {
                    Console.Clear();
                    Console.WriteLine("Введите корректное значение!");
                }
            }
            return choice;
        }
        /// <summary>
        /// Метод, проверяющий введенную дату
        /// </summary>
        /// <returns>Дата</returns>
        static DateTime CheckDate()
        {
            DateTime datetime = DateTime.Now;
            bool flag = false;

            while (!flag)
            {
                Console.WriteLine("Введите дату в формате:" + datetime);
                flag = DateTime.TryParse(Console.ReadLine(), out datetime);
            }
            return datetime;
        }
        /// <summary>
        /// Метод
        /// </summary>
        /// <returns></returns>
        static int CheckChoiceLoad()
        {
            int choice = 0;
            bool flag = false;

            while (!flag)
            {
                flag = true;
                Console.Clear();
                Console.WriteLine("Желаете извлечь \n" +
                "1)все записи \n" +
                "2)за конкретную дату?");
                choice = CheckValue(Console.ReadLine(), out flag);

                if (choice != 1 && choice != 2) flag = false;
            }
            return choice;
        }
        /// <summary>
        /// Метод, выводящий на экран стартовое приветствие и считывающие выбор действия
        /// </summary>
        /// <returns>Значение выбора</returns>
        static int Greetings()
        {
            Console.Clear();
            Console.WriteLine("Добро пожаловать в программу ежедневника! \n");
            Console.WriteLine("Выберите действие: \n\n" +
                "1)Создать запись \n" +
                "2)Извлечь записи из файла \n" +
                "3)Сохранить записи в файл \n" +
                "4)Отредактировать запись \n" +
                "5)Удалить запись \n" +
                "6)Отсортировать записи \n" +
                "7)Вывести ежеднивник на экран \n" +
                "8)Закрыть программу \n");
            int choice = CheckChoice();
            return choice;
        }
        /// <summary>
        /// Основнной метод, запускающий цикл работы с ежедневником
        /// </summary>
        /// <param name="notebook">Ежедневник</param>
        /// <param name="pathNB">Место хранения</param>
        static void ProgramCycle(Notebook notebook, string pathNB)
        {
            bool Flag = true;
            while (Flag)
            {
                int choice = Greetings();
                int index;
                switch (choice)
                {
                    case (1): Console.Clear();  notebook.CreateNotes(); break;
                    case (2):
                        Console.Clear();
                        Console.WriteLine("Введите путь к файлу");
                        string PathLoad = Console.ReadLine();
                        choice = CheckChoiceLoad();

                        if (choice == 1) notebook.Load(PathLoad);

                        if (choice == 2)
                        {
                            DateTime datetime = CheckDate();
                            notebook.Load(PathLoad, datetime);
                        }
                        break;
                    case (3): Console.Clear(); notebook.Save(); break;
                    case (4):
                        Console.Clear();
                        Console.WriteLine("Введите индекс записи, которую хотите отредактировать");
                        index = notebook.CheckIndex();

                        Console.WriteLine("Введите поле, которое хотите отредактировать (Заголовок, Важность, Место, Выполненность)");
                        notebook.EditNotes(Console.ReadLine(), index);
                        break;
                    case (5):
                        Console.Clear();
                        Console.WriteLine("Введите индекс записи, которую хотите удалить");
                        index = notebook.CheckIndex();
                        notebook.DeleteNotes(index);
                        break;
                    case (6):
                        Console.Clear();
                        Console.WriteLine("Введите поле, по которому хотите отсортировать (Заголовок, Важность, Дата создания, Место, Выполненность)");
                        notebook.SortNotes(Console.ReadLine());
                        break;
                    case (7): notebook.PrintNbToConsole(); Console.ReadLine(); break;
                    case (8): Flag = false; break;
                    default: break;
                }
            }
        }
        static void Main(string[] args)
        {
            Notebook notebook = CreateNB(out string pathNB);
            ProgramCycle(notebook, pathNB);
        }
    }
}