using System.Collections.Generic;
using System.ComponentModel;

namespace SpareManagement.Models
{
    public class BasicInfoViewModel : BaseResponseViewModel
    {
        public List<BasicInfoDetailViewModel> Details { get; set; }
    }


    public class BasicInfoDetailViewModel
    {
        [DisplayName("物料編號")]
        public string PartNo { get; set; }

        [DisplayName("名稱")]
        public string Name { get; set; }

        [DisplayName("採購料號")]
        public string PurchaseId { get; set; }

        [DisplayName("儲位")]
        public string Placement { get; set; }

        [DisplayName("建檔日")]
        public string CreateDate { get; set; }

    }
}
