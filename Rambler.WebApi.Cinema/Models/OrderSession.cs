namespace Rambler.WebApi.Cinema.Models
{
    /// <summary>
    /// Модель для реализации отношения Many-to-Many между заказами и сеансами
    /// </summary>
    public class OrderSession
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        
        public int SessionId { get; set; }
        public Session Session { get; set; }
    }
}