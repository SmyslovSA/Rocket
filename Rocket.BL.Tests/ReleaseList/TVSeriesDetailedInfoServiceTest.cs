using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Rocket.BL.Services.ReleaseList;
using Rocket.BL.Tests.ReleaseList.FakeData;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.Repositories;
using Rocket.DAL.Common.UoW;
using System;
using System.Linq;
using System.Linq.Expressions;
using Rocket.BL.Common.Models.ReleaseList;

namespace Rocket.BL.Tests.ReleaseList
{
    [TestFixture]
    public class TvSeriesDetailedInfoServiceTest
    {
        private TvSeriesDetailedInfoService _tvSeriesDetailedInfoService;
        private FakeDb _fakeDb;

        /// <summary>
        /// Осуществляет все необходимые настройки для тестов.
        /// AutoMapper, Bogus (FakeDbFilmData), Moq
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => { cfg.AddProfiles("Rocket.BL.Common"); });
            _fakeDb = new FakeDb();

            var mockTvSeriasRepository = new Mock<IBaseRepository<TvSeriasEntity>>();
            mockTvSeriasRepository
                .Setup(mock => mock.Get(It.IsAny<Expression<Func<TvSeriasEntity, bool>>>(), null, It.IsAny<string>()))
                .Returns(
                    (Expression<Func<TvSeriasEntity, bool>> filter,
                    Func<IQueryable<TvSeriasEntity>, IOrderedQueryable<TvSeriasEntity>> orderBy,
                    string includeProperties)
                        => _fakeDb.FakeTvSeriasEntities.TvSeriasEntities.Where(filter.Compile()));
            mockTvSeriasRepository.Setup(mock => mock.GetById(It.IsAny<int>()))
                .Returns((int id) => _fakeDb.FakeTvSeriasEntities.TvSeriasEntities.Find(f => f.Id == id));
            mockTvSeriasRepository.Setup(mock => mock.Insert(It.IsAny<TvSeriasEntity>()))
                .Callback((TvSeriasEntity f) => _fakeDb.FakeTvSeriasEntities.TvSeriasEntities.Add(f));
            mockTvSeriasRepository.Setup(mock => mock.Update(It.IsAny<TvSeriasEntity>()))
                .Callback((TvSeriasEntity f) => _fakeDb.FakeTvSeriasEntities.TvSeriasEntities.Find(d => d.Id == f.Id).TitleEn = f.TitleEn);
            mockTvSeriasRepository.Setup(mock => mock.Delete(It.IsAny<int>()))
                .Callback((int id) => _fakeDb.FakeTvSeriasEntities.TvSeriasEntities
                    .Remove(_fakeDb.FakeTvSeriasEntities.TvSeriasEntities.Find(f => f.Id == id)));

            var mockEpisodeRepository = new Mock<IBaseRepository<EpisodeEntity>>();
            mockEpisodeRepository
                .Setup(mock => mock.Get(It.IsAny<Expression<Func<EpisodeEntity, bool>>>(), null, string.Empty))
                .Returns(
                    (Expression<Func<EpisodeEntity, bool>> filter,
                            Func<IQueryable<TvSeriasEntity>, IOrderedQueryable<TvSeriasEntity>> orderBy,
                            string includeProperties)
                        => _fakeDb.FakeEpisodeEntities.EpisodeEntities.Where(filter.Compile()));

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(mock => mock.TvSeriasRepository)
                .Returns(() => mockTvSeriasRepository.Object);
            mockUnitOfWork.Setup(mock => mock.EpisodeRepository)
                .Returns(() => mockEpisodeRepository.Object);

            _tvSeriesDetailedInfoService = new TvSeriesDetailedInfoService(mockUnitOfWork.Object);
        }

        /// <summary>
        /// Тест метода получения экземпляра сериала по заданному идентификатору.
        /// Сериал с передаваемым идентификатором существует
        /// </summary>
        /// <param name="id">Идентификатор сериал</param>
        [Test, Order(1)]
        public void GetExistedTVSeriesTest([Random(0, 99, 5)] int id)
        {
            var expectedTvSeries = _fakeDb.FakeTvSeriasEntities.TvSeriasEntities.Find(f => f.Id == id);

            var actualTvSeries = _tvSeriesDetailedInfoService.GetTvSeries(id);

            actualTvSeries.Should().BeEquivalentTo(expectedTvSeries,
                options => options.ExcludingMissingMembers());
            actualTvSeries.ListPerson.Should().BeEquivalentTo(expectedTvSeries.ListPerson,
                options => options.ExcludingMissingMembers());
            actualTvSeries.Genres.Should().BeEquivalentTo(expectedTvSeries.ListGenreEntity,
                options => options.ExcludingMissingMembers());
        }

        /// <summary>
        /// Тест метода получения экземпляра сериала по заданному идентификатору.
        /// Сериал с передаваемым идентификатором не существует
        /// </summary>
        /// <param name="id">Идентификатор сериала</param>
        [Test, Order(1)]
        public void GetNotExistedTVSeriesTest([Random(100, 300, 5)] int id)
        {
            var actualTvSeries = _tvSeriesDetailedInfoService.GetTvSeries(id);

            actualTvSeries.Should().BeNull();
        }

        /// <summary>
        /// Тест метода добавления сериала в хранилище данных
        /// </summary>
        [Test, Repeat(5), Order(2)]
        public void AddTVSeriesTest()
        {
            var tvSeries = Mapper.Map<TVSeries>(_fakeDb.FakeTvSeriasEntities.TvSeriasFaker.Generate());

            var actualId = _tvSeriesDetailedInfoService.AddTvSeries(tvSeries);
            var actualTvSeries = _tvSeriesDetailedInfoService.GetTvSeries(actualId);

            actualTvSeries.Should().BeEquivalentTo(tvSeries);
        }

        /// <summary>
        /// Тест метода обновления данных о сериале
        /// </summary>
        /// <param name="id">Идентификатор сериала для обновления</param>
        [Test, Order(2)]
        public void UpdateTVSeriesTest([Random(0, 99, 5)] int id)
        {
            var tvSeries = _tvSeriesDetailedInfoService.GetTvSeries(id);
            tvSeries.TitleEn = new Bogus.Faker().Lorem.Word();

            _tvSeriesDetailedInfoService.UpdateTvSeries(tvSeries);
            var actualTvSeries = _fakeDb.FakeTvSeriasEntities.TvSeriasEntities.Find(f => f.Id == id);

            actualTvSeries.TitleEn.Should().Be(tvSeries.TitleEn);
        }

        /// <summary>
        /// Тест метода удаления сериала из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор сериала для удаления</param>
        [Test, Order(3)]
        public void DeleteTVSeriesTest([Random(0, 99, 5)] int id)
        {
            _tvSeriesDetailedInfoService.DeleteTvSeries(id);

            var actualTVSeries = _fakeDb.FakeTvSeriasEntities.TvSeriasEntities.Find(tvSeries => tvSeries.Id == id);

            actualTVSeries.Should().BeNull();
        }

        /// <summary>
        /// Тест метода проверки наличия сериала в хранилище данных.
        /// Сериал существует
        /// </summary>
        /// <param name="id">Идентификатор сериала для поиска</param>
        [Test, Order(2)]
        public void TVSeriesExistsTest([Random(0, 99, 5)] int id)
        {
            var titleToFind = _fakeDb.FakeTvSeriasEntities.TvSeriasEntities.Find(tv => tv.Id == id).TitleEn;

            var actual = _tvSeriesDetailedInfoService.TvSeriesExists(tv => tv.TitleEn == titleToFind);

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
            var actual = _tvSeriesDetailedInfoService.TvSeriesExists(tv => tv.TitleEn == title);

            actual.Should().BeFalse();
        }
    }
}