using AutoMapper;
using Rocket.BL.Common.DtoModels.ReleaseList;
using Rocket.BL.Common.Models.Pagination;
using Rocket.BL.Common.Services.ReleaseList;
using Rocket.DAL.Common.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using Rocket.DAL.Common.DbModels.Parser;

namespace Rocket.BL.Services.ReleaseList
{
    public class EpisodeService : BaseService, IEpisodeService
    {
        public EpisodeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public EpisodeFullDto GetEpisodesById(int id)
        {
            var episode = _unitOfWork.EpisodeRepository
                .Get(f => f.Id == id, includeProperties: $"{nameof(EpisodeEntity.Season)}")?.FirstOrDefault();

            if (episode == null)
            {
                return null;
            }

            episode.Season.TvSeries = _unitOfWork.TvSeriasRepository.GetById(episode.Season.TvSeriesId);
            return Mapper.Map<EpisodeFullDto>(episode);
        }

        public PageInfo<EpisodeDto> GetNewEpisodesPage(int pageSize, int pageNumber)
        {
            var pageInfo = new PageInfo<EpisodeDto>();
            pageInfo.TotalItemsCount = _unitOfWork.EpisodeRepository.ItemsCount(e => e.ReleaseDateRu <= DateTime.Now);
            pageInfo.TotalPagesCount = (int)Math.Ceiling((double)pageInfo.TotalItemsCount / pageSize);
            var episodes = _unitOfWork.EpisodeRepository.GetPage(
                    pageSize,
                    pageNumber,
                    f => f.ReleaseDateRu <= DateTime.Now,
                    o => o.OrderByDescending(e => e.ReleaseDateRu),
                    $"{nameof(EpisodeEntity.Season)}");

            foreach (var entity in episodes)
            {
                entity.Season.TvSeries = _unitOfWork.TvSeriasRepository.GetById(entity.Season.TvSeriesId);
            }

            pageInfo.PageItems = Mapper.Map<IEnumerable<EpisodeDto>>(episodes);
            return pageInfo;
        }

        public PageInfo<EpisodeDto> GetScheduleEpisodesPage(int pageSize, int pageNumber)
        {
            var pageInfo = new PageInfo<EpisodeDto>();
            pageInfo.TotalItemsCount = _unitOfWork.EpisodeRepository.ItemsCount(e => e.ReleaseDateRu > DateTime.Now);
            pageInfo.TotalPagesCount = (int)Math.Ceiling((double)pageInfo.TotalItemsCount / pageSize);
            var episodes = _unitOfWork.EpisodeRepository.GetPage(
                pageSize,
                pageNumber,
                f => f.ReleaseDateRu > DateTime.Now,
                o => o.OrderBy(e => e.ReleaseDateRu),
                $"{nameof(EpisodeEntity.Season)}");

            foreach (var entity in episodes)
            {
                entity.Season.TvSeries = _unitOfWork.TvSeriasRepository.GetById(entity.Season.TvSeriesId);
            }

            pageInfo.PageItems = Mapper.Map<IEnumerable<EpisodeDto>>(episodes);
            return pageInfo;
        }
    }
}
