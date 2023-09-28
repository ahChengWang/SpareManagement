using SpareManagement.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SpareManagement.Models
{
    public class SpareViewModel
    {
        public int No { get; set; }

        [DisplayName("類別")]
        public int CategoryId { get; set; }

        [DisplayName("名稱")]
        [Required(ErrorMessage = "*必填")]
        public string Name { get; set; }

        [DisplayName("規格")]
        [Required(ErrorMessage = "*必填")]
        public string Spec { get; set; }

        [DisplayName("採購料號")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "*英數字")]
        [Required(ErrorMessage = "*必填")]
        public string PurchaseId { get; set; }

        [DisplayName("是否為特製品")]
        public bool IsSpecial { get; set; }

        [DisplayName("是否為關鍵備品")]
        public bool IsKeySpare { get; set; }

        [DisplayName("是否為市購品")]
        public bool IsCommercial { get; set; }

        [DisplayName("適用機種")]
        [Required(ErrorMessage = "*必填")]
        public string Equipment { get; set; }

        [DisplayName("適用製程")]
        [Required(ErrorMessage = "*必填")]
        public List<string> UseLocation { get; set; }

        [DisplayName("請購交期")]
        public int PurchaseDelivery { get; set; }

        [DisplayName("安全庫存")]
        public int SafetyCount { get; set; }

        [DisplayName("是否需檢驗")]
        public bool IsNeedInspect { get; set; }

        [DisplayName("檢驗日期")]
        public DateTime InspectDate { get; set; }

        [DisplayName("檢驗週期")]
        public int InspectCycle { get; set; }

        [DisplayName("儲位")]
        [Required(ErrorMessage = "*必填")]
        public string Placement { get; set; }

        [DisplayName("建檔人員")]
        [Required(ErrorMessage = "*必填")]
        public string CreateUser { get; set; }

        [DisplayName("管理人員")]
        [Required(ErrorMessage = "*必填")]
        public string Manager { get; set; }

        [DisplayName("備註")]
        public string Memo { get; set; }

    }

    public class SpareType
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
    }

    public class Option
    {
        public string PartNo { get; set; }
        public string Desc { get; set; }
    }
}
