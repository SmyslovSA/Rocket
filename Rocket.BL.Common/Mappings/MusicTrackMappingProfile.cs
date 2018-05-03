﻿using AutoMapper;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.DAL.Common.DbModels;

namespace Rocket.BL.Common.Mappings
{
	public class MusicTrackMappingProfile : Profile
	{
		public MusicTrackMappingProfile()
		{
			CreateMap<MusicTrack, DbMusicTrack>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
				.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
				.ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
				.ReverseMap();
		}
	}
}
