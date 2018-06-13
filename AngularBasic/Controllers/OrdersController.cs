using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AngularBasic.Data;
using AngularBasic.ViewModel;
using AngularBasic.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace AngularBasic.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly IAngularBasicRepository _angularBasicRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;

        public OrdersController(IAngularBasicRepository angularBasicRepository, IMapper mapper, UserManager<StoreUser> userManager)
        {
            _angularBasicRepository = angularBasicRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //return Ok(_angularBasicRepository.GetAllOrders());
                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(_angularBasicRepository.GetAllOrders()));
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            try
            {
                var order = _angularBasicRepository.GetOrderById(User.Identity.Name, orderId);
                if (order != null)
                {
                    var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
                    if (item != null)
                    {
                        return Ok(_mapper.Map<Order, OrderViewModel>(order));
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("{includeItems:bool}")]
        public IActionResult Get(bool includeItems = true)
        {
            try
            {
                var username = User.Identity.Name;

                var result = _angularBasicRepository.GetAllOrdersByUser(username, includeItems);
                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(_angularBasicRepository.GetAllOrders()));
            
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to get orders");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = new Order()
                    {
                        OrderDate = model.OrderDate,
                        OrderNumber = model.OrderNumber,
                        Id = model.Id
                    };

                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    newOrder.User = currentUser;

                    //_angularBasicRepository.AddOrder(newOrder);
                    _angularBasicRepository.AddEntity(newOrder);

                    if (_angularBasicRepository.SaveAll())
                    {
                       
                        return Created($"/api/orders/{newOrder.Id}", _mapper.Map<Order, OrderViewModel>(newOrder));
                    }
                }
                
                return BadRequest(ModelState);
                
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to get orders");
            }
        }
    }
}