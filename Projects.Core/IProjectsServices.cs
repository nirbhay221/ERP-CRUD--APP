using Projects.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Core
{
    public interface IProjectsServices
    {
        List<Project> GetProjects();
        Project GetProject(int id);
        Project CreateProject(Project project);
        void DeleteProject(Project project);
        Project EditProject(Project project);
    }
}
