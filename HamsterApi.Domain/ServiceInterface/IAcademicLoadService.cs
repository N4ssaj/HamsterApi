

using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface.Base;

namespace HamsterApi.Domain.ServiceInterface;

public interface IAcademicLoadService : IBaseService<AcademicLoad>
{
    public Task<bool> Update(string id, int lectures, int laboratory, int practice, int credits, AcademicEvaluationType academicEvaluationType);
}
