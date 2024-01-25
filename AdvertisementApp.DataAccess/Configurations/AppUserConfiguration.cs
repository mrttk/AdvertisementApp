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
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(p => p.Firstname).HasMaxLength(300).IsRequired();
            builder.Property(p => p.Surname).HasMaxLength(300).IsRequired();
            builder.Property(p => p.Username).HasMaxLength(300).IsRequired();
            builder.Property(p => p.PhoneNumber).HasMaxLength(20).IsRequired();
            builder.Property(p => p.Password).HasMaxLength(50).IsRequired();

            builder.HasOne(x => x.Gender).WithMany(x => x.AppUsers).HasForeignKey(x => x.GenderId);
        }
    }
}
