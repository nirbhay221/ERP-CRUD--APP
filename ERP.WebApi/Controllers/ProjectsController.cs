using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Core;
using Products.Core.DTO;
using Products.DB;

namespace ERP.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsServices _projectsServices;

        public ProjectsController(IProjectsServices projectsServices)
        {
            _projectsServices = projectsServices;
        }

        [HttpGet]
        public IActionResult GetProjects()
        {
            var projects = _projectsServices.GetProjects();
            return Ok(projects);
        }

        [HttpGet("{id}", Name = "GetProject")]
        public IActionResult GetProject(int id)
        {
            var project = _projectsServices.GetProject(id);
            if (project == null)
            {
                return NotFound("Project not found or you do not have permission to view it.");
            }

            return Ok(project);
        }

        [HttpPost]
        public IActionResult CreateProject([FromBody] Project projectDto)
        {
            if (projectDto == null)
            {
                return BadRequest("Invalid project data.");
            }

            var newProject = _projectsServices.CreateProject(projectDto);
            return CreatedAtRoute("GetProject", new { id = newProject.Id }, newProject);
        }

        [HttpDelete]
        public IActionResult DeleteProject([FromBody] Project projectDto)
        {
            if (projectDto == null)
            {
                return BadRequest("Invalid project data.");
            }

            try
            {
                _projectsServices.DeleteProject(projectDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult EditProject([FromBody] Project projectDto)
        {
            if (projectDto == null)
            {
                return BadRequest("Invalid project data.");
            }

            try
            {
                var updatedProject = _projectsServices.EditProject(projectDto);
                return Ok(updatedProject);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
