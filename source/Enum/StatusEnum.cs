using System.ComponentModel;

namespace SpareManagement.Enum
{
    public enum StatusEnum
    {
        [Description("入庫")]
        Stock = 1,

        [Description("領用")]
        UnStock = 2,

        [Description("檢驗")]
        Inspecting = 3,

        [Description("送修")]
        Fixing = 4,
    }
}
