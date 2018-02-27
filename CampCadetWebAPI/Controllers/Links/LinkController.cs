using AutoMapper;
using CampCadetWebAPI.DTOModels;
using CampCadetWebAPI.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;

namespace CampCadetWebAPI.Controllers
{
    [RoutePrefix("api/links")]
    public class LinkController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.Links.Include(l => l.LinkCategory).ToListAsync();

            return Ok(Mapper.Map<List<LinkDtoModel>>(records));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var record = await db.Links.Include(l => l.LinkCategory).FirstOrDefaultAsync(l => l.ID == id);

            if (record == null)
                return NotFound();

            return Ok(Mapper.Map<LinkDtoModel>(record));
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]LinkDtoModel link)
        {
            if (link == null || link.LinkCategory?.ID == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists(link.URL, link.Description))
                return Conflict();

            var record = Mapper.Map<LinkModel>(link);

            record.LinkCategoryID = (int)link.LinkCategory.ID;
            record.LinkCategory = null; // Otherwise, it will try to create a new category

            db.Links.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]LinkDtoModel link)
        {
            if (link == null)
                return BadRequest("Required data was not supplied.");

            if (!await Exists(id))
                return NotFound();

            var record = await db.Links.FindAsync(id);

            if (link.Description != null && record.Description != link.Description)
                record.Description = link.Description;

            if (link.URL != null && record.URL != link.URL)
                record.URL = link.URL;

            if (link.LinkCategory.ID != null && record.LinkCategoryID != link.LinkCategory.ID)
                record.LinkCategoryID = (int)link.LinkCategory.ID;


            return await SaveChanges(record, ActionType.Put);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var record = await db.Links.FindAsync(id);

            if (record == null)
                return NotFound();

            db.Links.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(int id)
        {
            var record = await db.Links.FindAsync(id);

            return record != null;
        }

        private async Task<bool> Exists(string url, string description)
        {
            var record = await db.Links.FirstOrDefaultAsync(r => r.URL == url || r.Description == description);

            return record != null;
        }
        #endregion
    }
}