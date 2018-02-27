using AutoMapper;
using CampCadetWebAPI.DTOModels;
using CampCadetWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace CampCadetWebAPI.Controllers
{
    [RoutePrefix("api/members")]
    public class MemberController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.Members.Include(m => m.BoardMemberCategory).ToListAsync();

            return Ok(Mapper.Map<List<MemberDtoModel>>(records));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var record = await db.Members.Include(m => m.BoardMemberCategory).FirstOrDefaultAsync(m => m.ID == id);

            if (record == null)
                return NotFound();

            return Ok(Mapper.Map<MemberDtoModel>(record));
        }

        [HttpGet]
        [Route("directors")]
        public async Task<IHttpActionResult> GetDirectors()
        {
            var directorCategory = await db.MemberCategories.FirstOrDefaultAsync(c => c.Description == "Directors");

            if (directorCategory == null)
                return NotFound();

            var records = await db.Members
                .Where(m => (bool)m.Enabled && m.BoardMemberCategoryID == directorCategory.ID)
                .Include(m => m.BoardMemberCategory)
                .ToListAsync();

            return Ok(Mapper.Map<List<MemberDtoModel>>(records));
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]MemberDtoModel member)
        {
            if (member == null || member.BoardMemberCategory?.ID == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists(member.FirstName, member.LastName))
                return Conflict();

            var record = Mapper.Map<MemberModel>(member);

            record.BoardMemberCategoryID = (int)member.BoardMemberCategory.ID;
            record.BoardMemberCategory = null; // Otherwise, it will try to create a new category

            db.Members.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]MemberDtoModel member)
        {
            if (member == null)
                return BadRequest("Required data was not supplied.");

            if (!await Exists(id))
                return NotFound();

            var record = await db.Members.FindAsync(id);

            if (member.Description != null && record.Description != member.Description)
                record.Description = member.Description;

            if (member.Email != null && record.Email != member.Email)
                record.Email = member.Email;

            if (member.Enabled != null && record.Enabled != member.Enabled)
                record.Enabled = member.Enabled;

            if (member.FirstName != null && record.FirstName != member.FirstName)
                record.FirstName = member.FirstName;

            if (member.LastName != null && record.LastName != member.LastName)
                record.LastName = member.LastName;

            if (member.Prefix != null && record.Prefix != member.Prefix)
                record.Prefix = member.Prefix;

            if (member.ShowEmail != null && record.ShowEmail != member.ShowEmail)
                record.ShowEmail = member.ShowEmail;

            if (member.SortPriority != null && record.SortPriority != member.SortPriority)
                record.SortPriority = member.SortPriority;

            if (member.Title != null && record.Title != member.Title)
                record.Title = member.Title;

            if (member.BoardMemberCategory.ID != null && record.BoardMemberCategoryID != member.BoardMemberCategory.ID)
                record.BoardMemberCategoryID = (int)member.BoardMemberCategory.ID;

            return await SaveChanges(record, ActionType.Put);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var record = await db.Members.FindAsync(id);

            if (record == null)
                return NotFound();

            db.Members.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(int id)
        {
            var record = await db.Members.FindAsync(id);

            return record != null;
        }

        private async Task<bool> Exists(string firstName, string lastName)
        {
            var record = await db.Members.FirstOrDefaultAsync(m => m.FirstName == firstName && m.LastName == lastName);

            return record != null;
        }
        #endregion
    }
}