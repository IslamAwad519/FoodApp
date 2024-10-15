using FoodApp.Api.VerticalSlicing.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodApp.Api.VerticalSlicing.Data.Configuration
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.status).HasConversion
                (
                    status => status.ToString(),
                    status => (OrderStatus)Enum.Parse(typeof(OrderStatus), status)
               );
            
            builder.Property(o => o.StatusTrip).HasConversion
                (
                    status => status.ToString(),
                    status => (OrderStatusTrip)Enum.Parse(typeof(OrderStatusTrip), status)
               );


            //builder.Property(o => o.StatusTrip).HasConversion
            //   (
            //       StatusTrip => StatusTrip.ToString(),
            //       StatusTrip => (OrderStatusTrip)Enum.Parse(typeof(OrderStatusTrip), StatusTrip)
            //  );


            builder.Property(o => o.TotalPrice)
                .HasColumnType("decimal(12,2)");
        }
    }
}
