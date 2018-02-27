using AutoMapper;
using CampCadetWebAPI.DTOModels;
using CampCadetWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;

namespace CampCadetWebAPI.Controllers.Board.Meetings
{
    [RoutePrefix("meetings")]
    public class MeetingController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.Meetings.ToListAsync();

            return Ok(Mapper.Map<List<MeetingDtoModel>>(records));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var record = await db.Meetings.FindAsync(id);

            if (record == null)
                return NotFound();

            return Ok(Mapper.Map<MeetingDtoModel>(record));
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]MeetingDtoModel meeting)
        {
            if (meeting == null || meeting.BoardMeetingLocation?.ID == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists(meeting.DateTime))
                return Conflict();

            var record = Mapper.Map<MeetingModel>(meeting);

            record.BoardMeetingLocationID = (int)meeting.BoardMeetingLocation.ID;
            record.BoardMeetingLocation = null; // Otherwise, it will try to create a new location

            db.Meetings.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] MeetingDtoModel meeting)
        {
            if (meeting == null)
                return BadRequest("Required data was not supplied.");

            if (!await Exists(id))
                return NotFound();

            var record = await db.Meetings.FindAsync(id);

            if (meeting.DateTime != null && record.DateTime != meeting.DateTime)
                record.DateTime = meeting.DateTime;

            if (meeting.Notes != null && record.Notes != meeting.Notes)
                record.Notes = meeting.Notes;

            if (meeting.BoardMeetingLocation.ID != null && record.BoardMeetingLocationID != meeting.BoardMeetingLocation.ID)
                record.BoardMeetingLocationID = (int)meeting.BoardMeetingLocation.ID;

            return await SaveChanges(record, ActionType.Put);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var record = await db.Meetings.FindAsync(id);

            if (record == null)
                return NotFound();

            db.Meetings.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(int id)
        {
            var record = await db.Meetings.FindAsync(id);

            return record != null;
        }

        private async Task<bool> Exists(DateTime dateTime)
        {
            var record = await db.Meetings.FirstOrDefaultAsync(m => m.DateTime == dateTime);

            return record != null;
        }
        #endregion
    }
}