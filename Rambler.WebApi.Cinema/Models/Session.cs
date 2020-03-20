using System;
using System.Collections.Generic;

namespace Rambler.WebApi.Cinema.Models
{
    /// <summary>
    /// Модель для представления сеанса
    /// </summary>
    public class Session
    {
        /// <summary>
        /// Id сеанса
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Дата и время сеанса
        /// </summary>
        public DateTime DateStart { get; set; }
        
        /// <summary>
        /// Цена билета на сеанс
        /// </summary>
        public int Price { get; set; }
        
        /// <summary>
        /// Количество свободных мест
        /// </summary>
        public int FreeSeats { get; set; }

        /// <summary>
        /// Кинозал, в котором проходит сеанс
        /// </summary>
        public Hall Hall { get; set; }
        
        /// <summary>
        /// Фильм, который демонстрируется на данном сеансе
        /// </summary>
        public Film Film { get; set; }
        
        /// <summary>
        /// Коллекция заказов на данный сеанс
        /// </summary>
        public ICollection<OrderSession> OrderSessions { get; set; }
    }
}