using CampCadetWebAPI.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace CampCadetWebAPI.Controllers
{
    [RoutePrefix("api/campsupplies")]
    public class CampSupplyController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.CampSupplies.ToListAsync();

            return Ok(records);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var record = await db.CampSupplies.FindAsync(id);

            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpGet]
        [Route("item/{item}")]
        public async Task<IHttpActionResult> Get(string item)
        {
            var record = await db.CampSupplies.SingleOrDefaultAsync(s => s.Item == item);

            if (record == null)
                return NotFound();

            return Ok(record);
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]CampSupplyModel record)
        {
            if (record == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists(record.Item))
                return Conflict();

            db.CampSupplies.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] CampSupplyModel supply)
        {
            if (supply == null)
                return BadRequest("Required data was not supplied.");

            if (!await Exists(id))
                return NotFound();

            var record = await db.CampSupplies.FindAsync(id);

            if (supply.Item != null && record.Item != supply.Item)
                record.Item = supply.Item;

            return await SaveChanges(record, ActionType.Put);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var record = await db.CampSupplies.FindAsync(id);

            if (record == null)
                return NotFound();

            db.CampSupplies.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(int id)
        {
            var record = await db.CampSupplies.FindAsync(id);

            return record != null;
        }

        private async Task<bool> Exists(string item)
        {
            var record = await db.CampSupplies.FirstOrDefaultAsync(r => r.Item == item);

            return record != null;
        }
        #endregion
    }
}