﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rolisa.DataAccess;
using Rolisa.DataModel;
using Rolisa.ViewModel;

namespace Rolisa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConditionController : ControllerBase
    {
        private DACondition condition;
        public ConditionController(RolisaContext _db) { condition = new DACondition(_db); }
        [HttpGet]
        public VMResponse GetALl() => condition.GetAll();
        [HttpGet("[action]/{filter?}")]
        public VMResponse GetByFilter(string filter) => condition.GetByFilter(filter);
        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id) => condition.GetById(id);
        [HttpPost]
        public VMResponse Add(VMCondition data) => condition.Create(data);
        [HttpPut]
        public VMResponse Edit(VMCondition data) => condition.Update(data);
        [HttpDelete("[action]/{id?}/{userId?}")]
        public VMResponse Delete(int id, int userId) => condition.Delete(id, userId);
    }
}