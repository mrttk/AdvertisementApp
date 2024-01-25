using AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.DataAccess.Configurations
{
    public class ProvidedServiceConfiguration : IEntityTypeConfiguration<ProvidedService>
    {
        public void Configure(EntityTypeBuilder<ProvidedService> builder)
        {
            builder.Property(p => p.Title).HasMaxLength(300).IsRequired();
            builder.Property(p => p.Description).HasColumnType("ntext").IsRequired();
            builder.Property(p => p.ImagePath).HasMaxLength(500).IsRequired();
            builder.Property(p => p.CreatedDate).HasDefaultValueSql("getdate()").IsRequired();
        }
    }
}
