using CampCadetWebAPI.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace CampCadetWebAPI.Controllers
{
    [RoutePrefix("api/campdates")]
    public class CampDateController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.CampDates.ToListAsync();

            return Ok(records.OrderBy(d => d.StartDate));
        }

        [HttpGet]
        [Route("latest")]
        public async Task<IHttpActionResult> GetLatest()
        {
            var records = await db.CampDates.ToListAsync();

            var record = records.OrderByDescending(d => d.StartDate).FirstOrDefault();

            return Ok(record);
        }

        [HttpGet]
        [Route("current")]
        public async Task<IHttpActionResult> GetCurrent()
        {
            var records = await db.CampDates.ToListAsync();

            var filtered = records.Where(d => d.EndDate >= DateTime.Now).OrderBy(d => d.StartDate);

            return Ok(filtered);
        }

        [HttpGet]
        [Route("{startDate}")]
        public async Task<IHttpActionResult> Get(DateTime startDate)
        {
            var record = await db.CampDates.SingleOrDefaultAsync(d => d.StartDate == startDate);

            if (record == null)
                return NotFound();

            return Ok(record);
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]CampDateModel record)
        {
            if (record == null || record.StartDate == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists((DateTime)record.StartDate))
                return Conflict();

            db.CampDates.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Put
        #region Put
        [HttpPut]
        [Route("{startDate}")]
        public async Task<IHttpActionResult> Put(DateTime startDate, [FromBody]CampDateModel dates)
        {
            if (dates == null)
                return BadRequest("Required data was not supplied.");

            if (!await Exists(startDate))
                return NotFound();

            var record = await db.CampDates.FindAsync(dates);

            if (dates.StartDate != null && record.StartDate != dates.StartDate)
                record.StartDate = dates.StartDate;

            if (dates.EndDate != null && record.EndDate != dates.EndDate)
                record.EndDate = dates.EndDate;

            if (dates.ApplicationDeadline != null && record.ApplicationDeadline != dates.ApplicationDeadline)
                record.ApplicationDeadline = dates.ApplicationDeadline;

            if (dates.ApplicationsAvailableBeginning != null && record.ApplicationsAvailableBeginning != dates.ApplicationsAvailableBeginning)
                record.ApplicationsAvailableBeginning = dates.ApplicationsAvailableBeginning;

            if (dates.OrientationDate != null && record.OrientationDate != dates.OrientationDate)
                record.OrientationDate = dates.OrientationDate;

            return await SaveChanges(record, ActionType.Put);
        }
        #endregion
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{startDate}")]
        public async Task<IHttpActionResult> Delete(DateTime startDate)
        {
            var record = await db.CampDates.FindAsync(startDate);

            if (record == null)
                return NotFound();

            db.CampDates.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(DateTime startDate)
        {
            var record = await db.CampDates.FindAsync(startDate);

            return record != null;
        }
        #endregion
    }
}