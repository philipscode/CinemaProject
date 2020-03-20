using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rambler.WebApi.Cinema.Models;

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

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="context">Объект для взаимодействия с БД</param>
        public SeedController(CinemaContext context)
        {
            _context = context;
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
                _context.Sessions.Add(new Session
                {
                    DateStart = DateTime.Now, 
                    Price = 500, 
                    Film = new Film
                    {
                        Name = "Восемь с половиной", 
                        Duration = new TimeSpan(0, 2, 0, 0)
                    },
                    Hall = new Hall
                    {
                        SeatNumber = 200,
                        SessionType = new SessionType
                        {
                            Type = "2D"
                        }
                    }
                });
                
                _context.Sessions.Add(new Session
                {
                    DateStart = DateTime.Now.AddDays(1), 
                    Price = 300,
                    Film = new Film
                    {
                        Name = "Фотоувеличение",
                        Duration = new TimeSpan(0, 1, 30, 0)
                    },
                    Hall = new Hall
                    {
                        SeatNumber = 100,
                        SessionType = new SessionType
                        {
                            Type = "IMAX"
                        }
                    }
                });
                
                await _context.SaveChangesAsync();

                return StatusCode(200);
            }
            catch (Exception e)
            {
                // log exception
                return StatusCode(500);
            }
        }
    }
}