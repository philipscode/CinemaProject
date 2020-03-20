using System;
using System.Collections.Generic;

namespace Rambler.WebApi.Cinema.Models
{
    /// <summary>
    /// Модель для представления фильма
    /// </summary>
    public class Film
    {
        /// <summary>
        /// Id фильма
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Название фильма
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Длительность фильма
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Коллекция сеансов с данным фильмом
        /// </summary>
        public ICollection<Session> Sessions { get; set; }
    }
}