using System.Collections.Generic;

namespace Rambler.WebApi.Cinema.Models
{
    /// <summary>
    /// Модель для представления статусов заказов
    /// </summary>
    public class OrderStatus
    {
        /// <summary>
        /// Id статуса
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Наименование статуса
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// Коллекция заказов
        /// </summary>
        public ICollection<Order> Orders { get; set; }
    }
}