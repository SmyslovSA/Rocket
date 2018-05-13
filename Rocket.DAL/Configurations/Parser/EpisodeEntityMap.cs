﻿using Rocket.DAL.Common.DbModels.Parser;
using System.Data.Entity.ModelConfiguration;

namespace Rocket.DAL.Configurations.Parser
{
    public class EpisodeEntityMap : EntityTypeConfiguration<EpisodeEntity>
    {
        public EpisodeEntityMap()
        {
            ToTable("Episode", "seria")
                .HasKey(p => p.Id);

            Property(p => p.ReleaseDateRu)
                .IsRequired()
                .HasColumnName("ReleaseDateRu");

            Property(p => p.ReleaseDateEn)
                .IsRequired()
                .HasColumnName("ReleaseDateEn");

            Property(p => p.Number)
                .IsRequired()
                .HasColumnName("Number");

            Property(p => p.TitleRu)
                .IsRequired()
                .HasColumnName("TitleRu")
                .HasMaxLength(250);

            Property(p => p.TitleEn)
                .IsRequired()
                .HasColumnName("TitleEn")
                .HasMaxLength(250);

            Property(p => p.Duration)
                .IsOptional()
                .HasColumnName("Duration");

            Property(p => p.UrlForEpisodeSource)
                .IsRequired()
                .HasColumnName("UrlForEpisodeSource")
                .IsMaxLength();

            Property(p => p.SeasonId)
                .IsRequired()
                .HasColumnName("SeasonId");

        }
    }
}
