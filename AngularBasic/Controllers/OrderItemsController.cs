using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AngularBasic.Data;
using AutoMapper;
using AngularBasic.Data.Entities;
using AngularBasic.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AngularBasic.Controllers
{
    [Route("/api/orders/{orderid}/items")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemsController : Controller
    {
        private readonly IAngularBasicRepository _angularBasicRepository;
        private readonly IMapper _mapper;

        public OrderItemsController(IAngularBasicRepository angularBasicRepository,
            IMapper mapper)
        {
            _angularBasicRepository = angularBasicRepository;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = _angularBasicRepository.GetOrderById(User.Identity.Name, orderId);
            if (order != null) return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var order = _angularBasicRepository.GetOrderById(User.Identity.Name, orderId);
            if (order != null)
            {
                var item = order.Items.Where(i => i.Id == id).FirstOrDefault();

                if (item != null)
                {
                    return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
                }
                
            }
            return NotFound();
        }
    }
}