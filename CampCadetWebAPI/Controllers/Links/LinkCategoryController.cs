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
    [RoutePrefix("api/linkcategories")]
    public class LinkCategoryController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.LinkCategories.ToListAsync();

            return Ok(Mapper.Map<List<LinkCategoryDtoModel>>(records));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var record = await db.LinkCategories.FindAsync(id);

            if (record == null)
                return NotFound();

            return Ok(Mapper.Map<LinkCategoryDtoModel>(record));
        }

        [HttpGet]
        [Route("description/{description}")]
        public async Task<IHttpActionResult> Get(string description)
        {
            var record = await db.LinkCategories.SingleOrDefaultAsync(r => r.Description == description);

            if (record == null)
                return NotFound();

            return Ok(Mapper.Map<LinkCategoryDtoModel>(record));
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]LinkCategoryDtoModel category)
        {
            if (category == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists(category.Description))
                return Conflict();

            var record = Mapper.Map<LinkCategoryModel>(category);
            db.LinkCategories.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]LinkCategoryDtoModel category)
        {
            if (category == null)
                return BadRequest("Required data was not supplied.");

            if (!await Exists(id))
                return NotFound();

            var record = await db.LinkCategories.FindAsync(id);

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
            var record = await db.LinkCategories.FindAsync(id);

            if (record == null)
                return NotFound();

            db.LinkCategories.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(int id)
        {
            var record = await db.LinkCategories.FindAsync(id);

            return record != null;
        }

        private async Task<bool> Exists(string description)
        {
            var record = await db.LinkCategories.FirstOrDefaultAsync(r => r.Description == description);

            return record != null;
        }
        #endregion
    }
}