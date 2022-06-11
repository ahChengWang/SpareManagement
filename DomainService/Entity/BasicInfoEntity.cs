using System;

namespace SpareManagement.DomainService.Entity
{
    public class BasicInfoEntity
    {

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string PartNo { get; set; }

        public string Spec { get; set; }

        public string PurchaseId { get; set; }

        public bool IsSpecial { get; set; }

        public bool IsKeySpare { get; set; }

        public bool IsCommercial { get; set; }

        public string Equipment { get; set; }

        public string UseLocation { get; set; }

        public int PurchaseDelivery { get; set; }

        public int SafetyCount { get; set; }

        public bool IsNeedInspect { get; set; }

        //public DateTime InspectDate { get; set; }

        public int InspectCycle { get; set; }

        public string Placement { get; set; }

        public string CreateUser { get; set; }

        public string Manager { get; set; }

        public string Memo { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
