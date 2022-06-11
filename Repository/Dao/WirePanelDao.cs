using SpareManagement.Enum;
using System;

namespace SpareManagement.Repository.Dao
{
    public class WirePanelDao
    {
        public string PartNo { get; set; }

        public string Name { get; set; }

        public string SerialNo { get; set; }

        public StatusEnum Status { get; set; }

        public string PurchaseId { get; set; }

        public string Spec { get; set; }

        public bool IsKeySpare { get; set; }

        public bool IsCommercial { get; set; }

        public string Equipment { get; set; }

        public string Placement { get; set; }

        public int Inventory { get; set; }

        public int SafetyCount { get; set; }

        public bool IsNeedInspect { get; set; }

        public DateTime? InspectDate { get; set; }

        public DateTime? NextInspectDate { get; set; }

        public bool IsOverdueInspect { get; set; }

        public bool IsTemporary { get; set; }

        public string Memo { get; set; }
    }
}
