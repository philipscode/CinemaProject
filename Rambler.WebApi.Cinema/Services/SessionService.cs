using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rambler.WebApi.Cinema.Models;
using Serilog;

namespace Rambler.WebApi.Cinema.Services
{
    /// <summary>
    /// Сервис для работы с сеансами
    /// </summary>
    public class SessionService
    {
        private readonly CinemaContext _context;
        private readonly ILogger _logger;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="context">Объект для работы с БД</param>
        /// <param name="logger">Объект для логирования</param>
        public SessionService(CinemaContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Список сеансов с информацией о фильмах с кинозалами
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        public async Task<IEnumerable<Session>> GetSessionList()
        {
            try
            {
                var result = await _context.Sessions
                    .Include(s => s.Film)
                    .Include(s => s.Hall)
                    .Include(s => s.Hall.SessionType)
                    .ToListAsync();
                
                return result;
            }
            catch (Exception e)
            {
                _logger.Error($"Ошибка во время получения списка сеансов. {e.Message}");
                return null;
            }
        }
    }
}