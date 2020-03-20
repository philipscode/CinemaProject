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
        public const string OrderCreated = "Создан";
        public const string OrderPayed = "Оплачен";
        public const string OrderDeleted = "Удален";
        
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
                
                OrderStatus orderStatus = await _context
                    .OrderStatuses
                    .FirstOrDefaultAsync(o => o.Status == OrderCreated);

                if (orderStatus == null)
                {
                    return false;
                }
                
                Order order = new Order
                {
                    ModifyDate = DateTime.Now,
                    OrderStatus = orderStatus,
                    User = user
                };

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
                Console.Write($"{e.Message} {e.StackTrace}");
                return false;
            }
        }

        /// <summary>
        /// Обновление статуса заказа
        /// </summary>
        /// <param name="orderId">Id заказа</param>
        /// <param name="newStatus">Статус заказа</param>
        /// <returns>Статус выполнения операции</returns>
        public async Task<bool> UpdateOrderStatus(int orderId, string newStatus)
        {
            try
            {
                Order order = await _context
                    .Orders
                    .FirstOrDefaultAsync(o => o.Id == orderId);

                if (order == null)
                {
                    return false;
                }

                OrderStatus status = await _context
                    .OrderStatuses
                    .FirstOrDefaultAsync(o => o.Status == newStatus);

                if (status == null)
                {
                    return false;
                }

                order.OrderStatus = status;

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