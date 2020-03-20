using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rambler.WebApi.Cinema.Dto
{
    /// <summary>
    /// DTO-класс для создания заказа
    /// </summary>
    public class OrderDto
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        [Required]
        public int UserId { get; set; }
        
        /// <summary>
        /// Id статуса заказа
        /// </summary>
        public int OrderStatusId { get; set; }
        
        /// <summary>
        /// Коллекция id сеансов
        /// </summary>
        [Required]
        public ICollection<int> OrderSessionsIds { get; set; }
    }
}