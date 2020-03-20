namespace Rambler.WebApi.Cinema.Dto
{
    /// <summary>
    /// DTO-класс для кинозалов
    /// </summary>
    public class HallDto
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
        public SessionTypeDto SessionType { get; set; }
    }
}