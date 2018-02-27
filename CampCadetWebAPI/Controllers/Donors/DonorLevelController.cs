using AutoMapper;
using CampCadetWebAPI.DTOModels;
using CampCadetWebAPI.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;

namespace CampCadetWebAPI.Controllers
{
    [RoutePrefix("api/donorlevels")]
    public class DonorLevelController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.DonorLevels.ToListAsync();

            return Ok(records);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var record = await db.DonorLevels.FindAsync(id);

            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpGet]
        [Route("description/{description}")]
        public async Task<IHttpActionResult> Get(string description)
        {
            var record = await db.DonorLevels.SingleOrDefaultAsync(r => r.Description == description);

            if (record == null)
                return NotFound();

            return Ok(record);
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]DonorLevelModel level)
        {
            if (level == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists(level.Description))
                return Conflict();

            var record = new DonorLevelModel
            {
                Description = level.Description,
                AmountLower = level.AmountLower,
                AmountUpper = level.AmountUpper,
                Color = level.Color,
                FontColor = level.FontColor
            };

            db.DonorLevels.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]DonorLevelModel level)
        {
            if (level == null || string.IsNullOrEmpty(level.Description))
                return BadRequest("Required data was not supplied.");

            if (!await Exists(id))
                return NotFound();

            if (await Exists(level.Description))
                return Conflict();

            var record = await db.DonorLevels.FindAsync(id);

            if (level.Description != null && record.Description != level.Description)
                record.Description = level.Description;

            if (record.AmountLower != level.AmountLower)
                record.AmountLower = level.AmountLower;

            if (record.AmountUpper != level.AmountUpper)
                record.AmountUpper = level.AmountUpper;

            if (level.Color != null && record.Color != level.Color)
                record.Color = level.Color;

            if (level.FontColor != null && record.FontColor != level.FontColor)
                record.FontColor = level.FontColor;

            return await SaveChanges(record, ActionType.Put);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var record = await db.DonorLevels.FindAsync(id);

            if (record == null)
                return NotFound();

            db.DonorLevels.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(int id)
        {
            var record = await db.DonorLevels.FindAsync(id);

            return record != null;
        }

        private async Task<bool> Exists(string description)
        {
            var record = await db.DonorLevels.FirstOrDefaultAsync(r => r.Description == description);

            return record != null;
        }
        #endregion
    }
}