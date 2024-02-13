using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rolisa.DataAccess;
using Rolisa.DataModel;
using Rolisa.ViewModel;

namespace Rolisa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private DAFavorite favorite;
        public FavoriteController(RolisaContext _db) { favorite = new DAFavorite(_db); }
        [HttpGet]
        public VMResponse GetALl() => favorite.GetAll();
        [HttpGet("[action]/{filter?}")]
        public VMResponse GetByFilter(string filter) => favorite.GetByFilter(filter);
        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id) => favorite.GetById(id);
        [HttpPost]
        public VMResponse Add(VMFavorite data) => favorite.Create(data);

        [HttpDelete("[action]/{id?}/{userId?}")]
        public VMResponse Delete(int id, int userId) => favorite.Delete(id, userId);
    }
}
