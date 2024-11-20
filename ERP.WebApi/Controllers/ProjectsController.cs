using Microsoft.AspNetCore.Mvc;
using Projects.Core;
using Projects.DB;

namespace ERP.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private IProjectsServices _projectsServices;
        public ProjectsController(IProjectsServices projectsServices)
        {
            _projectsServices = projectsServices;
        }

        [HttpGet]
        public IActionResult GetProjects()
        {
            return Ok(_projectsServices.GetProjects());

        }

        [HttpGet("{id}", Name = "GetProject")]
        public IActionResult GetProject(int id)
        {
            return Ok(_projectsServices.GetProject(id));
        }

        [HttpPost]
        public IActionResult CreateProject(Project project)
        {
            var newProject = _projectsServices.CreateProject(project);
            return CreatedAtRoute("GetProject", new { newProject.Id }, newProject);
        }

        [HttpDelete]
        public IActionResult DeleteProject(Project project)
        {
            _projectsServices.DeleteProject(project);
            return Ok();
        }

        [HttpPut]
        public IActionResult EditProject(Project project)
        {
            return Ok(_projectsServices.EditProject(project));
        }
    }
}
