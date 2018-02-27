using CampCadetWebAPI.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;

namespace CampCadetWebAPI.Controllers
{
    [RoutePrefix("api/camprules")]
    public class CampRuleController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.CampRules.ToListAsync();

            return Ok(records);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var record = await db.CampRules.FindAsync(id);

            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpGet]
        [Route("description/{description}")]
        public async Task<IHttpActionResult> Get(string description)
        {
            var record = await db.CampRules.SingleOrDefaultAsync(r => r.Description == description);

            if (record == null)
                return NotFound();

            return Ok(record);
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]CampRuleModel record)
        {
            if (record == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists(record.Description))
                return Conflict();

            db.CampRules.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] CampRuleModel rule)
        {
            if (rule == null)
                return BadRequest("Required data was not supplied.");

            if (!await Exists(id))
                return NotFound();

            var record = await db.CampRules.FindAsync(id);

            if (rule.Description != null && record.Description != rule.Description)
                record.Description = rule.Description;

            return await SaveChanges(record, ActionType.Put);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var record = await db.CampRules.FindAsync(id);

            if (record == null)
                return NotFound();

            db.CampRules.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(int id)
        {
            var record = await db.CampRules.FindAsync(id);

            return record != null;
        }

        private async Task<bool> Exists(string description)
        {
            var record = await db.CampRules.FirstOrDefaultAsync(r => r.Description == description);

            return record != null;
        }
        #endregion
    }
}