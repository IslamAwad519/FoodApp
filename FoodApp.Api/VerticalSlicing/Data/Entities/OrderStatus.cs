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

        [EnumMember(Value = "Ready")]
        Ready,

        [EnumMember(Value = "Completed")]
        Completed,


    }
    public enum OrderStatusTrip
    {
        [EnumMember(Value = "OnTrip")]
        OnTrip,

        [EnumMember(Value = "OnMyWayToCustomer")]
        OnMyWayToCustomer,

        [EnumMember(Value = "ArrivedToCustomer")]
        ArrivedToCustomer,

        [EnumMember(Value = "Delivered")]
        Delivered,
    }
}