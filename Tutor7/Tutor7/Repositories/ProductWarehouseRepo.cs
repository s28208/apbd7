using Microsoft.Data.SqlClient;
using Tutor7.Models;
using Tutor7.Models;

namespace Tutor7.Repositories;

public class ProductWarehouseRepo : IProductWarehouseRepo
{
    private readonly IConfiguration _configuration;
    public ProductWarehouseRepo(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    
    public void updateOrder(int idOrder, DateTime createdAT)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "UPDATE Order SET FulfilledAt = @createdAt WHERE IdOrder=@idOrder";
        command.Parameters.AddWithValue("@createdAt", createdAT);
        command.Parameters.AddWithValue("@idOrder", idOrder);
        var dr = command.ExecuteNonQuery();
    }
    
    public int AddWarehouseProductAndUpdateOrder(ProductWarehouse productWarehouse)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "Insert into Product_Warehouse VALUES (@idWarehouse, @idProduct, @idOrder, @amount, @price, @createdAt)";
        command.Parameters.AddWithValue("@idWarehouse", productWarehouse.IdWarehouse);
        command.Parameters.AddWithValue("@idProduct", productWarehouse.IdProduct);
        command.Parameters.AddWithValue("@idOrder", productWarehouse.IdOrder);
        command.Parameters.AddWithValue("@amount", productWarehouse.Amount);
        command.Parameters.AddWithValue("@price", productWarehouse.Price);
        command.Parameters.AddWithValue("@createdAt", productWarehouse.CreatedAt); 
        command.ExecuteNonQuery();
        updateOrder(productWarehouse.IdOrder,productWarehouse.CreatedAt);
        command.CommandText = "Select IdProductWarehouse from ProductWarehouse where IdWarehouse = @idWarehouse and IdProduct = @idProduct and IdOrder = @idOrder)";
        command.Parameters.AddWithValue("@idWarehouse", productWarehouse.IdWarehouse);
        command.Parameters.AddWithValue("@idProduct", productWarehouse.IdProduct);
        command.Parameters.AddWithValue("@idOrder", productWarehouse.IdOrder);
        int idNew =      (int)command.ExecuteScalar();

        
        
        
        return idNew;
    }
    
    
    
    public double getPriceFromProduct(int idProduct)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "Select Price from Product where IdProduct = @idProduct";
        command.Parameters.AddWithValue("@IdProduct", idProduct);

        var dr = command.ExecuteReader();
        if (!dr.Read())
        {
            return 0;
        }
        var price = dr.GetDouble(dr.GetOrdinal("Price")); 
        
        return price;
    }
    public bool checkProduct(ProductWarehouseDTO productWarehouseDto)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT Count(*) FROM Product where IdProduct = @idProduct ";
        command.Parameters.AddWithValue("@idProduct", productWarehouseDto.IdProduct);
        int countRowProduct = (int)command.ExecuteScalar();
        if (countRowProduct == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int getIdOrder(ProductWarehouseDTO productWarehouseDto)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText =
            "SELECT IdOrder FROM Order where IdProduct = @idProduct and Amount = @amount and FulfilledAt is null ";
        command.Parameters.AddWithValue("@idProduct", productWarehouseDto.IdProduct);
        command.Parameters.AddWithValue("@amount", productWarehouseDto.Amount);
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int indexidOrder = reader.GetOrdinal("IdOrder");
            int idOrder = reader.GetInt32(indexidOrder);
            /*int idProduct = reader.GetOrdinal("IdProduct");
            int amount = reader.GetOrdinal("Amount");
            int createdAt = reader.GetOrdinal("CreatedAt");
            Order order = new Order(){IdOrder = reader.GetInt32(idOrder),
                IdProduct = reader.GetInt32(idProduct),
                Amount = reader.GetInt32(amount),
                CreatedAt = reader.GetDateTime(createdAt)
            };*/
            return idOrder;
        }

        return -1;
    }

    public bool checkIdOrderInProductWarehouse(int idOrder)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT Count(*) FROM Product_Warehouse where IdOrder = @idOrder ";
        command.Parameters.AddWithValue("@idOrder", idOrder);
        int countRowOrder = (int)command.ExecuteScalar();
        if (countRowOrder == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool checkWarehouse(ProductWarehouseDTO productWarehouseDto)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT Count(*) FROM Warehouse where IdWarehouse = @idWarehouse ";
        command.Parameters.AddWithValue("@idWarehouse", productWarehouseDto.IdWarehouse);
        int countRowWarehouse = (int)command.ExecuteScalar();
        if (countRowWarehouse == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool checkAmount(ProductWarehouseDTO productWarehouseDto)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        if (productWarehouseDto.Amount >0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
}