using SpareManagement.Enum;
using System;

namespace SpareManagement.DomainService.Entity
{
    public class HistoryEntity
    {
        public int CategoryId { get; set; }
        public string PartNo { get; set; }
        public StatusEnum Status { get; set; }
        public int Quantity { get; set; }
        public string EmpName { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Memo { get; set; }

    }
}
