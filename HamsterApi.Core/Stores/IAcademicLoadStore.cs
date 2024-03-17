
using HamsterApi.Core.Common.Enum;
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores.Base;

namespace HamsterApi.Core.Stores;

public interface IAcademicLoadStore:IBaseStore<AcademicLoad>
{
    public Task<bool> Update(string id, int lectures, int laboratory, int practice, int credits,AcademicEvaluationType academicEvaluationType);
}
