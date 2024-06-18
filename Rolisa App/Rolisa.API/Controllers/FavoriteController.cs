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
    public class FavoriteController : ControllerBase
    {
        private FavoriteService favorite;
        public FavoriteController(FavoriteService _favorite) { favorite = _favorite; }
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
