using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rambler.WebApi.Cinema.Models;
using Serilog;

namespace Rambler.WebApi.Cinema.Controllers
{
    /// <summary>
    /// Контроллер для заполнения БД тестовыми данными
    /// </summary>
    [ApiController]
    [Route("/admin/[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly CinemaContext _context;
        private readonly ILogger _logger;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="context">Объект для взаимодействия с БД</param>
        /// <param name="logger">Объект для логирования</param>
        public SeedController(CinemaContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        
        /// <summary>
        /// Заполнения БД тестовыми данными
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> SeedDatabase()
        {
            try
            {
                _context.Films.Add(new Film
                {
                    Name = "Восемь с половиной",
                    Duration = new TimeSpan(0, 2, 0, 0)
                });
                
                _context.Films.Add(new Film
                {
                    Name = "Ночь на земле",
                    Duration = new TimeSpan(0, 2, 15, 0)
                });
                
                _context.Films.Add(new Film
                {
                    Name = "Фотоувеличение",
                    Duration = new TimeSpan(0, 1, 30, 0)
                });

                _context.Halls.Add(new Hall
                {
                    SeatNumber = 200,
                    SessionType = new SessionType
                    {
                        Type = "2D"
                    }
                });
                
                _context.Halls.Add(new Hall
                {
                    SeatNumber = 150,
                    SessionType = new SessionType
                    {
                        Type = "3D"
                    }
                });
                
                _context.Halls.Add(new Hall
                {
                    SeatNumber = 100,
                    SessionType = new SessionType
                    {
                        Type = "IMAX"
                    }
                });
                
                await _context.SaveChangesAsync();
                
                DateTime dateMorning = DateTime.Today.AddHours(10);
                DateTime dateEvening = DateTime.Today.AddHours(19);

                for (int i = 1; i < 15; i++)
                {
                    _context.Sessions.Add(new Session
                    {
                        DateStart = dateMorning, 
                        Price = 100,
                        FreeSeats = 200,
                        Film = _context.Films.First(f => f.Name == "Восемь с половиной"),
                        Hall = _context.Halls.First(h => h.SessionType.Type == "2D")
                    });
                    
                    _context.Sessions.Add(new Session
                    {
                        DateStart = dateMorning, 
                        Price = 100,
                        FreeSeats = 150,
                        Film = _context.Films.First(f => f.Name == "Ночь на земле"),
                        Hall = _context.Halls.First(h => h.SessionType.Type == "3D")
                    });
                
                    _context.Sessions.Add(new Session
                    {
                        DateStart = dateMorning, 
                        Price = 100,
                        FreeSeats = 100,
                        Film = _context.Films.First(f => f.Name == "Фотоувеличение"),
                        Hall = _context.Halls.First(h => h.SessionType.Type == "IMAX")
                    });
                    
                    _context.Sessions.Add(new Session
                    {
                        DateStart = dateEvening, 
                        Price = 200,
                        FreeSeats = 200,
                        Film = _context.Films.First(f => f.Name == "Восемь с половиной"),
                        Hall = _context.Halls.First(h => h.SessionType.Type == "2D")
                    });
                    
                    _context.Sessions.Add(new Session
                    {
                        DateStart = dateEvening, 
                        Price = 200,
                        FreeSeats = 100,
                        Film = _context.Films.First(f => f.Name == "Ночь на земле"),
                        Hall = _context.Halls.First(h => h.SessionType.Type == "3D")
                    });
                
                    _context.Sessions.Add(new Session
                    {
                        DateStart = dateEvening, 
                        Price = 200,
                        FreeSeats = 100,
                        Film = _context.Films.First(f => f.Name == "Фотоувеличение"),
                        Hall = _context.Halls.First(h => h.SessionType.Type == "IMAX")
                    });

                    dateMorning = dateMorning.AddDays(1);
                    dateEvening = dateEvening.AddDays(1);
                }

                _context.OrderStatuses.Add(new OrderStatus
                {
                    Status = "Создан"
                });
                
                _context.OrderStatuses.Add(new OrderStatus
                {
                    Status = "Оплачен"
                });
                
                _context.OrderStatuses.Add(new OrderStatus
                {
                    Status = "Удален"
                });

                _context.Users.Add(new User
                {
                    Name = "Федерико Феллини",
                    Email = "felline@rambler.ru"
                });
                
                _context.Users.Add(new User
                {
                    Name = "Микеланджело Антониони",
                    Email = "antonioni@rambler.ru"
                });
                
                await _context.SaveChangesAsync();

                return StatusCode(200);
            }
            catch (Exception e)
            {
                _logger.Error($"Ошибка во время заполнения БД. {e.Message}");
                return StatusCode(500);
            }
        }
    }
}