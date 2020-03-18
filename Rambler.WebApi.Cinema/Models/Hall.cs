using System.Collections.Generic;

namespace Rambler.WebApi.Cinema.Models
{
    /// <summary>
    /// Модель для представления кинозала
    /// </summary>
    public class Hall
    {
        /// <summary>
        /// Id кинозала
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Количество мест в кинозале
        /// </summary>
        public int SeatNumber { get; set; }

        /// <summary>
        /// Тип сеансов, которые можно демонстрировать в данном кинозале
        /// </summary>
        public SessionType SessionType { get; set; }
        
        /// <summary>
        /// Коллекция сеансов в данном кинозале
        /// </summary>
        public ICollection<Session> Sessions { get; set; }
    }
}