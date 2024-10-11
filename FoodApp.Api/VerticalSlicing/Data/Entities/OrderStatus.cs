using System.Runtime.Serialization;

namespace FoodApp.Api.VerticalSlicing.Data.Entities
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "Accepted")]
        Accepted,

        [EnumMember(Value = "Rejected")]
        Rejected,

        [EnumMember(Value = "Cancelled")]
        Cancelled,

        [EnumMember(Value = "InProgress")]
        InProgress,

        [EnumMember(Value = "Delivered")]
        Delivered,

        [EnumMember(Value = "Completed")]
        Completed,


    }
}