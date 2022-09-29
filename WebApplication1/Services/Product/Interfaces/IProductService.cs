using Auth.DBContext;
using Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services.Product.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDetails>> GetProductList();
        Task<IEnumerable<ProductDetails>> GetProductListByMaterId(int id);
        Task<IEnumerable<ProductMasters>> GetProductMasterList();
        Task<int> SaveProductMaster(ProductMasters item);
        Task<int> SaveProductDetails(ProductViewModel items);
        Task<int> SaveProductDetailList(List<ProductDetails> model);
        Task<bool> DeleteProductMaster(int id);
        Task<bool> DeleteProductDetail(int id);
        Task<bool> DeleteProductDetailsMyMasterId(int id);
    }
}
