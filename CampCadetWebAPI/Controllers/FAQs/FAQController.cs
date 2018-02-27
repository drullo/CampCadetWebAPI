using AutoMapper;
using CampCadetWebAPI.DTOModels;
using CampCadetWebAPI.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;

namespace CampCadetWebAPI.Controllers
{
    [RoutePrefix("api/faqs")]
    public class FAQController : _BaseController
    {
        #region Get
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var records = await db.FAQs.Include(l => l.FAQCategory).ToListAsync();

            return Ok(Mapper.Map<List<FAQDtoModel>>(records));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var record = await db.FAQs.Include(l => l.FAQCategory).FirstOrDefaultAsync(l => l.ID == id);

            if (record == null)
                return NotFound();

            return Ok(Mapper.Map<FAQDtoModel>(record));
        }
        #endregion

        #region Post
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]FAQDtoModel faq)
        {
            if (faq == null || faq.FAQCategory?.ID == null)
                return BadRequest("Required data was not supplied.");

            if (await Exists(faq.Question, faq.Answer))
                return Conflict();

            var record = Mapper.Map<FAQModel>(faq);

            record.FAQCategoryID = (int)faq.FAQCategory.ID;
            record.FAQCategory = null; // Otherwise, it will try to create a new category

            db.FAQs.Add(record);

            return await SaveChanges(record, ActionType.Post);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]FAQDtoModel faq)
        {
            if (faq == null)
                return BadRequest("Required data was not supplied.");

            if (!await Exists(id))
                return NotFound();

            var record = await db.FAQs.FindAsync(id);

            if (faq.Question != null && record.Question != faq.Question)
                record.Question = faq.Question;

            if (faq.Answer != null && record.Answer != faq.Answer)
                record.Answer = faq.Answer;

            if (faq.FAQCategory.ID != null && record.FAQCategoryID != faq.FAQCategory.ID)
                record.FAQCategoryID = (int)faq.FAQCategory.ID;

            return await SaveChanges(record, ActionType.Put);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var record = await db.FAQs.FindAsync(id);

            if (record == null)
                return NotFound();

            db.FAQs.Remove(record);

            return await SaveChanges(record, ActionType.Delete);
        }
        #endregion

        #region Exists
        private async Task<bool> Exists(int id)
        {
            var record = await db.FAQs.FindAsync(id);

            return record != null;
        }

        private async Task<bool> Exists(string question, string answer)
        {
            var record = await db.FAQs.FirstOrDefaultAsync(f => f.Question == question || f.Answer == answer);

            return record != null;
        }
        #endregion
    }
}