using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SpareManagement.Models
{
    public class WirePanelEditViewModel
    {
        public string PartNo { get; set; }

        [DisplayName("名稱")]
        public string Name { get; set; }

        [DisplayName("序號")]
        public string SerialNo { get; set; }

        [DisplayName("檢驗日期")]
        public string InspectDate { get; set; }

        [DisplayName("暫停使用")]
        public bool IsTemporary { get; set; }

        [DisplayName("儲位")]
        public string Placement { get; set; }

    }
}
