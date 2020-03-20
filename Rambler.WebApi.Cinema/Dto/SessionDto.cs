using System;

namespace Rambler.WebApi.Cinema.Dto
{
    /// <summary>
    /// DTO-класс для сеансов
    /// </summary>
    public class SessionDto
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
        /// Кинозал, в котором проходит сеанс
        /// </summary>
        public HallDto Hall { get; set; }
        
        /// <summary>
        /// Фильм, который демонстрируется на данном сеансе
        /// </summary>
        public FilmDto Film { get; set; }
    }
}