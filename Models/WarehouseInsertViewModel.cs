using SpareManagement.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SpareManagement.Models
{
    public class WarehouseInsertViewModel
    {
        [DisplayName("物料編號 1.")]
        [RegularExpression(@"^[A-Za-z]_[0-9][0-9][0-9][0-9]_[0-9][0-9][0-9][0-9]+$", ErrorMessage = "*英數字")]
        public string PartNo1 { get; set; }

        [DisplayName("入庫數量")]
        public int Count1 { get; set; }

        [DisplayName("物料編號 2.")]
        public string PartNo2 { get; set; }

        [DisplayName("入庫數量")]
        public int Count2 { get; set; }
        [DisplayName("物料編號 3. ")]
        public string PartNo3 { get; set; }

        [DisplayName("入庫數量")]
        public int Count3 { get; set; }
        [DisplayName("物料編號 4. ")]
        public string PartNo4 { get; set; }

        [DisplayName("入庫數量")]
        public int Count4 { get; set; }

        [DisplayName("物料編號 5. ")]
        public string PartNo5 { get; set; }

        [DisplayName("入庫數量")]
        public int Count5 { get; set; }

        [DisplayName("入庫人員")]
        [Required(ErrorMessage = "*必填")]
        public string CreateUser { get; set; }

        [DisplayName("入庫日期")]
        [Required(ErrorMessage = "*必填")]
        public DateTime? CreateDate { get; set; }

        [DisplayName("備註")]
        public string Memo { get; set; }

    }
}
