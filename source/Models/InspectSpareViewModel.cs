using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SpareManagement.Models
{
    public class InspectSpareViewModel
    {
        public List<InspectDetailViewModel> Details { get; set; }
    }

    public class InspectDetailViewModel
    {
        [DisplayName("物料編號")]
        public string PartNo { get; set; }

        [DisplayName("名稱")]
        public string Name { get; set; }

        [DisplayName("序號")]
        public string SerialNo { get; set; }

        [DisplayName("規格")]
        public string Spec { get; set; }

        [DisplayName("下次檢驗日期")]
        public string NextInspectDate { get; set; }

        [DisplayName("狀態")]
        public string Status { get; set; }

        public int CategoryId { get; set; }

        public int StatusId { get; set; }

        public string UpdateUser { get; set; }

        public DateTime UpdateDTE { get; set; }


    }
}
