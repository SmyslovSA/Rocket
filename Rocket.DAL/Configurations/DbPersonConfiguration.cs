﻿using Rocket.DAL.Common.DbModels;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations
{
    /// <summary>
    /// Конфигурация хранения данных о человеке (актёре, режиссёре)
    /// </summary>
    public class DbPersonConfiguration : EntityTypeConfiguration<DbPerson>
    {
        public DbPersonConfiguration()
        {
            ToTable("Persons")
                .HasKey(p => p.Id)
                .Property(p => p.Id)
                .HasColumnName("Id");

            Property(p => p.FullName)
                .IsRequired()
                .HasColumnName("FullName")
                .HasMaxLength(50);
        }
    }
}
