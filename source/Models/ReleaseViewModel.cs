using SpareManagement.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SpareManagement.Models
{
    public class ReleaseViewModel
    {
        [DisplayName("物料編號 1.")]
        public string PartNo1 { get; set; }

        [DisplayName("領用數量")]
        public int Count1 { get; set; }

        [DisplayName("物料編號 2.")]
        public string PartNo2 { get; set; }

        [DisplayName("領用數量")]
        public int Count2 { get; set; }
        [DisplayName("物料編號 3. ")]
        public string PartNo3 { get; set; }

        [DisplayName("領用數量")]
        public int Count3 { get; set; }
        [DisplayName("物料編號 4. ")]
        public string PartNo4 { get; set; }

        [DisplayName("領用數量")]
        public int Count4 { get; set; }

        [DisplayName("物料編號 5. ")]
        public string PartNo5 { get; set; }

        [DisplayName("領用數量")]
        public int Count5 { get; set; }

        [DisplayName("領用人員")]
        public string ReleaseUser { get; set; }

        [DisplayName("領用日期")]
        public DateTime? ReleaseDate { get; set; }

        [DisplayName("備註")]
        public string Memo { get; set; }

    }
}
