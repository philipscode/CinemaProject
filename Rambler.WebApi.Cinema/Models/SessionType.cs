using System.Collections.Generic;

namespace Rambler.WebApi.Cinema.Models
{
    /// <summary>
    /// Модель для представления типа сеанса (2D, 3D...)
    /// </summary>
    public class SessionType
    {
        /// <summary>
        /// Id типа сеанса
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Наименование типа сеанса
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// Коллекция кинозалов
        /// </summary>
        public ICollection<Hall> Halls { get; set; }
    }
}