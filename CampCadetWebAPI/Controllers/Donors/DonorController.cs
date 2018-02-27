using AutoMapper;
using CampCadetWebAPI.DTOModels;
using CampCadetWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;

namespace CampCadetWebAPI.Controllers
{
    [RoutePrefix("api/donors")]
    public class DonorController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.Donors.ToListAsync();

            return Ok(Mapper.Map<List<DonorDtoModel>>(records));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var record = await db.Donors.FindAsync(id);

            if (record == null)
                return NotFound();

            return Ok(Mapper.Map<DonorDtoModel>(record));
        }

        [HttpGet]
        [Route("name/{name}")]
        public async Task<IHttpActionResult> Get(string name)
        {
            var record = await db.Donors.SingleOrDefaultAsync(d => d.Name == name);

            if (record == null)
                return NotFound();

            return Ok(Mapper.Map<DonorDtoModel>(record));
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]DonorDtoModel donor)
        {
            if (donor == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists(donor.Name))
                return Conflict();

            var record = Mapper.Map<DonorModel>(donor);

            record.SortPriority = donor.SortPriority == null ?
                    Convert.ToInt32(ConfigurationManager.AppSettings["DefaultDonorSortPriority"]) :
                    donor.SortPriority;

            record.DisplayOnWebsite = donor.DisplayOnWebsite == null ?
                    Convert.ToBoolean(ConfigurationManager.AppSettings["DefaultDonorDisplayOnWebsite"]) :
                    donor.DisplayOnWebsite;

            db.Donors.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]DonorDtoModel donor)
        {
            if (donor == null || string.IsNullOrEmpty(donor.Name))
                return BadRequest("Required data was not supplied.");

            if (!await Exists(id))
                return NotFound();

            if (await Exists(donor.Name))
                return Conflict();

            var record = await db.Donors.FindAsync(id);

            if (donor.Name != null && record.Name != donor.Name)
                record.Name = donor.Name;

            if (record.DisplayOnWebsite != donor.DisplayOnWebsite)
                record.DisplayOnWebsite = donor.DisplayOnWebsite;

            if (donor.Notes != null && record.Notes != donor.Notes)
                record.Notes = donor.Notes;

            if (donor.SortPriority != null && record.SortPriority != donor.SortPriority)
                record.SortPriority = donor.SortPriority;

            if (donor.Website != null && record.Website != donor.Website)
                record.Website = donor.Website;

            if (donor.IconURL != null && record.IconURL != donor.IconURL)
                record.IconURL = donor.IconURL;

            return await SaveChanges(record, ActionType.Put);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var record = await db.Donors.FindAsync(id);

            if (record == null)
                return NotFound();

            db.Donors.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(int id)
        {
            var record = await db.Donors.FindAsync(id);

            return record != null;
        }

        private async Task<bool> Exists(string name)
        {
            var record = await db.Donors.FirstOrDefaultAsync(d => d.Name == name);

            return record != null;
        }
        #endregion
    }
}