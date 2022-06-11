﻿namespace SpareManagement.Repository.Dao
{
    public class ExpendablesDao
    {
        public string PartNo { get; set; }

        public string Name { get; set; }

        public string PurchaseId { get; set; }

        public string Spec { get; set; }

        public bool IsKeySpare { get; set; }

        public bool IsCommercial { get; set; }

        public string Placement { get; set; }

        public int Inventory { get; set; }

        public int SafetyCount { get; set; }

        public string Memo { get; set; }
    }
}
