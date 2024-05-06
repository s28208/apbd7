using Tutor7.Models;
using Tutor7.Repositories;
namespace Tutor7.Services;

public class Servise : IService
{
    private readonly IProductWarehouseRepo _productWarehouseRepo;
    public Servise(IProductWarehouseRepo productWarehouseRepo)
    {
        _productWarehouseRepo = productWarehouseRepo;
    }
     

    public bool checkAllCategory(ProductWarehouseDTO productWarehouseDto)
    { 
        return (_productWarehouseRepo.checkProduct(productWarehouseDto) &&
            _productWarehouseRepo.checkWarehouse(productWarehouseDto) &&
                _productWarehouseRepo.checkAmount(productWarehouseDto))?  true: false;
    }

    public int create(ProductWarehouseDTO productWarehouseDto)
    {
        if(checkAllCategory(productWarehouseDto))
        {
            int idOrderM = _productWarehouseRepo.getIdOrder(productWarehouseDto);
            if (idOrderM != -1)
            {
                if (!_productWarehouseRepo.checkIdOrderInProductWarehouse(idOrderM))
                {
                    double priceOfProduct = _productWarehouseRepo.getPriceFromProduct(productWarehouseDto.IdProduct);
                    ProductWarehouse productWarehouse = new ProductWarehouse()
                    {
                        IdProduct = productWarehouseDto.IdProduct,
                        IdWarehouse = productWarehouseDto.IdWarehouse,
                        IdOrder = idOrderM,
                        Amount = productWarehouseDto.Amount,
                        Price = productWarehouseDto.Amount * priceOfProduct,
                        CreatedAt = productWarehouseDto.CreatedAt
                    };
                    return _productWarehouseRepo.AddWarehouseProductAndUpdateOrder(productWarehouse);
                }
                else
                {
                    throw new Exception("Order already exist");

                }
            }
            else
            {
                throw new Exception("Order does not yet exist");
            }
        }
        else
        {
            throw new Exception("Bad value");
        }
    }
    
}