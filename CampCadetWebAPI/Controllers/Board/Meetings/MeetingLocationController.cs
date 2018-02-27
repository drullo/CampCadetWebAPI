using AutoMapper;
using CampCadetWebAPI.DTOModels;
using CampCadetWebAPI.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Collections.Generic;

namespace CampCadetWebAPI.Controllers
{
    [RoutePrefix("api/meetinglocations")]
    public class MeetingLocationController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.MeetingLocations.ToListAsync();

            return Ok(Mapper.Map<List<MeetingLocationDtoModel>>(records));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var record = await db.MeetingLocations.FindAsync(id);

            if (record == null)
                return NotFound();

            return Ok(Mapper.Map<MeetingLocationDtoModel>(record));
        }

        [HttpGet]
        [Route("description/{description}")]
        public async Task<IHttpActionResult> Get(string description)
        {
            var record = await db.MeetingLocations.SingleOrDefaultAsync(r => r.Description == description);

            if (record == null)
                return NotFound();

            return Ok(Mapper.Map<MeetingLocationDtoModel>(record));
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]MeetingLocationDtoModel location)
        {
            if (location == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists(location.Description))
                return Conflict();

            var record = Mapper.Map<MeetingLocationModel>(location);
            db.MeetingLocations.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]MeetingLocationDtoModel location)
        {
            if (location == null)
                return BadRequest("Required data was not supplied.");

            if (!await Exists(id))
                return NotFound();

            var record = await db.MeetingLocations.FindAsync(id);

            if (location.Description != null && record.Description != location.Description)
                record.Description = location.Description;

            return await SaveChanges(record, ActionType.Put);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var record = await db.MeetingLocations.FindAsync(id);

            if (record == null)
                return NotFound();

            db.MeetingLocations.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(int id)
        {
            var record = await db.MeetingLocations.FindAsync(id);

            return record != null;
        }

        private async Task<bool> Exists(string description)
        {
            var record = await db.MeetingLocations.FirstOrDefaultAsync(r => r.Description == description);

            return record != null;
        }
        #endregion
    }
}