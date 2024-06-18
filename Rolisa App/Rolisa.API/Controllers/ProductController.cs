using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rolisa.API.Service;
using Rolisa.DataAccess;
using Rolisa.DataModel;
using Rolisa.ViewModel;

namespace Rolisa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private VMResponse response = new VMResponse();
        private ProductService product;
        public ProductController(ProductService _product)
        {
            product = _product;
        }
        [HttpGet("[action]/{filter?}")]
        public VMResponse GetByFilter(string filter) => product.GetByFilter(filter);
        [HttpGet]
        public VMResponse GetAll() => product.GetAll();
        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id) => product.GetById(id);
        [HttpPut]
        public VMResponse Edit(VMProduct data) => product.Update(data);
        [HttpPost]
        public VMResponse Add(VMProduct data) => product.Create(data);
        [HttpDelete]
        public VMResponse Delete(int id, int userId) => product.Delete(id, userId);
    }
}
