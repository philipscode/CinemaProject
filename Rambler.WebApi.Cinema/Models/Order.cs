using System;
using System.Collections.Generic;

namespace Rambler.WebApi.Cinema.Models
{
    /// <summary>
    /// Класс для представления заказа
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Id заказа
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Дата изменения статуса
        /// </summary>
        public DateTime ModifyDate { get; set; }
        
        /// <summary>
        /// Статус заказа
        /// </summary>
        public OrderStatus OrderStatus { get; set; }
        
        /// <summary>
        /// Коллекция сеансов в данном заказе
        /// </summary>
        public ICollection<OrderSession> OrderSessions { get; set; }
    }
}