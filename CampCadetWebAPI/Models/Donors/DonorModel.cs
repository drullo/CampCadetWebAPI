using CampCadetWebAPI.DTOModels;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace CampCadetWebAPI.Models
{
    public class DonorModel : DonorDtoModel
    {
        public List<DonorCategoryLinkModel> Donations { get; set; }

        public DonorModel()
        {
            SortPriority = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultDonorSortPriority"]);
        }
    }
}