using System.ComponentModel.DataAnnotations;

namespace Tutor7.Models;

public class ProductWarehouseDTO
{
    [Required] public int IdProduct { get; set; }
    [Required] public int IdWarehouse { get; set; }
    [Required] public int Amount { get; set; }
    [Required] public DateTime CreatedAt { get; set; }
}