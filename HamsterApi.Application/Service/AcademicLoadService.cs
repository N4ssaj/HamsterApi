using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Domain.ServiceInterface;

namespace HamsterApi.Application.Service;

internal class AcademicLoadService:IAcademicLoadService
{
    private readonly IAcademicLoadRepository _academicLoadRepository;

    public AcademicLoadService(IAcademicLoadRepository academicLoadRepository)
        => _academicLoadRepository = academicLoadRepository;

    public async Task<string> Create(AcademicLoad item)
        =>await _academicLoadRepository.Create(item);

    public async Task<bool> Delete(string id)
        =>await _academicLoadRepository.Delete(id);

    public async Task<AcademicLoad?> Read(string id)
        =>await _academicLoadRepository.Read(id);

    public async Task<List<AcademicLoad>> ReadAll()
        =>await _academicLoadRepository.ReadAll();

    public async Task<List<AcademicLoad>> ReadByIds(IEnumerable<string> ids)
        => await _academicLoadRepository.ReadByIds(ids);

    public async Task<bool> Update(string id, int lectures, int laboratory, int practice, int credits, AcademicEvaluationType academicEvaluationType)
        => await _academicLoadRepository.Update(id, lectures, laboratory, practice, credits, academicEvaluationType);
}
