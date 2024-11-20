using Projects.DB;

namespace Projects.Core
{
    public class ProjectsServices : IProjectsServices
    {
        private ProjectDbContext _context;
        public ProjectsServices(ProjectDbContext context)
        {
            _context = context;
        }

        public Project CreateProject(Project project)
        {
            _context.Add(project);
            _context.SaveChanges();

            return project;
        }

        public void DeleteProject(Project project)
        {
            _context.Projects.Remove(project);
            _context.SaveChanges();
        }

        public Project EditProject(Project project)
        {
            var dbExpense = _context.Projects.First(e => e.Id == project.Id);
            dbExpense.Description = project.Description;
            dbExpense.Quantity = project.Quantity;
            _context.SaveChanges();
            return dbExpense;
        }

        public Project GetProject(int id)
        {
            return _context.Projects.First(e => e.Id == id);
        }
        public List<Project> GetProjects()
        {
            return _context.Projects.ToList();
        }
    }
}
