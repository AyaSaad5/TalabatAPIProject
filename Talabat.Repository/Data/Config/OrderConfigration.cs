using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enitites.Order_Aggregate;

namespace Talabat.Repository.Data.Config
{
    internal class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress, NP => NP.WithOwner());
      
            builder.Property(O => O.Status)
                   .HasConversion(OStatus => OStatus.ToString(),
                                  OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus),OStatus));

            builder.Property(O => O.SubTotal)
                .HasColumnType("decimal(18,2");

            builder.HasMany(O => O.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
