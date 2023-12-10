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
    internal class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasOne(s => s.specializations).WithMany();
            builder.HasMany(d=>d.patients).WithMany(p=>p.Doctors);
            builder.Property(d => d.FullName).IsRequired();
            builder.Property(d=>d.Email).IsRequired();
          


        }
    }
}
