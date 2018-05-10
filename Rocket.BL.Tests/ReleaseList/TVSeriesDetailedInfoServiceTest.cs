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
using System.Linq;
using System.Linq.Expressions;

namespace Rocket.BL.Tests.ReleaseList
{
    [TestFixture]
    public class TVSeriesDetailedInfoServiceTest
    {
        private const int TVSeriesCount = 300;
        private TVSeriesDetailedInfoService _tvSeriesDetailedInfoService;
        private FakeDbTVSerialsData _fakeDbTVSerialsData;

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

            this._fakeDbTVSerialsData = new FakeDbTVSerialsData(100, 10, 10, TVSeriesCount);

            var mockDbTVSeriesRepository = new Mock<IDbTVSeriesRepository>();
            mockDbTVSeriesRepository.Setup(mock => mock.Get(It.IsAny<Expression<Func<DbTVSeries, bool>>>(), null, ""))
                .Returns((Expression<Func<DbTVSeries, bool>> filter,
                    Func<IQueryable<DbTVSeries>, IOrderedQueryable<DbTVSeries>> orderBy,
                    string includeProperties) => this._fakeDbTVSerialsData.TVSerials.Where(filter.Compile()));
            mockDbTVSeriesRepository.Setup(mock => mock.GetById(It.IsAny<int>()))
                .Returns((object id) => this._fakeDbTVSerialsData.TVSerials.Find(f => f.Id == (int)id));
            mockDbTVSeriesRepository.Setup(mock => mock.Insert(It.IsAny<DbTVSeries>()))
                .Callback((DbTVSeries f) => this._fakeDbTVSerialsData.TVSerials.Add(f));
            mockDbTVSeriesRepository.Setup(mock => mock.Update(It.IsAny<DbTVSeries>()))
                .Callback((DbTVSeries f) => this._fakeDbTVSerialsData.TVSerials.Find(d => d.Id == f.Id).Title = f.Title);
            mockDbTVSeriesRepository.Setup(mock => mock.Delete((int) It.IsAny<object>()))
                .Callback((object id) => this._fakeDbTVSerialsData.TVSerials
                    .Remove(this._fakeDbTVSerialsData.TVSerials.Find(f => f.Id == (int)id)));

            var mockTVSeriesUnitOfWork = new Mock<IUnitOfWork>();
            mockTVSeriesUnitOfWork.Setup(mock => mock.TVSeriesRepository)
                .Returns(() => mockDbTVSeriesRepository.Object);

            this._tvSeriesDetailedInfoService = new TVSeriesDetailedInfoService(mockTVSeriesUnitOfWork.Object);
        }

        /// <summary>
        /// Тест метода получения экземпляра сериала по заданному идентификатору.
        /// Сериал с передаваемым идентификатором существует
        /// </summary>
        /// <param name="id">Идентификатор сериал</param>
        [Test, Order(1)]
        public void GetExistedTVSeriesTest([Random(0, TVSeriesCount - 1, 5)] int id)
        {
            var expectedTVSeries = this._fakeDbTVSerialsData.TVSerials.Find(f => f.Id == id);

            var actualTVSeries = this._tvSeriesDetailedInfoService.GetTVSeries(id);

            actualTVSeries.Should().BeEquivalentTo(expectedTVSeries,
                options => options.ExcludingMissingMembers());
            actualTVSeries.Directors.Should().BeEquivalentTo(expectedTVSeries.Directors,
                options => options.ExcludingMissingMembers());
            actualTVSeries.Cast.Should().BeEquivalentTo(expectedTVSeries.Cast,
                options => options.ExcludingMissingMembers());
            actualTVSeries.Genres.Should().BeEquivalentTo(expectedTVSeries.Genres,
                options => options.ExcludingMissingMembers());
            actualTVSeries.Countries.Should().BeEquivalentTo(expectedTVSeries.Countries,
                options => options.ExcludingMissingMembers());
        }

        /// <summary>
        /// Тест метода получения экземпляра сериала по заданному идентификатору.
        /// Сериал с передаваемым идентификатором не существует
        /// </summary>
        /// <param name="id">Идентификатор сериала</param>
        [Test, Order(1)]
        public void GetNotExistedTVSeriesTest([Random(TVSeriesCount, TVSeriesCount + 300, 5)] int id)
        {
            var actualTVSeries = this._tvSeriesDetailedInfoService.GetTVSeries(id);

            actualTVSeries.Should().BeNull();
        }

        /// <summary>
        /// Тест метода добавления сериала в хранилище данных
        /// </summary>
        [Test, Repeat(5), Order(2)]
        public void AddTVSeriesTest()
        {
            var tvSeries = new FakeTVSerialsData(50, 10, 10, 0).TVSeriesFaker.Generate();
            tvSeries.Id = this._fakeDbTVSerialsData.TVSerials.Last().Id + 1;

            var actualId = this._tvSeriesDetailedInfoService.AddTVSeries(tvSeries);
            var actualTVSeries = this._tvSeriesDetailedInfoService.GetTVSeries(actualId);

            actualTVSeries.Should().BeEquivalentTo(tvSeries);
        }

        /// <summary>
        /// Тест метода обновления данных о сериале
        /// </summary>
        /// <param name="id">Идентификатор сериала для обновления</param>
        [Test, Order(2)]
        public void UpdateTVSeriesTest([Random(0, TVSeriesCount - 1, 5)] int id)
        {
            var tvSeries = this._tvSeriesDetailedInfoService.GetTVSeries(id);
            tvSeries.Title = new Bogus.Faker().Lorem.Word();

            this._tvSeriesDetailedInfoService.UpdateTVSeries(tvSeries);
            var actualTVSeries = this._fakeDbTVSerialsData.TVSerials.Find(f => f.Id == id);

            actualTVSeries.Title.Should().Be(tvSeries.Title);
        }

        /// <summary>
        /// Тест метода удаления сериала из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор сериала для удаления</param>
        [Test, Order(3)]
        public void DeleteTVSeriesTest([Random(0, TVSeriesCount - 1, 5)] int id)
        {
            this._tvSeriesDetailedInfoService.DeleteTVSeries(id);

            var actualTVSeries = this._fakeDbTVSerialsData.TVSerials.Find(tvSeries => tvSeries.Id == id);

            actualTVSeries.Should().BeNull();
        }

        /// <summary>
        /// Тест метода проверки наличия сериала в хранилище данных.
        /// Сериал существует
        /// </summary>
        /// <param name="id">Идентификатор сериала для поиска</param>
        [Test, Order(2)]
        public void TVSeriesExistsTest([Random(0, TVSeriesCount - 1, 5)] int id)
        {
            var titleToFind = this._fakeDbTVSerialsData.TVSerials.Find(tv => tv.Id == id).Title;

            var actual = this._tvSeriesDetailedInfoService
                .TVSeriesExists(tv => tv.Title == titleToFind);

            actual.Should().BeTrue();
        }

        /// <summary>
        /// Тест метода проверки наличия сериала в хранилище данных.
        /// Сериал не существует
        /// </summary>
        /// <param name="title">Название сериала для поиска</param>
        [Test, Order(2)]
        public void TVSeriesNotExistsTest([Values("1 1 1", "2 22 2", "", "4 word 4", "three words title")] string title)
        {
            var actual = this._tvSeriesDetailedInfoService
                .TVSeriesExists(tv => tv.Title == title);

            actual.Should().BeFalse();
        }
    }
}
