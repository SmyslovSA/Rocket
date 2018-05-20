using AutoMapper;
using Rocket.BL.Common.Models.PersonalArea;
using Rocket.BL.Common.Services.PersonalArea;
using Rocket.DAL.Common.DbModels.DbPersonalArea;
using Rocket.DAL.Common.UoW;
using System.Linq;

namespace Rocket.BL.Services.PersonalArea
{
    public class ChangeGenreManagerService : BaseService, IGenreManager
    {
        public ChangeGenreManagerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public bool AddGenre(SimpleUser model, string category, string genre)
        {
            //проверка на валидность данных
            if (model != null && string.IsNullOrEmpty(category) && string.IsNullOrEmpty(genre))
            {
                var user = Mapper.Map<DbAuthorisedUser>(model);
                user.Genres.Add(new DbGenre
                {
                    Name = genre,
                    DbCategory = new DbCategory
                    {
                        Name = category
                    }
                });
                _unitOfWork.UserAuthorisedRepository.Update(user);
                _unitOfWork.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeleteGenre(SimpleUser model, string category, string genre)
        {
            if (model != null && string.IsNullOrEmpty(category) && string.IsNullOrEmpty(genre))
            {
                if (_unitOfWork.GenreRepository.Get().FirstOrDefault(c => c.Name == genre) != null)
                {
                    var ganre = _unitOfWork.GenreRepository.Get().FirstOrDefault(c => c.Name == genre);
                    _unitOfWork.GenreRepository.Delete(ganre);
                    _unitOfWork.SaveChanges();
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}