using System;
using System.Diagnostics.SymbolStore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rambler.WebApi.Cinema.Dto;
using Rambler.WebApi.Cinema.Services;

namespace Rambler.WebApi.Cinema.Controllers
{
    /// <summary>
    /// Контроллер для работы с заказами
    /// </summary>
    [ApiController]
    [Route("/api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _service;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="service">Сервис для работы с заказами</param>
        public OrderController(OrderService service)
        {
            _service = service;
        }
        
        /// <summary>
        /// Обработка созданного заказа
        /// </summary>
        /// <param name="orderDto">DTO-объект с информацией по заказу</param>
        /// <returns>HTTP-код статуса выполнения операции</returns>
        [HttpPost("place")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderDto orderDto)
        {
            bool isSuccessful = await _service.ProcessPlacedOrder(orderDto);

            if (isSuccessful)
            {
                return StatusCode(201);
            }

            return StatusCode(500);
        }

        /// <summary>
        /// Оплата существующего заказа
        /// </summary>
        /// <param name="orderId">Id заказа</param>
        /// <returns>HTTP-код статуса выполнения операции</returns>
        [HttpGet("pay/{orderId}")]
        public async Task<IActionResult> PayForTheOrder(int orderId)
        {
            bool isSuccessfull = await _service.PayForTheOrder(orderId);

            if (isSuccessfull)
            {
                return StatusCode(200);
            }

            return StatusCode(500);
        }
    }
}