using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces.Base;

namespace HamsterApi.Domain.RepositoriesInterfaces;

public interface IAcademicLoadRepository : IBaseRepository<AcademicLoad>
{
    public Task<bool> Update(string id, int lectures, int laboratory, int practice, int credits, AcademicEvaluationType academicEvaluationType);
}
