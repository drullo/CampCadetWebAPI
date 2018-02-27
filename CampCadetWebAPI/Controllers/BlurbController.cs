using CampCadetWebAPI.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;

namespace CampCadetWebAPI.Controllers
{
    [RoutePrefix("api/blurbs")]
    public class BlurbController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.Blurbs.ToListAsync();

            return Ok(records);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var record = await db.Blurbs.FindAsync(id);

            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpGet]
        [Route("name/{name}")]
        public async Task<IHttpActionResult> Get(string name)
        {
            var record = await db.Blurbs.SingleOrDefaultAsync(b => b.Name == name);
            
            if (record == null)
                return NotFound();

            return Ok(record);
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]BlurbModel record)
        {
            if (record == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists(record.Name))
                return Conflict();

            db.Blurbs.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] BlurbModel blurb)
        {
            if (blurb == null)
                return BadRequest("Required data was not supplied.");

            if (!await Exists(id))
                return NotFound();

            var record = await db.Blurbs.FindAsync(id);

            if (blurb.Name != null && record.Name != blurb.Name)
                record.Name = blurb.Name;

            if (blurb.Blurb != null && record.Blurb != blurb.Blurb)
                record.Blurb = blurb.Blurb;

            return await SaveChanges(record, ActionType.Put);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var record = await db.Blurbs.FindAsync(id);

            if (record == null)
                return NotFound();

            db.Blurbs.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(int id)
        {
            var record = await db.Blurbs.FindAsync(id);

            return record != null;
        }

        private async Task<bool> Exists(string name)
        {
            var record = await db.Blurbs.FirstOrDefaultAsync(b => b.Name == name);

            return record != null;
        }
        #endregion
    }
}