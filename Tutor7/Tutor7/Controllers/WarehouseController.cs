using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Tutor7.Models;
using Tutor7.Services;

namespace Tutor7.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly IService _service;

    public WarehouseController(IService service)
    {
        _service = service;
        //
        //
    }
    

    [HttpPost]
    public IActionResult AddProductWarehouse(ProductWarehouseDTO productWarehouseDto)
    {
        return Ok(_service.create(productWarehouseDto));
        
    }
}