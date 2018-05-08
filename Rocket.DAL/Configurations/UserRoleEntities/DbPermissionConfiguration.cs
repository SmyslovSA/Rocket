﻿using System.Data.Entity.ModelConfiguration;
using Rocket.DAL.Common.DbModels.DbUserRole;

namespace Rocket.DAL.Configurations.UserRoleEntities
{
    // Конфигурация хранения данных прав доступа (пермишенов)
    public class DbPermissionConfiguration : EntityTypeConfiguration<DbPermission>
    {
        public DbPermissionConfiguration()
        {
            ToTable("t_permission")
                .HasKey(t => t.Id)
                .Property(t => t.Id)
                .HasColumnName("prm_id");

            Property(t => t.Description)
                .IsOptional()
                .HasColumnName("description")
                .HasMaxLength(250)
                .IsVariableLength();

            Property(t => t.ValueName)
                .IsRequired()
                .HasColumnName("value_name")
                .HasMaxLength(50)
                .IsVariableLength();
        }
    }
}