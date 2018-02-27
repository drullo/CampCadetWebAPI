
using AutoMapper;
using CampCadetWebAPI.DataAccess;
using CampCadetWebAPI.DTOModels;
using CampCadetWebAPI.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CampCadetWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public abstract class _BaseController : ApiController
    {
        public enum ActionType
        {
            Delete,
            Post,
            Put
        }

        public CampCadetDbContext db = new CampCadetDbContext();

        /// <summary>
        /// Perform mapping (when needed) and lookup records on Post so that the Location Uri in the header can point to the proper record
        /// </summary>
        /// <param name="record"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        private async Task<object> GetReturnRecord(object record, ActionType actionType)
        {
            var returnRecord = record;

            // Blurb (no mapping)
            if (record is BlurbModel && actionType == ActionType.Post)
                returnRecord = await db.Blurbs.SingleAsync(b => b.Name == ((BlurbModel)record).Name);
            // Camp Date (no mapping)
            else if (record is CampDateModel && actionType == ActionType.Post)
                returnRecord = await db.CampDates.SingleAsync(d => d.StartDate == ((CampDateModel)record).StartDate);
            // Camp Rule (no mapping)
            else if (record is CampRuleModel && actionType == ActionType.Post)
                returnRecord = await db.CampRules.SingleAsync(r => r.Description == ((CampRuleModel)record).Description);
            // Camp Supply (no mapping)
            else if (record is CampSupplyModel && actionType == ActionType.Post)
                returnRecord = await db.CampSupplies.SingleAsync(s => s.Item == ((CampSupplyModel)record).Item);
            // Contact Category (no mapping)
            else if (record is ContactCategoryModel && actionType == ActionType.Post)
                returnRecord = await db.ContactCategories.SingleAsync(c => c.Description == ((ContactCategoryModel)record).Description);
            // Contact Reason Category (no mapping)
            else if (record is ContactReasonCategoryModel && actionType == ActionType.Post)
                returnRecord = await db.ContactReasonCategories.SingleAsync(c => c.Description == ((ContactReasonCategoryModel)record).Description);
            // Download (no mapping)
            else if (record is DownloadModel && actionType == ActionType.Post)
                returnRecord = await db.Downloads.SingleAsync(d => d.FileName == ((DownloadModel)record).FileName);
            // Eligibility Requirement (no mapping)
            else if (record is EligibilityRequirementModel && actionType == ActionType.Post)
                returnRecord = await db.EligibilityRequirements.SingleAsync(r => r.Requirement == ((EligibilityRequirementModel)record).Requirement);
            // Fundraising Event (no mapping)
            else if (record is FundraisingEventModel && actionType == ActionType.Post)
                returnRecord = await db.FundraisingEvents.SingleAsync(e => e.Description == ((FundraisingEventModel)record).Description && e.StartDate == ((FundraisingEventModel)returnRecord).StartDate);
            // Miscellaneous Configuration (no mapping)
            else if (record is MiscConfigurationModel && actionType == ActionType.Post)
                returnRecord = await db.MiscConfigurations.SingleAsync(m => m.Description == ((MiscConfigurationModel)record).Description);
            // Schedule (no mapping)
            else if (record is ScheduleModel && actionType == ActionType.Post)
                returnRecord = await db.Schedules.SingleAsync(s => s.StartTime == ((ScheduleModel)record).StartTime && s.Event == ((ScheduleModel)record).Event);
            // Donor Level (no mapping)
            else if (record is DonorLevelModel && actionType == ActionType.Post)
                returnRecord = await db.DonorLevels.SingleAsync(l => l.Description == ((DonorLevelModel)record).Description);
            // Donor Category
            else if (record is DonorCategoryModel)
            {
                if (actionType == ActionType.Post)
                    returnRecord = await db.DonorCategories.SingleAsync(c => c.Description == ((DonorCategoryModel)record).Description);

                returnRecord = Mapper.Map<DonorCategoryDtoModel>(returnRecord);
            }
            // Donor
            else if (record is DonorModel)
            {
                if (actionType == ActionType.Post)
                    returnRecord = await db.Donors.SingleAsync(d => d.Name == ((DonorModel)record).Name);

                returnRecord = Mapper.Map<DonorDtoModel>(returnRecord);
            }
            // Donor <=> Category Link
            else if (record is DonorCategoryLinkModel)
            {
                if (actionType == ActionType.Post)
                    returnRecord = await db.DonorCategoryLinks
                        .SingleAsync(l => l.DonorID == ((DonorCategoryLinkModel)record).DonorID && l.CategoryID == ((DonorCategoryLinkModel)record).CategoryID);

                returnRecord = Mapper.Map<DonorCategoryLinkDtoModel>(returnRecord);
            }
            // FAQ Category
            else if (record is FAQCategoryModel)
            {
                if (actionType == ActionType.Post)
                    returnRecord = await db.FAQCategories.SingleAsync(c => c.Description == ((FAQCategoryModel)record).Description);

                returnRecord = Mapper.Map<FAQCategoryDtoModel>(returnRecord);
            }
            // FAQ
            else if (record is FAQModel)
            {
                if (actionType == ActionType.Post || actionType == ActionType.Put)
                    returnRecord = await db.FAQs
                        .Include(f => f.FAQCategory)
                        .SingleAsync(f => f.Question == ((FAQModel)record).Question);

                returnRecord = Mapper.Map<FAQDtoModel>(returnRecord);
            }
            // Link Category
            else if (record is LinkCategoryModel)
            {
                if (actionType == ActionType.Post)
                    returnRecord = await db.LinkCategories.SingleAsync(c => c.Description == ((LinkCategoryModel)record).Description);

                returnRecord = Mapper.Map<LinkCategoryDtoModel>(returnRecord);
            }
            // Link
            else if (record is LinkModel)
            {
                if (actionType == ActionType.Post || actionType == ActionType.Put)
                    returnRecord = await db.Links
                        .Include(l => l.LinkCategory)
                        .SingleAsync(l => l.Description == ((LinkModel)record).Description);

                returnRecord = Mapper.Map<LinkDtoModel>(returnRecord);
            }
            // Meeting Location
            else if (record is MeetingLocationModel)
            {
                if (actionType == ActionType.Post)
                    returnRecord = await db.MeetingLocations.SingleAsync(l => l.Description == ((MeetingLocationModel)record).Description);

                returnRecord = Mapper.Map<MeetingLocationDtoModel>(returnRecord);
            }
            // Meeting
            else if (record is MeetingModel)
            {
                if (actionType == ActionType.Post)
                    returnRecord = await db.Meetings.SingleAsync(m => m.DateTime == ((MeetingModel)record).DateTime);

                returnRecord = Mapper.Map<MeetingDtoModel>(returnRecord);
            }
            // Member Category
            else if (record is MemberCategoryModel)
            {
                if (actionType == ActionType.Post)
                    returnRecord = await db.MemberCategories.SingleAsync(c => c.Description == ((MemberCategoryModel)record).Description);

                returnRecord = Mapper.Map<MemberCategoryDtoModel>(returnRecord);
            }
            // Member
            else if (record is MemberModel)
            {
                if (actionType == ActionType.Post || actionType == ActionType.Put)
                    returnRecord = await db.Members
                        .Include(m => m.BoardMemberCategory)
                        .SingleAsync(m => m.FirstName == ((MemberModel)record).FirstName && m.LastName == ((MemberModel)record).LastName);

                returnRecord = Mapper.Map<MemberDtoModel>(returnRecord);
            }

            return returnRecord;
        }

        /// <summary>
        /// Get a Uri to be returned as a header on Post/Put results
        /// </summary>
        /// <param name="record"></param>
        /// <param name="returnRecord"></param>
        /// <returns></returns>
        private Uri GetLocationUri(object record, object returnRecord)
        {
            Uri uri = null;

            // Blurb (no mapping)
            if (record is BlurbModel)
                uri = new Uri(Request.RequestUri + "/" + ((BlurbModel)returnRecord).ID);
            // Camp Date (no mapping)
            else if (record is CampDateModel)
                uri = new Uri(Request.RequestUri + "/" + ((CampDateModel)returnRecord).StartDate);
            // Camp Rule (no mapping)
            else if (record is CampRuleModel)
                uri = new Uri(Request.RequestUri + "/" + ((CampRuleModel)returnRecord).ID);
            // Camp Supply (no mapping)
            else if (record is CampSupplyModel)
                uri = new Uri(Request.RequestUri + "/" + ((CampSupplyModel)returnRecord).ID);
            // Contact Category (no mapping)
            else if (record is ContactCategoryModel)
                uri = new Uri(Request.RequestUri + "/" + ((ContactCategoryModel)returnRecord).ID);
            // Contact Reason Category (no mapping)
            else if (record is ContactReasonCategoryModel)
                uri = new Uri(Request.RequestUri + "/" + ((ContactReasonCategoryModel)returnRecord).ID);
            // Download (no mapping)
            else if (record is DownloadModel)
                uri = new Uri(Request.RequestUri + "/" + ((DownloadModel)returnRecord).ID);
            // Eligibility Requirement (no mapping)
            else if (record is EligibilityRequirementModel)
                uri = new Uri(Request.RequestUri + "/" + ((EligibilityRequirementModel)returnRecord).ID);
            // Fundraising Event (no mapping)
            else if (record is FundraisingEventModel)
                uri = new Uri(Request.RequestUri + "/" + ((FundraisingEventModel)returnRecord).ID);
            // Miscellaneous Configuration (no mapping)
            else if (record is MiscConfigurationModel)
                uri = new Uri(Request.RequestUri + "/" + ((MiscConfigurationModel)returnRecord).ID);
            // Schedule (no mapping)
            else if (record is ScheduleModel)
                uri = new Uri(Request.RequestUri + "/" + ((ScheduleModel)returnRecord).ID);
            // Donor Level (no mapping)
            else if (record is DonorLevelModel)
                uri = new Uri(Request.RequestUri + "/" + ((DonorLevelModel)returnRecord).ID);
            // Donor Category
            else if (record is DonorCategoryModel)
                uri = new Uri(Request.RequestUri + "/" + ((DonorCategoryDtoModel)returnRecord).ID);
            // Donor
            else if (record is DonorModel)
                uri = new Uri(Request.RequestUri + "/" + ((DonorDtoModel)returnRecord).ID);
            // Donor <=> Category Link
            else if (record is DonorCategoryLinkModel)
                uri = new Uri(Request.RequestUri + "/" + ((DonorCategoryLinkDtoModel)returnRecord).Donor.ID + "/" + ((DonorCategoryLinkDtoModel)returnRecord).DonorCategory.ID);
            // FAQ Category
            else if (record is FAQCategoryModel)
                uri = new Uri(Request.RequestUri + "/" + ((FAQCategoryDtoModel)returnRecord).ID);
            // FAQ
            else if (record is FAQModel)
                uri = new Uri(Request.RequestUri + "/" + ((FAQDtoModel)returnRecord).ID);
            // Link Category
            else if (record is LinkCategoryModel)
                uri = new Uri(Request.RequestUri + "/" + ((LinkCategoryDtoModel)returnRecord).ID);
            // Link
            else if (record is LinkModel)
                uri = new Uri(Request.RequestUri + "/" + ((LinkDtoModel)returnRecord).ID);
            // Meeting Location
            else if (record is MeetingLocationModel)
                uri = new Uri(Request.RequestUri + "/" + ((MeetingLocationDtoModel)returnRecord).ID);
            // Meeting
            else if (record is MeetingModel)
                uri = new Uri(Request.RequestUri + "/" + ((MeetingDtoModel)returnRecord).ID);
            // Member Category
            else if (record is MemberCategoryModel)
                uri = new Uri(Request.RequestUri + "/" + ((MemberCategoryDtoModel)returnRecord).ID);
            // Member
            else if (record is MemberModel)
                uri = new Uri(Request.RequestUri + "/" + ((MemberDtoModel)returnRecord).ID);

            return uri;
        }

        public async Task<IHttpActionResult> SaveChanges(object record, ActionType actionType)
        {
            if (await db.SaveChangesAsync() == 0)
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));

            var returnRecord = await GetReturnRecord(record, actionType);

            switch (actionType)
            {
                case ActionType.Post:
                case ActionType.Put:
                    var uri = GetLocationUri(record, returnRecord);
                    return Created(uri, returnRecord);
                default:
                    return Ok(returnRecord);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}