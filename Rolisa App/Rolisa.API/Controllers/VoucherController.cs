using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rolisa.DataAccess;
using Rolisa.DataModel;
using Rolisa.ViewModel;

namespace Rolisa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private DAVoucher voucher;
        public VoucherController(RolisaContext _db) { voucher = new DAVoucher(_db); }
        [HttpGet]
        public VMResponse GetALl() => voucher.GetAll();
        [HttpGet("[action]/{filter?}")]
        public VMResponse GetByFilter(string filter) => voucher.GetByFilter(filter);
        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id) => voucher.GetById(id);
        [HttpPost]
        public VMResponse Add(VMVoucher data) => voucher.Create(data);
        [HttpPut]
        public VMResponse Edit(VMVoucher data) => voucher.Update(data);
        [HttpDelete("[action]/{id?}/{userId?}")]
        public VMResponse Delete(int id, int userId) => voucher.Delete(id, userId);
    }
}
