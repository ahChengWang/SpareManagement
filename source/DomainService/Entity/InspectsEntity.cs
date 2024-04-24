using SpareManagement.Enum;
using System;

namespace SpareManagement.DomainService.Entity
{
    public class InspectsEntity
    {
        public string PartNo { get; set; }
        public string Name { get; set; }
        public string Spec { get; set; }
        public string SerialNo { get; set; }
        public string PurchaseId { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime NextInspectDate { get; set; }
        public int CategoryId { get; set; }

        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
