using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
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
        private FakeDbFilmsData _fakeDbFilmsData;
        private FakeDbTVSerialsData _fakeDbTVSerialsData;
        private FakeDbMusicData _fakeDbMusicData;
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
            this._fakeDbFilmsData = new FakeDbFilmsData(100, 10, 10, FakeCount);
            this._fakeDbTVSerialsData = new FakeDbTVSerialsData(100, 10, 10, FakeCount);
            this._fakeDbMusicData = new FakeDbMusicData(100, 10, FakeCount);
            this._fakeDbReleases = new List<DbBaseRelease>(this._fakeDbFilmsData.Films);
            this._fakeDbReleases.AddRange(this._fakeDbTVSerialsData.FakeDbSeasonsData.FakeDbEpisodesData.Episodes);
            this._fakeDbReleases.AddRange(this._fakeDbMusicData.Music);

            var mockDbReleaseRepository = new Mock<IDbReleaseRepository>();
            mockDbReleaseRepository.Setup(mock => mock.GetPage(
                It.IsInRange<int>(1, FakeCount * 3, Range.Inclusive),
                It.IsInRange<int>(1, FakeCount * 3, Range.Inclusive),
                It.IsAny<Expression<Func<DbBaseRelease, bool>>>(),
                It.IsAny< Func<IQueryable<DbBaseRelease>, IOrderedQueryable<DbBaseRelease>>>(),
                ""))
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
                        return this._fakeDbReleases.AsQueryable().Count(filter);
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
        [Test]
        public void GetPublishedReleasesPageTest([Random(4, 100, 5)] int pageSize,
            [Random(1, 20, 5)] int pageNumber)
        {
            var actualPage = this._guestReleaseListService.GetPublishedReleasesPage(pageSize, pageNumber);
        }
    }
}
