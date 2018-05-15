using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Rocket.BL.Common.Services.User;
using Swashbuckle.Swagger.Annotations;

namespace Rocket.Web.Controllers.User
{
    /// <summary>
    /// Контроллер WebApi работы с пользователями.
    /// </summary>
    [RoutePrefix("/users")]
    public class UsersController : ApiController
    {
        private readonly IUserManagementService _userManagementService;

        public UsersController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        /// <summary>
        /// Возвращает всех пользователей.
        /// </summary>
        /// <returns>Все пользователи хранилища данных.</returns>
        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAllUsers()
        {
            var users = _userManagementService.GetAllUsers();

            if (users == null)
            {
                return NotFound();
            }

            if (users.Count == 0)
            {
                return NotFound();
            }

            return Ok(users);
        }

        /// <summary>
        /// Возвращает пользователя с конкретным
        /// уникальным идентификатором.
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя.</param>
        /// <returns>Пользователь хранилища.</returns>
        [HttpGet]
        [Route("{id:int:min(1)}")]
        public IHttpActionResult GetUserById(int id)
        {
            var user = _userManagementService.GetUser(id);

            return user == null ? (IHttpActionResult) NotFound() : Ok(user);
        }

        /// <summary>
        /// Регистрирует нового пользователя.
        /// </summary>
        /// <param name="user">Данные экземпляра пользователя.</param>
        /// <returns>Пользователь хранилища.</returns>
        [HttpPost]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Model is not valid", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Created, "New model description", typeof(BL.Common.Models.User.User))]
        public IHttpActionResult AddUser([FromBody] BL.Common.Models.User.User user)
        {
            if (user == null)
            {
                return BadRequest("User can not be empty");
            }

            _userManagementService.AddUser(user);

            return Created($"users/{user.Id}", user);
        }

        /// <summary>
        /// Обновляет пользователя.
        /// </summary>
        /// <param name="user">Пользователь для обновления.</param>
        /// <returns>Сведения об обновлении пользователя.</returns>
        [HttpPut]
        public IHttpActionResult UpdateUser([FromBody]BL.Common.Models.User.User user)
        {
            _userManagementService.UpdateUser(user);

            return new StatusCodeResult(HttpStatusCode.NoContent, Request);
        }

        /// <summary>
        /// Удаление пользователя с конкретным уникальным
        /// идентификатором.
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя.</param>
        /// <returns>Сведения об удалении.</returns>
        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public IHttpActionResult DeleteUserById(int id)
        {
            var usersCount = _userManagementService.GetAllUsers().Count;

            if (id > usersCount)
            {
                return BadRequest("User id invalid");
            }

            _userManagementService.DeleteUser(id);

            return Ok($"User with id = {id} successfully deleted");
        }

        /// <summary>
        /// Удаление пользователя по его модели.
        /// </summary>
        /// <param name="user">Экземпляр пользователя.</param>
        /// <returns>Сведения об удалении.</returns>
        [HttpDelete]
        public IHttpActionResult DeleteUserByModel([FromBody]BL.Common.Models.User.User user)
        {
            var usersLogin = user.Login;

            if (!_userManagementService.UserExists(f => f.Login == usersLogin))
            {
                return BadRequest("User invalid");
            }

            _userManagementService.DeleteUser(user.Id);

            return Ok($"User with id = {user.Id} successfully deleted");
        }
    }
}
