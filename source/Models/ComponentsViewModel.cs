using System.Collections.Generic;
using System.ComponentModel;

namespace SpareManagement.Models
{
    public class ComponentsListViewModel
    {
        public List<ComponentsViewModel> Details { get; set; }
    }

    public class ComponentsViewModel
    {
        [DisplayName("物料編號")]
        public string PartNo { get; set; }

        [DisplayName("名稱")]
        public string Name { get; set; }

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

        [DisplayName("庫存")]
        public int Inventory { get; set; }

        [DisplayName("安全庫存")]
        public int SafetyCount { get; set; }

        [DisplayName("備註")]
        public string Memo { get; set; }

    }
}
