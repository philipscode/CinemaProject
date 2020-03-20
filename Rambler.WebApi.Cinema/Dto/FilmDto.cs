using System;
using System.Collections.Generic;
using Rambler.WebApi.Cinema.Models;

namespace Rambler.WebApi.Cinema.Dto
{
    /// <summary>
    /// DTO-класс для фильмов
    /// </summary>
    public class FilmDto
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
    }
}