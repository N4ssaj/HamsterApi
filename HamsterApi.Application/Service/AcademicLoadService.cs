
using HamsterApi.Core.Common.Enum;
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface;
using HamsterApi.Core.Stores;

namespace HamsterApi.Application.Service;

public class AcademicLoadService:IAcademicLoadService
{
    private readonly IAcademicLoadStore _academicLoadStore;

    public AcademicLoadService(IAcademicLoadStore academicLoadStore)
        => _academicLoadStore = academicLoadStore;

    public async Task<string> Create(AcademicLoad item)
        =>await _academicLoadStore.Create(item);

    public async Task<bool> Delete(string id)
        =>await _academicLoadStore.Delete(id);

    public async Task<AcademicLoad?> Read(string id)
        =>await _academicLoadStore.Read(id);

    public async Task<List<AcademicLoad>?> ReadAll()
        =>await _academicLoadStore.ReadAll();

    public async Task<bool> Update(string id, int lectures, int laboratory, int practice, int credits, AcademicEvaluationType academicEvaluationType)
        => await _academicLoadStore.Update(id, lectures, laboratory, practice, credits, academicEvaluationType);
}
