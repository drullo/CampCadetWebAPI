using CampCadetWebAPI.Models;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;

namespace CampCadetWebAPI.Controllers
{
    [RoutePrefix("api/events")]
    public class FundraisingEventController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.FundraisingEvents.ToListAsync();

            return Ok(records);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var record = await db.FundraisingEvents.FindAsync(id);

            if (record == null)
                return NotFound();

            return Ok(record);
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]FundraisingEventModel record)
        {
            if (record == null || record.StartDate == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists(record.Description, record.StartDate))
                return Conflict();

            db.FundraisingEvents.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] FundraisingEventModel fevent)
        {
            if (fevent == null)
                return BadRequest("Required data was not supplied.");

            if (!await Exists(id))
                return NotFound();

            var record = await db.FundraisingEvents.FindAsync(id);

            if (fevent.Description != null && record.Description != fevent.Description)
                record.Description = fevent.Description;

            if (fevent.Details != null && record.Details != fevent.Details)
                record.Details = fevent.Details;

            if (fevent.StartDate != null && record.StartDate != fevent.StartDate)
                record.StartDate = fevent.StartDate;

            if (fevent.EndDate != null && record.EndDate != fevent.EndDate)
                record.EndDate = fevent.EndDate;

            return await SaveChanges(record, ActionType.Put);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var record = await db.FundraisingEvents.FindAsync(id);

            if (record == null)
                return NotFound();

            db.FundraisingEvents.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(int id)
        {
            var record = await db.FundraisingEvents.FindAsync(id);

            return record != null;
        }

        private async Task<bool> Exists(string description, DateTime startDate)
        {
            var record = await db.FundraisingEvents.FirstOrDefaultAsync(e => e.Description == description && e.StartDate == startDate);

            return record != null;
        }
        #endregion
    }
}