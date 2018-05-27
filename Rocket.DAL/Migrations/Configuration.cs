using System.Collections.Generic;
using System.Linq;
using Rocket.DAL.Common.DbModels.DbUserRole;
using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.DbModels.User;
using Rocket.DAL.Migrations.InitialDataCreators.User;
using Rocket.DAL.Migrations.InitialDataCreators.UserRole;

namespace Rocket.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Rocket.DAL.Context.RocketContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Rocket.DAL.Context.RocketContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //todo insert fake users data here...

            // Добавление в репозиторий первоначальной информации о половой принадлежности пользователя.
            List<DbGender> initialGenderDatas = new DbGendersCreator().Items.ToList(); ;
            //if (!context.DbGenders.Any())
            //{
                initialGenderDatas.ForEach(gender => context.DbGenders.Add(gender));
                context.SaveChanges();
            //}

            // Добавление в репозиторий первоначальной информации о статусах аккаунта пользователей.
            List<DbAccountStatus> initialAccountStatusDatas = new DbAccountStatusesCreator().Items; ;
            if (!context.DbAccountStatuses.Any())
            {
                initialAccountStatusDatas.ForEach(accountStatus => context.DbAccountStatuses.Add(accountStatus));
                context.SaveChanges();
            }

            // Добавление в репозиторий первоначальной информации об уровне аккаунта пользователей.
            List<DbAccountLevel> initialAccountLevelDatas = new DbAccountLevelsCreator().Items; ;
            if (!context.DbAccountLevels.Any())
            {
                initialAccountLevelDatas.ForEach(accountLevel => context.DbAccountLevels.Add(accountLevel));
                context.SaveChanges();
            }

            // Добавление в репозиторий первоначальной информации о странах мира (всего 251 наименование стран взято из международного
            // классификатора ISO).
            List<DbCountry> initialCountryDatas = new DbCountriesCreator().Items; ; ;
            if (!context.DbCountries.Any())
            {
                initialCountryDatas.ForEach(country => context.DbCountries.Add(country));
                context.SaveChanges();
            }

            // Добавление в репозиторий первоначальной информации об основных языках мира (всего 13 основных мировых разговорных языков).
            List<DbLanguage> initialLanguageDatas = new DbLanguagesCreator().Items;
            if (!context.DbLanguages.Any())
            {
                initialLanguageDatas.ForEach(languages => context.DbLanguages.Add(languages));
                context.SaveChanges();
            }

            // Добавление в репозиторий первоначальной информации о том, как обращаться к пользователю (Mr. и Ms.).
            List<DbHowToCall> initialHowToCallDatas = new DbHowToCallCreator().Items; ;
            if (!context.DbHowToCalls.Any())
            {
                initialHowToCallDatas.ForEach(howToCall => context.DbHowToCalls.Add(howToCall));
                context.SaveChanges();
            }

            // Добавление в репозиторий первоначальной информации о ролях пользователей.
            List<DbRole> initialRolesdatas = new DbUserRolesCreator().Items; ;
            if (!context.DbRoles.Any())
            {
                initialRolesdatas.ForEach(role => context.DbRoles.Add(role));
                context.SaveChanges();
            }

            // Добавление в репозиторий первоначальной тестовой информации о пользователей.
            if (!context.DbUsers.Any())
            {
                var initialUserdatas = new FakeDbUsersCreator(initialAccountLevelDatas, initialAccountStatusDatas,
                    initialCountryDatas, initialGenderDatas, initialHowToCallDatas, initialLanguageDatas).Users;

                initialUserdatas.ForEach(user => context.DbUsers.Add(user));
                context.SaveChanges();
            }
        }
    }
}
