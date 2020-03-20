namespace Rambler.WebApi.Cinema.Models
{
    /// <summary>
    /// Модель для реализации отношения Many-to-Many между заказами и сеансами
    /// </summary>
    public class OrderSession
    {
        /// <summary>
        /// Id связки заказа с сеансом
        /// </summary>
        public int Id { get; set; }
        
        public Order Order { get; set; }
        
        public Session Session { get; set; }
    }
}