using AutoMapper;
using CampCadetWebAPI.DTOModels;
using CampCadetWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace CampCadetWebAPI.Controllers
{
    [RoutePrefix("api/membercategories")]
    public class MemberCategoryController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.MemberCategories.ToListAsync();

            return Ok(Mapper.Map<List<MemberCategoryDtoModel>>(records));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var record = await db.MemberCategories.FindAsync(id);

            if (record == null)
                return NotFound();

            return Ok(Mapper.Map<MemberCategoryDtoModel>(record));
        }

        [HttpGet]
        [Route("description/{description}")]
        public async Task<IHttpActionResult> Get(string description)
        {
            var record = await db.MemberCategories.SingleOrDefaultAsync(r => r.Description == description);

            if (record == null)
                return NotFound();

            return Ok(Mapper.Map<MemberCategoryDtoModel>(record));
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]MemberCategoryDtoModel category)
        {
            if (category == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists(category.Description))
                return Conflict();

            var record = Mapper.Map<MemberCategoryModel>(category);
            db.MemberCategories.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]MemberCategoryDtoModel category)
        {
            if (category == null)
                return BadRequest("Required data was not supplied.");

            if (!await Exists(id))
                return NotFound();

            var record = await db.MemberCategories.FindAsync(id);

            if (category.Description != null && record.Description != category.Description)
                record.Description = category.Description;

            return await SaveChanges(record, ActionType.Put);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var record = await db.MemberCategories.FindAsync(id);

            if (record == null)
                return NotFound();

            db.MemberCategories.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(int id)
        {
            var record = await db.MemberCategories.FindAsync(id);

            return record != null;
        }

        private async Task<bool> Exists(string description)
        {
            var record = await db.MemberCategories.FirstOrDefaultAsync(r => r.Description == description);

            return record != null;
        }
        #endregion
    }
}