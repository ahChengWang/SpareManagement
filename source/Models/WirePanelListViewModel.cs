using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SpareManagement.Models
{
    public class WirePanelListViewModel
    {
        public List<WirePanelViewModel> Details { get; set; }
    }

    public class WirePanelViewModel
    {
        [DisplayName("物料編號")]
        public string PartNo { get; set; }

        [DisplayName("名稱")]
        public string Name { get; set; }

        [DisplayName("序號")]
        public string SerialNo { get; set; }

        [DisplayName("狀態")]
        public string Status { get; set; }

        [DisplayName("採購編號")]
        public string PurchaseId { get; set; }

        [DisplayName("規格")]
        public string Spec { get; set; }

        [DisplayName("關鍵備品")]
        public string IsKeySpare { get; set; }

        [DisplayName("市購品")]
        public string IsCommercial { get; set; }

        [DisplayName("適用機種")]
        public string Equipment { get; set; }

        [DisplayName("儲位")]
        public string Placement { get; set; }

        [DisplayName("數量")]
        public int Count { get; set; }

        [DisplayName("庫存")]
        public int Inventory { get; set; }

        [DisplayName("安全庫存")]
        public int SafetyCount { get; set; }

        [DisplayName("是否需檢驗")]
        public string IsNeedInspect { get; set; }

        [DisplayName("檢驗日期")]
        public string InspectDate { get; set; }

        [DisplayName("下次檢驗日期")]
        public string NextInspectDate { get; set; }

        [DisplayName("檢驗逾期")]
        public string IsOverdueInspect { get; set; }

        [DisplayName("暫停使用")]
        public string IsTemporary { get; set; }

        [DisplayName("備註")]
        public string Memo { get; set; }

        public int StatusId { get; set; }

    }
}
