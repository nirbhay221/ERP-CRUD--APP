using Products.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Core
{
    public interface IProjectsServices
    {
        List<Project> GetProjects();
        Project GetProject(int id);
        Project CreateProject(DB.Project project);
        void DeleteProject(Project project);
        Project EditProject(Project project);
    }
}
