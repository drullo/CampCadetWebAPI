using CampCadetWebAPI.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace CampCadetWebAPI.Controllers
{
    [RoutePrefix("api/contactreasoncategories")]
    public class ContactReasonCategoryController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.ContactReasonCategories.ToListAsync();

            return Ok(records);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var record = await db.ContactReasonCategories.FindAsync(id);

            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpGet]
        [Route("description/{description}")]
        public async Task<IHttpActionResult> Get(string description)
        {
            var record = await db.ContactReasonCategories.SingleOrDefaultAsync(r => r.Description == description);

            if (record == null)
                return NotFound();

            return Ok(record);
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]ContactReasonCategoryModel record)
        {
            if (record == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists(record.Description))
                return Conflict();

            db.ContactReasonCategories.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]ContactReasonCategoryModel category)
        {
            if (category == null)
                return BadRequest("Required data was not supplied.");

            if (!await Exists(id))
                return NotFound();

            var record = await db.ContactReasonCategories.FindAsync(id);

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
            var record = await db.ContactReasonCategories.FindAsync(id);

            if (record == null)
                return NotFound();

            db.ContactReasonCategories.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(int id)
        {
            var record = await db.ContactReasonCategories.FindAsync(id);

            return record != null;
        }

        private async Task<bool> Exists(string description)
        {
            var record = await db.ContactReasonCategories.FirstOrDefaultAsync(r => r.Description == description);

            return record != null;
        }
        #endregion
    }
}