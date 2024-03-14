
using HamsterApi.Core.Common;

namespace HamsterApi.Core.Models;

public class Faculty
{
    private Faculty(string id,string title,IReadOnlyCollection<Department> departments, IReadOnlyCollection<Direction> directions)
        =>(Id,Title,Departments,Directions)=(id,title,departments,directions);

    public string Title { get; }

    public string Id { get; }

    public IReadOnlyCollection<Department> Departments { get; }

    public IReadOnlyCollection<Direction> Directions { get; }

    public static Result<Faculty> Create(string id, string title, IReadOnlyCollection<Department> departments, IReadOnlyCollection<Direction> directions)
    {
        var faculty = new Faculty(id, title, departments,directions);
        return faculty;
    }
}
