using Auth.DBContext;
using Auth.Model;
using Auth.Services.Product.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Auth.Areas
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        [Authorize(Roles ="General User")]
        public async Task<IActionResult> GetProductList()
        {
            try
            {
                var data = await productService.GetProductList();
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "General User")]
        public async Task<IActionResult> GetProductListMyMasterId(int id)
        {
            try
            {
                var data = await productService.GetProductListByMaterId(id);
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "General User")]
        public async Task<IActionResult> GetProductMaster()
        {
            try
            {
                var data = await productService.GetProductMasterList();
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "General User")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveUpdateProductMaster(ProductMasters model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var data = await productService.SaveProductMaster(model);
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "General User")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveUpdateProductDetails(ProductViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var data = await productService.SaveProductDetails(model);
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "General User")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveUpdateMasterProductDetails(List<ProductDetails> model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var data = await productService.SaveProductDetailList(model);
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "General User")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductMaster(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var data = await productService.DeleteProductMaster(id);
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "General User")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DdeleteProductDetailsByMasterId(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var data = await productService.DeleteProductDetailsMyMasterId(id);
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "General User")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DdeleteProductDetail(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var data = await productService.DeleteProductDetail(id);
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles ="General User")]
        [HttpGet]
        public async Task<IActionResult> GetProductlistByApi()
        {
            try
            {

                string url = String.Format("https://pqstec.com/invoiceapps/values/getproductlistall");

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
