using Auth.DBContext;
using Auth.Model;
using Auth.Services.Product.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services.Product
{
    public class ProductService: IProductService
    {
        private readonly ApplicationDBContext _context;

        public ProductService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteProductDetail(int id)
        {
            _context.products.Remove(_context.products.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteProductDetailsMyMasterId(int id)
        {
            var list = await _context.products.Where(x => x.productMasterId == id).AsNoTracking().ToListAsync();
            _context.products.RemoveRange(list);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteProductMaster(int id)
        {
            _context.productMasters.Remove(_context.productMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductDetails>> GetProductList()
        {
            return await _context.products.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ProductDetails>> GetProductListByMaterId(int id)
        {
            return await _context.products.Where(x=>x.productMasterId==id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ProductMasters>> GetProductMasterList()
        {
            return await _context.productMasters.AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveProductDetailList(List<ProductDetails> model)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                foreach (var item in model)
                {
                    if (item.id != 0)
                        _context.products.Update(item);
                    else
                        _context.products.Add(item);
                    await _context.SaveChangesAsync();
                }
                transaction.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return 1;
            }
        }
        public async Task<int> SaveProductDetails(ProductViewModel items)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                for (int i = 0; i < items.ids.Length; i++)
                {
                    var item = new ProductDetails
                    {
                        id = items.ids[i],
                        productName = items.names[i],
                        productMasterId = items.masterId,
                        expireDate = items.dates[i]
                    };
                    if (item.id != 0)
                        _context.products.Update(item);
                    else
                        _context.products.Add(item);
                    await _context.SaveChangesAsync();
                }
                transaction.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return 1;
            }
        }

        public async Task<int> SaveProductMaster(ProductMasters item)
        {
            try
            {
                if (item.id != 0)
                    _context.productMasters.Update(item);
                else
                    _context.productMasters.Add(item);
                await _context.SaveChangesAsync();
                return item.id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
