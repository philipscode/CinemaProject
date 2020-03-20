using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rambler.WebApi.Cinema.Dto;
using Rambler.WebApi.Cinema.Services;

namespace Rambler.WebApi.Cinema.Controllers
{
    /// <summary>
    /// Контроллер для работы с сеансами
    /// </summary>
    [ApiController]
    [Route("/api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SessionService _service;
        
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="mapper">Объект для маппинга</param>
        /// <param name="service">Сервис для работы с сеансами</param>
        public SessionController(IMapper mapper, SessionService service)
        {
            _mapper = mapper;
            _service = service;
        }
        
        /// <summary>
        /// Получение списка сеансов
        /// </summary>
        /// <returns>Список сеансов</returns>
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<SessionDto>>> GetSessionList()
        {
            var result = await _service.GetSessionList();

            if (result == null)
            {
                return StatusCode(500);
            }
            
            var resultDto = _mapper.Map<IEnumerable<SessionDto>>(result);
            
            return Ok(resultDto);
        }
    }
}