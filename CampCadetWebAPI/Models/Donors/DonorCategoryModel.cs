using CampCadetWebAPI.DTOModels;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace CampCadetWebAPI.Models
{
    public class DonorCategoryModel : DonorCategoryDtoModel
    {
        public List<DonorCategoryLinkModel> Donations { get; set; }

        public DonorCategoryModel()
        {
            Enabled = Convert.ToBoolean(ConfigurationManager.AppSettings["DefaultDonorCategoryEnabled"]);
            ShowDonorLevels = Convert.ToBoolean(ConfigurationManager.AppSettings["DefaultDonorCategoryShowDonors"]);
            SortPriority = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultDonorCategorySortPriority"]);
        }
    }
}