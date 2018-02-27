using AutoMapper;
using CampCadetWebAPI.DTOModels;
using CampCadetWebAPI.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace CampCadetWebAPI.Controllers
{
    [RoutePrefix("api/donorcatlinks")]
    public class DonorCategoryLinkController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.DonorCategoryLinks
                .Include(l => l.Donor)
                .Include(l => l.DonorCategory)
                .ToListAsync();

            return Ok(Mapper.Map<List<DonorCategoryLinkDtoModel>>(records));
        }

        [HttpGet]
        [Route("current")]
        public async Task<IHttpActionResult> GetCurrent()
        {
            var records = await db.DonorCategoryLinks
                .Include(l => l.Donor)
                .Include(l => l.DonorCategory)
                .Where(l => l.DonorCategory.Enabled == true)
                .ToListAsync();

            return Ok(Mapper.Map<List<DonorCategoryLinkDtoModel>>(records));
        }

        [HttpGet]
        [Route("{donorId}/{categoryId}")]
        public async Task<IHttpActionResult> Get(int donorId, int categoryId)
        {
            var record = await db.DonorCategoryLinks
                .Include(l => l.Donor)
                .Include(l => l.DonorCategory)
                .FirstOrDefaultAsync(l => l.DonorID == donorId && l.CategoryID == categoryId);

            return Ok(Mapper.Map<DonorCategoryLinkDtoModel>(record));
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]DonorCategoryLinkDtoModel link)
        {
            if (link == null || link.Donor?.ID == null || link.DonorCategory?.ID == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists((int)link.Donor.ID, (int)link.DonorCategory.ID))
                return Conflict();

            var record = Mapper.Map<DonorCategoryLinkModel>(link);

            record.DonorID = (int)link.Donor.ID;
            record.Donor = null;

            record.CategoryID = (int)link.DonorCategory.ID;
            record.DonorCategory = null;

            if (link.AmountGiven != null)
                record.AmountGiven = link.AmountGiven;

            if (link.Notes != null)
                record.Notes = link.Notes;

            db.DonorCategoryLinks.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{donorId}/{categoryId}")]
        public async Task<IHttpActionResult> Delete(int donorId, int categoryId)
        {
            var record = await db.DonorCategoryLinks.FirstOrDefaultAsync(l => l.DonorID == donorId && l.CategoryID == categoryId);

            if (record == null)
                return NotFound();

            db.DonorCategoryLinks.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(int donorId, int categoryId)
        {
            var record = await db.DonorCategoryLinks.FirstOrDefaultAsync(l => l.DonorID == donorId && l.CategoryID == categoryId);

            return record != null;
        }
        #endregion
    }
}