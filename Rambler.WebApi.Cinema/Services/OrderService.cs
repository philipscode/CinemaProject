using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rambler.WebApi.Cinema.Dto;
using Rambler.WebApi.Cinema.Models;

namespace Rambler.WebApi.Cinema.Services
{
    /// <summary>
    /// Сервис для работы с заказами
    /// </summary>
    public class OrderService
    {
        public const string ORDER_CREATED = "Создан";
        public const string ORDER_PAYED = "Оплачен";
        public const string ORDER_DELETED = "Удален";
        
        private readonly CinemaContext _context;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="context">Объект для взаимодействия с БД</param>
        public OrderService(CinemaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Процессинг нового заказа
        /// </summary>
        /// <param name="orderDto">DTO-объект с информацией по заказу</param>
        /// <returns>Статус обработки заказа</returns>
        public async Task<bool> ProcessPlacedOrder(OrderDto orderDto)
        {
            try
            {
                User user = await _context
                    .Users
                    .FirstOrDefaultAsync(u => u.Id == orderDto.UserId);

                if (user == null)
                {
                    return false;
                }

                foreach (var sessionId in orderDto.SessionIds)
                {
                    Session session = await _context
                        .Sessions
                        .Include(s => s.OrderSessions)
                        .FirstOrDefaultAsync(s => s.Id == sessionId);

                    if (session == null || session.FreeSeats <= 0)
                    {
                        continue;
                    }

                    OrderStatus orderStatus = await _context
                        .OrderStatuses
                        .FirstOrDefaultAsync(o => o.Status == ORDER_CREATED);

                    if (orderStatus == null)
                    {
                        continue;
                    }

                    Order order = new Order
                    {
                        ModifyDate = DateTime.Now,
                        OrderStatus = orderStatus,
                        User = user
                    };
                    
                    session.OrderSessions.Add(new OrderSession
                    {
                        Order = order,
                        Session = session
                    });

                    session.FreeSeats--;
                }

                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception e)
            {
                // log exception
                return false;
            }
        }
    }
}