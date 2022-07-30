using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Homework_07_01
{   /// <summary>
    /// Структура для работы с ежедневником
    /// </summary>
    struct Notebook
    {
        #region Поля
        public Note[] note;     //Массив записей
        
        private string path;    //Место хранения ежедневника
        
        private int index;      //Индекс записей
        
        string[] titles;        //Массив заголовка для вывода на экран
        #endregion

        #region Конструктор
        /// <summary>
        /// Конструктор объявления ежедневника
        /// </summary>
        /// <param name="Path">Путь к ежедневнику</param>
        public Notebook(string Path)
        {
            this.path = Path;           //Путь к ежедневнику
            this.index = 0;             //Начальный индекс
            this.titles =new string[5] {"Заголовок","Важность","Время возникновения","Место","Отметка о выполнении"};//Заполнение массива заголовка
            this.note = new Note[1];    //Инициализация массива записей
        }
        #endregion

        #region Методы
        /// <summary>
        /// Метод, создающий запись о событии
        /// </summary>
        public void CreateNotes()
        {
            Console.WriteLine("Заполните поля события: \n" +
                            "Заголовок");
            string Heading = Console.ReadLine();

            Console.WriteLine("Важность");
            string Priority = Console.ReadLine();

            Console.WriteLine("Место");
            string Locate = Console.ReadLine();
            Note ConcreteNote = new Note(Heading, Priority, Locate);    //Создание записи
            AddNotes(ConcreteNote);                                     //Добавление записи
        }
        /// <summary>
        /// Метод увелечения массива записей
        /// </summary>
        /// <param name="Flag">Флаг метода</param>
        private void Resize(bool Flag)
        {
            if(Flag)
            {
                Array.Resize(ref this.note, this.note.Length + 1);  //Увеличение размера массива записей на еденицу
            }
        }
        /// <summary>
        /// Метод добавления записи в ежедневник
        /// </summary>
        /// <param name="ConcreteNote">Запись</param>
        public void AddNotes(Note ConcreteNote)
        {
            Resize(this.index >= this.note.Length); //Изменение размера массива
            this.note[this.index] = ConcreteNote;   //Добавление записи
            this.index++;                           //Увеличениие индекса
        }
        /// <summary>
        /// Метод добавления массива записей в ежедневник
        /// </summary>
        /// <param name="ArrayNote">Массив записей</param>
        public void AddNotes(Note[] ArrayNote)
        {
            int i = 0;

            while(i < ArrayNote.Length)                 //Цикл добавления
            {
                Resize(this.index >= this.note.Length); //Изменение размера
                this.note[this.index] = ArrayNote[i];   //Добаление элемента массива
                i += 1;
                this.index++;                           //Увеличение индекса
            }
        }
        /// <summary>
        /// Метод, удаляющий запись под конкретным индексом из массива
        /// </summary>
        /// <param name="index">Индекс записи</param>
        public void DeleteNotes(int Index)
        {
            Array.Clear(this.note, index - 1, 1);           //Удаление одной записи из массива под конкретным индексом

            for (int i = Index - 1; i < this.index - 1; i++)//Сдвиг записей назад на еденицу
                this.note[i] = this.note[i + 1];

            this.index--;                                   //Уменьшение индекса
        }
        /// <summary>
        /// Метод, проверяющий наличие файла
        /// </summary>
        /// <returns>Наличие фала</returns>
        private bool CheckFile()
        {
            FileInfo fi = new FileInfo(path);

            if (!fi.Exists) fi.Create().Close();    //Создание файла в случае его отсутствия

            return fi.Exists;
        }
        /// <summary>
        /// Метод, проверяющий попадание введеного индекса диапазону массива ежедневника
        /// </summary>
        /// <returns>Индекс</returns>
        public int CheckIndex()
        {
            bool flag = false;
            int Index = 0;

            while(!flag)                                            //Цикл ввода и проверки
            {
                flag = true;
                flag = int.TryParse(Console.ReadLine(), out Index); //Проверка введеного значения

                if (Index > this.index) flag = false;               //Проверка попадания в диапазон

                if (!flag)
                {
                    Console.Clear();
                    Console.WriteLine("Введите индекс из диапазона индекса записей!");
                }
            }
            return index;
        }
        /// <summary>
        /// Метод загрузки ежедневника из файла
        /// </summary>
        public void Load(string Path)
        {
            if (CheckFile()) using (StreamReader sr = new StreamReader(Path))   //Открытие потока на чтение в случае присутствия файла
                {
                    while (!sr.EndOfStream)
                    {
                        string[] args = sr.ReadLine().Split(',');               //Разделение строки на элементы строкового массива

                        if (args.Length < 5) break;                             //Условие наличия достаточного количества элементво в строке

                        AddNotes(new Note(args[0], args[1], args[3]));          //Добавление новой записи
                        this.note[this.index - 1].Done = Convert.ToBoolean(args[4]);//Добавление в запись пометки о выполнении
                    }
                }
        }
        /// <summary>
        /// Метод сохранения ежедневника в файл
        /// </summary>
        public void Save()
        {
            string str = "";
            int i = 0;

            while (i < this.index)                                                  //Цикл формирования текста
            {
                str = str + String.Format("{0,10},{1,9},{2,19},{3,9},{4,21}\r\n",   //Формирование текста из записей
                    note[i].Heading,
                    note[i].Priority,
                    note[i].DateTime,
                    note[i].Locate,
                    note[i].Done);
                i += 1;
            }
                using (StreamWriter sw = new StreamWriter(this.path))
                    sw.Write(str);                                                  //Запись текста в файл
        }
        /// <summary>
        /// Метод загрузки ежедневника из файла по дате
        /// </summary>
        /// <param name="datetime">Дата</param>
        public void Load(string Path, DateTime datetime)
        {
            if (CheckFile()) using (StreamReader sr = new StreamReader(Path))   //Проверка на наличие файла
                {
                    while (!sr.EndOfStream)                                     //Цикл построчного чтения
                    {
                        string[] args = sr.ReadLine().Split(',');               //формирования массива значений записи

                        if (args.Length < 5) break;                             //Условие формирования записи

                        if (Convert.ToDateTime(args[2]) == datetime)            //Условие совпадения даты создания записи указанной дате
                        {
                            AddNotes(new Note(args[0], args[1], args[3]));      //Добавление записи
                            this.note[this.index - 1].Done = Convert.ToBoolean(args[4]);//Добавление пометки о выполнении
                        }
                    }
                }
        }
        /// <summary>
        /// Метод, редактирующий конкретное поле конкретной записи в массиве
        /// </summary>
        /// <param name="str">Поле</param>
        /// <param name="index">Индекс записи</param>
        public void EditNotes (string str, int Index)
        {
            switch (str)                                    //Выбор поля редактирования
            {
                case "Заголовок":
                    {
                        string value = Console.ReadLine();
                        this.note[Index - 1].Heading = value;//Редактирование Заголовка
                        break; 
                    }
                case "Важность":
                    {
                        string value = Console.ReadLine();
                        this.note[Index - 1].Priority = value;//Редактирование Важности
                        break;
                    }
                case "Место":
                    {
                        string value = Console.ReadLine();
                        this.note[Index - 1].Locate = value;   //Редактирование Место
                        break;
                    }
                case "Выполненность":
                    {
                        bool value = Convert.ToBoolean(Console.ReadLine());
                        this.note[Index - 1].Done = value;     //Редактирование Выполненности
                        break;
                    }
                default: Console.WriteLine("Не найдено такое поле"); Console.ReadLine(); break;
            }
        }
        /// <summary>
        /// Метод, сортирующий массив структур по выбранному полю
        /// </summary>
        /// <param name="a">наименование поля</param>
        public void SortNotes(string str)
        {
            switch (str)
            {
                case "Заголовок": Array.Sort(this.note, (i, j) => i.Heading.CompareTo(j.Heading)); break;   //Сортировка по Заголовку

                case "Важность": Array.Sort(this.note, (i, j) => i.Priority.CompareTo(j.Priority)); break;  //Сортировка по Выжности

                case "Дата создания": Array.Sort(this.note, (i, j) => i.DateTime.CompareTo(j.DateTime)); break;//Сортировка по Дате создания

                case "Место": Array.Sort(this.note, (i, j) => i.Locate.CompareTo(j.Locate)); break;         //Сортировка по Месту

                case "Выполненность": Array.Sort(this.note, (i, j) => i.Done.CompareTo(j.Done)); break;     //Сортировка по Выполненности

                default: Console.WriteLine("Не найдено такое поле"); break;
            }
        }
        /// <summary>
        /// Метод, выводящий ежедневник в коносль
        /// </summary>
        public void PrintNbToConsole()
        {
            Console.WriteLine("{0,10}{1,10}{2,20}{3,10}{4,22}",     //Печать заголовка массива записей
                this.titles[0],
                this.titles[1],
                this.titles[2],
                this.titles[3],
                this.titles[4]);
            int i = 0;
            while(i < this.index)
            {
                Console.WriteLine("{0,10}{1,10}{2,20}{3,10}{4,22}", //Цикл печати записей
                    this.note[i].Heading,
                    this.note[i].Priority,
                    this.note[i].DateTime,
                    this.note[i].Locate,
                    this.note[i].Done);
                i += 1;
            }
        }
        #endregion

        #region Свойство
        public int Index { get{ return this.index; } private set { this.index = value; } }      //Свойство "Индекс записи"
        #endregion
    }
}