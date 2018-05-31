using AutoMapper;
using Rocket.BL.Common.Models.PersonalArea;
using Rocket.BL.Common.Services.PersonalArea;
using Rocket.DAL.Common.DbModels.DbPersonalArea;
using Rocket.DAL.Common.UoW;
using System.Linq;
using Rocket.DAL.Common.DbModels.Parser;

namespace Rocket.BL.Services.PersonalArea
{
    public class ChangeGenreManagerService : BaseService, IGenreManager
    {
        public ChangeGenreManagerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public bool AddGenre(int idUser, string category, string genre)
        {
            //проверка на валидность данных
            if (string.IsNullOrEmpty(category) && string.IsNullOrEmpty(genre))
            {
                if (_unitOfWork.UserRepository.Get(f => f.Id == idUser).FirstOrDefault() != null)
                {
                    var modelUser = _unitOfWork.UserRepository.Get(f => f.Id == idUser).FirstOrDefault();
                    var user = Mapper.Map<DbAuthorisedUser>(modelUser);
                    user.Genres.Add(new GenreEntity
                    {
                        Name = genre,
                        Category = new CategoryEntity()
                        {
                            Name = category
                        }
                    });
                    _unitOfWork.UserAuthorisedRepository.Update(user);
                    _unitOfWork.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public bool DeleteGenre( int idUser, string category, string genre)
        {
            if (string.IsNullOrEmpty(category) && string.IsNullOrEmpty(genre))
            {
                if (_unitOfWork.GenreRepository.Get().FirstOrDefault(c => c.Name == genre) != null)
                {
                    var modelUser = _unitOfWork.UserRepository.Get(f => f.Id == idUser).FirstOrDefault();
                     var gen = _unitOfWork.GenreRepository.Get().Where(f => f.Users.Contains(modelUser)).FirstOrDefault();

                    _unitOfWork.GenreRepository.Delete(gen);
                    _unitOfWork.SaveChanges();
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}