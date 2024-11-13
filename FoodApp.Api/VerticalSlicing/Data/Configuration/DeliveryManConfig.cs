using FoodApp.Api.VerticalSlicing.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodApp.Api.VerticalSlicing.Data.Configuration
{
    public class DeliveryManConfig : IEntityTypeConfiguration<DeliveryMan>
    {
        public void Configure(EntityTypeBuilder<DeliveryMan> builder)
        {
            builder.Property(o => o.Status).HasConversion
             (
                 status => status.ToString(),
                 status => (DeliveryManStatus)Enum.Parse(typeof(DeliveryManStatus), status)
            );
        }
    }
}
