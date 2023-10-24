using System;
using System.Collections.Generic;

namespace SpareManagement.DomainService.Entity
{
    public class ReleaseEntity
    {
        public List<ReleaseDetailEntity> ReleaseList { get; set; }

        public string PartNo1 { get; set; }

        public int Count1 { get; set; }

        public string Node1 { get; set; }

        public string PartNo2 { get; set; }

        public int Count2 { get; set; }

        public string Node2 { get; set; }

        public string PartNo3 { get; set; }

        public int Count3 { get; set; }

        public string Node3 { get; set; }

        public string PartNo4 { get; set; }

        public int Count4 { get; set; }

        public string Node4 { get; set; }

        public string PartNo5 { get; set; }

        public int Count5 { get; set; }

        public string Node5 { get; set; }
        public DateTime? CreateDate { get; set; }

        public string CreateUser { get; set; }

        public string Memo { get; set; }

    }


    public class ReleaseGoodsEntity
    {
        public string ColName { get; set; }

        public string PartNo { get; set; }

        public int Count { get; set; }

        public string Node { get; set; }
    }

    public class ReleaseDetailEntity
    {
        public string PartNo { get; set; }

        public int Quantity { get; set; }
    }
}
