using CampCadetWebAPI.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;

namespace CampCadetWebAPI.Controllers
{
    [RoutePrefix("api/config")]
    public class MiscConfigurationController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.MiscConfigurations.ToListAsync();

            return Ok(records);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var record = await db.MiscConfigurations.FindAsync(id);

            if (record == null)
                return NotFound();

            return Ok(record);
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]MiscConfigurationModel record)
        {
            if (record == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists(record.Description))
                return Conflict();

            db.MiscConfigurations.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] MiscConfigurationModel config)
        {
            if (config == null)
                return BadRequest("Required data was not supplied.");

            if (!await Exists(id))
                return NotFound();

            var record = await db.MiscConfigurations.FindAsync(id);

            if (config.Description != null && record.Description != config.Description)
                record.Description = config.Description;

            if (config.Value != null && record.Value != config.Value)
                record.Value = config.Value;

            return await SaveChanges(record, ActionType.Put);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var record = await db.MiscConfigurations.FindAsync(id);

            if (record == null)
                return NotFound();

            db.MiscConfigurations.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(int id)
        {
            var record = await db.MiscConfigurations.FindAsync(id);

            return record != null;
        }

        private async Task<bool> Exists(string description)
        {
            var record = await db.MiscConfigurations.FirstOrDefaultAsync(c => c.Description == description);

            return record != null;
        }
        #endregion
    }
}