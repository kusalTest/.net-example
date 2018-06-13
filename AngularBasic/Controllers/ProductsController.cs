using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AngularBasic.Data;
using Microsoft.Extensions.Logging;
using AngularBasic.Data.Entities;

namespace AngularBasic.Controllers
{
    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {
        private readonly IAngularBasicRepository _angularBasicRepository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IAngularBasicRepository angularBasicRepository, ILogger<ProductsController> logger)
        {
            _angularBasicRepository = angularBasicRepository;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //throw new Exception();
                return Ok(_angularBasicRepository.GetAllProducts());
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to get products");
            }
            
        } 
    }
}