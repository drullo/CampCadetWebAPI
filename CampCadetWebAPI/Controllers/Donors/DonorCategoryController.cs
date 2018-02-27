using AutoMapper;
using CampCadetWebAPI.DTOModels;
using CampCadetWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace CampCadetWebAPI.Controllers.Donors
{
    [RoutePrefix("api/donorcategories")]
    public class DonorCategoryController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.DonorCategories.ToListAsync();

            return Ok(Mapper.Map<List<DonorCategoryDtoModel>>(records));
        }

        [HttpGet]
        [Route("enabled")]
        public async Task<IHttpActionResult> GetEnabled()
        {
            var records = await db.DonorCategories.Where(c => c.Enabled != null && (bool)c.Enabled).ToListAsync();

            return Ok(Mapper.Map<List<DonorCategoryDtoModel>>(records));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var record = await db.DonorCategories.FindAsync(id);

            if (record == null)
                return NotFound();

            return Ok(Mapper.Map<DonorCategoryDtoModel>(record));
        }

        [HttpGet]
        [Route("description/{description}")]
        public async Task<IHttpActionResult> Get(string description)
        {
            var record = await db.DonorCategories.SingleOrDefaultAsync(r => r.Description == description);

            if (record == null)
                return NotFound();

            return Ok(Mapper.Map<DonorCategoryDtoModel>(record));
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]DonorCategoryDtoModel category)
        {
            if (category == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists(category.Description))
                return Conflict();

            //var record = Mapper.Map<DonorCategoryModel>(category);
            var record = new DonorCategoryModel
            {
                Description = category.Description,
                Enabled = category.Enabled == null ?
                    Convert.ToBoolean(ConfigurationManager.AppSettings["DefaultDonorCategoryEnabled"]) :
                    category.Enabled,
                ShowDonorLevels = category.ShowDonorLevels == null ?
                    Convert.ToBoolean(ConfigurationManager.AppSettings["DefaultDonorCategoryShowDonors"]) :
                    category.ShowDonorLevels,
                SortPriority = category.SortPriority == null ?
                    Convert.ToInt32(ConfigurationManager.AppSettings["DefaultDonorCategorySortPriority"]) :
                    category.SortPriority
            };
            
            db.DonorCategories.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]DonorCategoryDtoModel category)
        {
            if (category == null || string.IsNullOrEmpty(category.Description))
                return BadRequest("Required data was not supplied.");

            if (!await Exists(id))
                return NotFound();

            if (await Exists(category.Description))
                return Conflict();

            var record = await db.DonorCategories.FindAsync(id);

            if (category.Description != null && record.Description != category.Description)
                record.Description = category.Description;

            if (category.Enabled != null && record.Enabled != category.Enabled)
                record.Enabled = category.Enabled;

            if (category.SortPriority != null && record.SortPriority != category.SortPriority)
                record.SortPriority = category.SortPriority;

            if (category.ShowDonorLevels != null && record.ShowDonorLevels != category.ShowDonorLevels)
                record.ShowDonorLevels = category.ShowDonorLevels;

            return await SaveChanges(record, ActionType.Put);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var record = await db.DonorCategories.FindAsync(id);

            if (record == null)
                return NotFound();

            db.DonorCategories.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(int id)
        {
            var record = await db.DonorCategories.FindAsync(id);

            return record != null;
        }

        private async Task<bool> Exists(string description)
        {
            var record = await db.DonorCategories.FirstOrDefaultAsync(r => r.Description == description);

            return record != null;
        }
        #endregion
    }
}