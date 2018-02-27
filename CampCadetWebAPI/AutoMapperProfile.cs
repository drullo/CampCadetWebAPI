using AutoMapper;
using CampCadetWebAPI.DTOModels;
using CampCadetWebAPI.Models;

namespace CampCadetWebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Board
            CreateMap<MeetingLocationModel, MeetingLocationDtoModel>();
            CreateMap<MeetingLocationDtoModel, MeetingLocationModel>();

            CreateMap<MemberCategoryModel, MemberCategoryDtoModel>();
            CreateMap<MemberCategoryDtoModel, MemberCategoryModel>();

            CreateMap<MemberModel, MemberDtoModel>();
            CreateMap<MemberDtoModel, MemberModel>();
            #endregion

            #region FAQs
            CreateMap<FAQCategoryModel, FAQCategoryDtoModel>();
            CreateMap<FAQCategoryDtoModel, FAQCategoryModel>();

            CreateMap<FAQModel, FAQDtoModel>();
            CreateMap<FAQDtoModel, FAQModel>();
            #endregion

            #region Links
            CreateMap<LinkCategoryModel, LinkCategoryDtoModel>();
            CreateMap<LinkCategoryDtoModel, LinkCategoryModel>();

            CreateMap<LinkModel, LinkDtoModel>();
            CreateMap<LinkDtoModel, LinkModel>();
            #endregion

            #region Donors
            CreateMap<DonorCategoryModel, DonorCategoryDtoModel>();
            CreateMap<DonorCategoryDtoModel, DonorCategoryModel>();

            CreateMap<DonorCategoryLinkModel, DonorCategoryLinkDtoModel>();
            CreateMap<DonorCategoryLinkDtoModel, DonorCategoryLinkModel>();

            CreateMap<DonorModel, DonorDtoModel>();
            CreateMap<DonorDtoModel, DonorModel>();
            #endregion
        }
    }
}