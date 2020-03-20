namespace Rambler.WebApi.Cinema.Dto
{
    /// <summary>
    /// DTO-класс для типов сеансов
    /// </summary>
    public class SessionTypeDto
    {
        /// <summary>
        /// Id типа сеанса
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Наименование типа сеанса
        /// </summary>
        public string Type { get; set; }
    }
}