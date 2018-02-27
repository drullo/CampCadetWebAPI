using CampCadetWebAPI.Models;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;

namespace CampCadetWebAPI.Controllers
{
    [RoutePrefix("api/schedule")]
    public class ScheduleController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.Schedules.ToListAsync();

            return Ok(records);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var record = await db.Schedules.FindAsync(id);

            if (record == null)
                return NotFound();

            return Ok(record);
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]ScheduleModel record)
        {
            if (record == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists(record.Event, record.StartTime))
                return Conflict();

            db.Schedules.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] ScheduleModel sched)
        {
            if (sched == null)
                return BadRequest("Required data was not supplied.");

            if (!await Exists(id))
                return NotFound();

            var record = await db.Schedules.FindAsync(id);

            if (sched.Event != null && record.Event != sched.Event)
                record.Event = sched.Event;

            if (sched.StartTime != null && record.StartTime != sched.StartTime)
                record.StartTime = sched.StartTime;

            if (sched.EndTime != null && record.EndTime != sched.EndTime)
                record.EndTime = sched.EndTime;

            if (sched.AdditionalInfo != null && record.AdditionalInfo != sched.AdditionalInfo)
                record.AdditionalInfo = sched.AdditionalInfo;

            if (sched.InfoURL != null && record.InfoURL != sched.InfoURL)
                record.InfoURL = sched.InfoURL;

            return await SaveChanges(record, ActionType.Put);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var record = await db.Schedules.FindAsync(id);

            if (record == null)
                return NotFound();

            db.Schedules.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(int id)
        {
            var record = await db.Schedules.FindAsync(id);

            return record != null;
        }

        private async Task<bool> Exists(string description, DateTime startTime)
        {
            var record = await db.Schedules.FirstOrDefaultAsync(s => s.Event == description && s.StartTime == startTime);

            return record != null;
        }
        #endregion
    }
}