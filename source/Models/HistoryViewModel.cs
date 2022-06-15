using System.Collections.Generic;
using System.ComponentModel;

namespace SpareManagement.Models
{
    public class HistoryViewModel
    {
        [DisplayName("物料編號")]
        public string PartNo { get; set; }

        public List<HistoryDetailViewModel> details { get; set; }
    }

    public class HistoryDetailViewModel
    {
        [DisplayName("日期")]
        public string Date { get; set; }

        [DisplayName("狀態")]
        public string Status { get; set; }

        [DisplayName("數量")]
        public string Quantity { get; set; }

        [DisplayName("站點")]
        public string Node { get; set; }

        [DisplayName("人員")]
        public string Employee { get; set; }

        [DisplayName("備註")]
        public string Memo { get; set; }

    }
}
