using core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.config
{
    internal class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasOne(d => d.Doctor).WithMany();
        }
    }
}
