using Tutor7.Models;

namespace Tutor7.Repositories;

public interface IProductWarehouseRepo
{
    public double getPriceFromProduct(int idProduct);
    public bool checkIdOrderInProductWarehouse(int idOrder);
    public int AddWarehouseProductAndUpdateOrder(ProductWarehouse productWarehouse);
    public void updateOrder(int idOrder, DateTime createdAT);
    public int getIdOrder(ProductWarehouseDTO productWarehouseDto);
    public bool checkProduct(ProductWarehouseDTO productWarehouseDto);
    
    public bool checkWarehouse(ProductWarehouseDTO productWarehouseDto);
    
    public bool checkAmount(ProductWarehouseDTO productWarehouseDto);
    
}