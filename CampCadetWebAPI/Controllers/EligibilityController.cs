using CampCadetWebAPI.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace CampCadetWebAPI.Controllers
{
    [RoutePrefix("api/eligibility")]
    public class CampEligibilityController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.EligibilityRequirements.ToListAsync();

            return Ok(records);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var record = await db.EligibilityRequirements.FindAsync(id);

            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpGet]
        [Route("requirement/{requirement}")]
        public async Task<IHttpActionResult> Get(string requirement)
        {
            var record = await db.EligibilityRequirements.SingleOrDefaultAsync(r => r.Requirement == requirement);

            if (record == null)
                return NotFound();

            return Ok(record);
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]EligibilityRequirementModel record)
        {
            if (record == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists(record.Requirement))
                return Conflict();

            db.EligibilityRequirements.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] EligibilityRequirementModel eligibility)
        {
            if (eligibility == null)
                return BadRequest("Required data was not supplied.");

            if (!await Exists(id))
                return NotFound();

            var record = await db.EligibilityRequirements.FindAsync(id);

            if (eligibility.Requirement != null && record.Requirement != eligibility.Requirement)
                record.Requirement = eligibility.Requirement;

            return await SaveChanges(record, ActionType.Put);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var record = await db.EligibilityRequirements.FindAsync(id);

            if (record == null)
                return NotFound();

            db.EligibilityRequirements.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(int id)
        {
            var record = await db.EligibilityRequirements.FindAsync(id);

            return record != null;
        }

        private async Task<bool> Exists(string requirement)
        {
            var record = await db.EligibilityRequirements.FirstOrDefaultAsync(r => r.Requirement == requirement);

            return record != null;
        }
        #endregion
    }
}