using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Rocket.BL.Common.Models.Pagination;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.BL.Services.ReleaseList;
using Rocket.BL.Tests.ReleaseList.FakeData;
using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.Repositories.ReleaseList;
using Rocket.DAL.Common.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Rocket.BL.Tests.ReleaseList
{
    /// <summary>
    /// Unit-тесты для сервиса <see cref="GuestReleaseListService"/>
    /// </summary>
    [TestFixture]
    public class GuestReleaseListServiceTest
    {
        private const int FakeCount = 150;
        private GuestReleaseListService _guestReleaseListService;
        private List<DbBaseRelease> _fakeDbReleases;

        /// <summary>
        /// Осуществляет все необходимые настройки для тестов.
        /// AutoMapper, Bogus (FakeDbFilmData), Moq
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles("Rocket.BL.Common");
            });
            this._fakeDbReleases = new List<DbBaseRelease>(new FakeDbFilmsData(100, 10, 10, FakeCount).Films);
            this._fakeDbReleases.AddRange(new FakeDbTVSerialsData(100, 10, 10, FakeCount).FakeDbSeasonsData.FakeDbEpisodesData.Episodes);
            this._fakeDbReleases.AddRange(new FakeDbMusicData(100, 10, FakeCount).Music);

            var mockDbReleaseRepository = new Mock<IDbReleaseRepository>();
            mockDbReleaseRepository.Setup(mock => 
                    mock.GetPage(
                        It.IsInRange<int>(1, 1000, Range.Inclusive),
                        It.IsInRange<int>(1, 100000, Range.Inclusive),
                        It.IsAny<Expression<Func<DbBaseRelease, bool>>>(), 
                        It.IsAny<Func<IQueryable<DbBaseRelease>, IOrderedQueryable<DbBaseRelease>>>(), string.Empty))
                .Returns((int pageSize, int pageNumber, 
                    Expression<Func<DbBaseRelease, bool>> filter,
                    Func<IQueryable<DbBaseRelease>, IOrderedQueryable<DbBaseRelease>> orderBy,
                    string includeProperties) =>
                        orderBy(this._fakeDbReleases.Where(filter.Compile()).AsQueryable())
                        .Skip(pageSize * (pageNumber - 1)).Take(pageSize));
            mockDbReleaseRepository.Setup(mock => mock.ItemsCount(It.IsAny<Expression<Func<DbBaseRelease, bool>>>()))
                .Returns((Expression<Func<DbBaseRelease, bool>> filter) =>
                {
                    if (filter != null)
                    {
                        return _fakeDbReleases.AsQueryable().Count(filter);
                    }
                    
                    return this._fakeDbReleases.Count;
                });

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(mock => mock.ReleaseRepository)
                .Returns(() => mockDbReleaseRepository.Object);

            this._guestReleaseListService = new GuestReleaseListService(mockUnitOfWork.Object);
        }

        /// <summary>
        /// Тест метода получения страницы релизов с заданными
        /// размером и номером страницы
        /// </summary>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="pageNumber">Номер страницы</param>
        [Test, Combinatorial]
        public void GetPublishedReleasesPageTest([Values(4, 15, 43)] int pageSize,
            [Values(1, 20, 100000)] int pageNumber)
        {
            var expectedPage = new ReleasesPageInfo();
            expectedPage.TotalItemsCount = this._fakeDbReleases.Count(r => r.ReleaseDate <= DateTime.Now);
            expectedPage.TotalPagesCount = (int)Math.Ceiling((double)expectedPage.TotalItemsCount / pageSize);
            expectedPage.PageItems = Mapper.Map<IEnumerable<BaseRelease>>(this._fakeDbReleases
                .OrderByDescending(r => r.ReleaseDate)
                .Where(r => r.ReleaseDate <= DateTime.Now)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize));
            var actualPage = this._guestReleaseListService.GetPublishedReleasesPage(pageSize, pageNumber);

            actualPage.Should().BeEquivalentTo(expectedPage);
        }

        /// <summary>
        /// Тест метода получения страницы релизов с заданными
        /// размером и номером страницы
        /// </summary>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="pageNumber">Номер страницы</param>
        [Test, Combinatorial]
        public void GetFutureReleasesPageTest([Values(6, 20, 77)] int pageSize,
            [Values(1, 132, 100000)] int pageNumber)
        {
            var expectedPage = new ReleasesPageInfo();
            expectedPage.TotalItemsCount = this._fakeDbReleases.Count(r => r.ReleaseDate > DateTime.Now);
            expectedPage.TotalPagesCount = (int)Math.Ceiling((double)expectedPage.TotalItemsCount / pageSize);
            expectedPage.PageItems = Mapper.Map<IEnumerable<BaseRelease>>(this._fakeDbReleases
                .OrderBy(r => r.ReleaseDate)
                .Where(r => r.ReleaseDate > DateTime.Now)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize));
            var actualPage = this._guestReleaseListService.GetFutureReleasesPage(pageSize, pageNumber);

            actualPage.Should().BeEquivalentTo(expectedPage);
        }
    }
}
