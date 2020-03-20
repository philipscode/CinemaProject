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
        private const string OrderCreated = "Создан";
        private const string OrderPayed = "Оплачен";
        private const string OrderDeleted = "Удален";
        
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
        /// Оплата существуюещго заказа
        /// </summary>
        /// <param name="id">Id заказа</param>
        /// <returns>Статус выполнения операции</returns>
        public async Task<bool> PayForTheOrder(int id)
        {
            try
            {
                Order order = await _context
                    .Orders
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (order == null)
                {
                    return false;
                }

                OrderStatus statusPayed = await _context
                    .OrderStatuses
                    .FirstOrDefaultAsync(o => o.Status == OrderPayed);

                if (statusPayed == null)
                {
                    return false;
                }

                order.OrderStatus = statusPayed;

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