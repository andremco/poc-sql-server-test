using Courses.Application.Services.Interfaces;
using Courses.Application.ViewModels;
using Courses.Domain.Interfaces.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.API.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("api/v1/course")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IDomainNotification _domainNotification;

        public CourseController(ICourseService courseService, IDomainNotification domainNotification)
        {
            _courseService = courseService;
            _domainNotification = domainNotification;
        }

        /// <summary>
        /// Cria um curso
        /// </summary>        
        /// <returns>Cria um curso</returns>
        /// <param name="courseViewModel">Requisição contendo curso</param>
        /// <response code="200">Return curso criado</response>
        /// <response code="400">Bad request</response>       
        /// <response code="404">Not found</response>       
        /// <response code="500">Internal server error</response>
        [HttpPost]
        public async Task<ActionResult<CourseViewModel>> RegisterCourse([FromBody] CourseViewModel courseViewModel)
        {
            var course = await _courseService.AddAsync(courseViewModel);

            if (_domainNotification.HasNotifications)
            {
                return new EmptyResult();
            }

            return Ok(course);
        }

        /// <summary>
        /// Edita um curso
        /// </summary>        
        /// <returns>Edita um curso</returns>
        /// <param name="courseViewModel">Requisição contendo curso</param>
        /// <response code="200">Return curso editado</response>
        /// <response code="400">Bad request</response>       
        /// <response code="404">Not found</response>       
        /// <response code="500">Internal server error</response>
        [HttpPut]
        public async Task<ActionResult<CourseViewModel>> EditCourse([FromBody] CourseViewModel courseViewModel)
        {
            var course = await _courseService.UpdateAsync(courseViewModel);

            if (_domainNotification.HasNotifications)
            {
                return new EmptyResult();
            }

            return Ok(course);
        }

        /// <summary>
        /// Remove curso pelo id
        /// </summary>        
        /// <returns>Remove curso</returns>
        /// <param name="id">Id do curso</param>
        /// <response code="204">No content</response>       
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<CourseViewModel>> DeleteCourse(int id)
        {
            await _courseService.DeleteAsync(id);

            if (_domainNotification.HasNotifications)
            {
                return new EmptyResult();
            }

            return NoContent();
        }

        /// <summary>
        /// Busca curso pelo id
        /// </summary>        
        /// <returns>Edita um curso</returns>
        /// <param name="id">Id do curso</param>
        /// <response code="200">Return curso</response>   
        /// <response code="204">No content</response>       
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseViewModel>> GetCourse(int id)
        {
            var course = await _courseService.GetByIdAsync(id);

            if (course == null)
            {
                return NoContent();
            }

            return Ok(course);
        }

        /// <summary>
        /// Busca todos os cursos cadastrados
        /// </summary>        
        /// <returns>Cursos cadastrados</returns>
        /// <response code="200">Return cursos cadastrados</response>   
        /// <response code="204">No content</response>       
        /// <response code="500">Internal server error</response>
        [HttpGet]
        public async Task<ActionResult<CourseViewModel>> GetAll()
        {
            var courses = await _courseService.GetAllAsync();

            if (courses == null || !courses.Any())
            {
                return NoContent();
            }

            return Ok(courses);
        }
    }

}
