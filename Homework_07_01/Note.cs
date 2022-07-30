using System;
using System.Collections.Generic;
using System.Text;

namespace Homework_07_01
{
    /// <summary>
    /// Структура записей событий
    /// </summary>
    struct Note
    {
        #region Поля
        private string heading;     //Поле "Заголовок события"
        
        private string priority;    //Поле "Степень важности события"
        
        private DateTime datetime;  //Поле "Время создания записи"
        
        private string locate;      //Поле "Место события"

        private bool done;          //Поле "Пометка о выполнении"


        #endregion

        #region Конструктор
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Heading">Заголовок записи</param>
        /// <param name="Priority">Приоритет</param>
        /// <param name="Locate">Место</param>
        public Note(string Heading, string Priority, string Locate)
        {
            this.heading = Heading;     //Заголовок
            this.priority = Priority;   //Важность
            this.datetime = DateTime.Now;//Текущее время
            this.locate = Locate;       //Место
            this.done = false;          //Отметка не выполненно
        }
        #endregion

        #region Метод
        /// <summary>
        /// Метод, печатающий информацию о событии
        /// </summary>
        /// <returns>Информация о событии</returns>
        public string Print()
        {
            return string.Format("{0,10}{1,10}{2,20}{3,10}{4,22}",
                heading,
                priority,
                datetime,
                locate,
                done);
        }
        #endregion

        #region Свойства
        public string Heading { get { return this.heading; } set { this.heading = value; } }    //Свойство "Заголовок события"
        
        public string Priority { get { return this.priority; } set { this.priority = value; } } // Свойство "Степень важности"
        
        public DateTime DateTime { get { return this.datetime; } private set { this.datetime = value; } }//Свойство "Время создания записи"
        
        public string Locate { get { return this.locate; } set { this.locate = value; } }       //Свойство "Место события"
        
        public bool Done { get { return this.done; } set { this.done = value; } }               //Пометка о выполнении
        #endregion
    }
}