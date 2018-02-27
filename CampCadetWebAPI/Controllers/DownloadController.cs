using CampCadetWebAPI.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;

namespace CampCadetWebAPI.Controllers
{
    [RoutePrefix("api/downloads")]
    public class DownloadController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.Downloads.ToListAsync();

            return Ok(records);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var record = await db.Downloads.FindAsync(id);

            if (record == null)
                return NotFound();

            return Ok(record);
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]DownloadModel record)
        {
            if (record == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists(record.FileName))
                return Conflict();

            db.Downloads.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] DownloadModel download)
        {
            if (download == null)
                return BadRequest("Required data was not supplied.");

            if (!await Exists(id))
                return NotFound();

            var record = await db.Downloads.FindAsync(id);

            if (download.FileName != null && record.FileName != download.FileName)
                record.FileName = download.FileName;

            if (record.AllowDownload != download.AllowDownload)
                record.AllowDownload = download.AllowDownload;

            if (download.UploadedBy != null && record.UploadedBy != download.UploadedBy)
                record.UploadedBy = download.UploadedBy;

            return await SaveChanges(record, ActionType.Put);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var record = await db.Downloads.FindAsync(id);

            if (record == null)
                return NotFound();

            db.Downloads.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(int id)
        {
            var record = await db.Downloads.FindAsync(id);

            return record != null;
        }

        private async Task<bool> Exists(string fileName)
        {
            var record = await db.Downloads.FirstOrDefaultAsync(d => d.FileName == fileName);

            return record != null;
        }
        #endregion
    }
}