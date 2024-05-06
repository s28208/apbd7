using Tutor7.Models;

namespace Tutor7.Services;

public interface IService
{
    public bool checkAllCategory(ProductWarehouseDTO productWarehouseDto);
    public int create(ProductWarehouseDTO productWarehouseDto);
}