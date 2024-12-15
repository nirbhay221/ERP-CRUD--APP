using Products.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Products.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Products.Core
{
    public class ProjectsServices : IProjectsServices
    {
        private readonly AppDbContext _context;
        private readonly User _user;

        public ProjectsServices(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user = _context.Users.FirstOrDefault(u => u.Username == httpContextAccessor.HttpContext.User.Identity.Name);

            if (_user == null)
                throw new Exception("Authenticated user not found.");
        }

        public Project CreateProject(Project project)
        {
            project.UserProjects = new List<UserProject>
            {
                new UserProject { UserId = _user.Id, Project = project, Role = "Owner" }
            };

            _context.Projects.Add(project);
            _context.SaveChanges();

            return project;
        }

        public void DeleteProject(Project project)
        {
            var dbProject = _context.Projects
                .Include(p => p.UserProjects)
                .FirstOrDefault(p => p.Id == project.Id && p.UserProjects.Any(up => up.UserId == _user.Id && up.Role == "Owner"));

            if (dbProject == null)
                throw new Exception("Project not found or you do not have permission to delete it.");

            _context.Projects.Remove(dbProject);
            _context.SaveChanges();
        }

        public Project EditProject(Project project)
        {
            var dbProject = _context.Projects
                .Include(p => p.UserProjects)
                .FirstOrDefault(p => p.Id == project.Id && p.UserProjects.Any(up => up.UserId == _user.Id && up.Role == "Owner"));

            if (dbProject == null)
                throw new Exception("Project not found or you do not have permission to edit it.");

            dbProject.Name = project.Name;
            dbProject.Description = project.Description;

            _context.SaveChanges();

            return dbProject;
        }

        public Project GetProject(int id)
        {
            return _context.Projects
                .Include(p => p.ProductProjects)
                .Include(p => p.UserProjects)
                .FirstOrDefault(p => p.Id == id && p.UserProjects.Any(up => up.UserId == _user.Id));
        }

        public List<Project> GetProjects()
        {
            return _context.Projects
                .Include(p => p.ProductProjects)
                .Include(p => p.UserProjects)
                .Where(p => p.UserProjects.Any(up => up.UserId == _user.Id))
                .ToList();
        }
    }
}
