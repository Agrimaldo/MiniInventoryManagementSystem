using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniInventoryManagementSystem.Domain.Dto;
using MiniInventoryManagementSystem.Domain.Interfaces;

namespace MiniInventoryManagementSystem.API.Controllers
{
    [ApiController, Route("api/[controller]"), EnableCors("General")]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public bool Create([FromBody] OrderInput input)
        {
            return _orderService.Create(input);
        }

        [HttpPut]
        public bool Update([FromBody] OrderInput input)
        {
            return _orderService.Update(input);
        }

        [HttpDelete, Route("{id?}")]
        public bool Delete([FromRoute] int id)
        {
            return _orderService.Cancel(id);
        }

        [HttpDelete, Route("Item/{id?}")]
        public bool DeleteItem([FromRoute] int id)
        {
            return _orderService.CancelItem(id);
        }

    }
}
