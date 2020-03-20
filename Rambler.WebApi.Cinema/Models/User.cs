using System.Collections.Generic;

namespace Rambler.WebApi.Cinema.Models
{
    /// <summary>
    /// Модель для представления пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Email пользователя
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Коллекция заказов пользователя
        /// </summary>
        public ICollection<Order> Orders { get; set; }
    }
}