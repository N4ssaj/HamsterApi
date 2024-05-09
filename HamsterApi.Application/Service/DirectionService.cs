using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface;
using HamsterApi.Domain.RepositoriesInterfaces;

namespace HamsterApi.Application.Service;

internal class DirectionService : IDirectionService
{
    private readonly IDirectionRepository _directionRepository;

    public DirectionService(IDirectionRepository directionRepository)
        =>_directionRepository = directionRepository;

    public async Task<bool> AddDepartment(string id, string departmentId)
        =>await _directionRepository.AddDepartment(id, departmentId);

    public async Task<bool> AddGroupById(string id, string groupId)
        =>await _directionRepository.AddGroupById(id, groupId);

    public async Task<bool> AddRangeGroupById(string id, IEnumerable<string> groupId)
        =>await _directionRepository.AddRangeGroupById(id,groupId);

    public async Task<string> Create(Direction item)
        =>await _directionRepository.Create(item);

    public async Task<bool> Delete(string id)
        =>await _directionRepository.Delete(id);

    public async Task<Direction?> Read(string id)
        =>await _directionRepository.Read(id);

    public async Task<List<Direction>> ReadAll()
        =>await _directionRepository.ReadAll();

    public async Task<List<Direction>> ReadByIds(IEnumerable<string> ids)
        =>await _directionRepository.ReadByIds(ids);

    public async Task<bool> RemoveDepartment(string id)
        =>await _directionRepository.RemoveDepartment(id);

    public async Task<bool> RemoveGroupById(string id, string groupId)
        =>await _directionRepository.RemoveGroupById(id, groupId);

    public async Task<bool> RemoveRangeGroupById(string id, IEnumerable<string> groupId)
        =>await _directionRepository.RemoveRangeGroupById(id, groupId);  

    public async Task<bool> Update(string id, string title, IReadOnlyCollection<string> groupsIds, FormOfEducation formOfEducation, LevelOfEducation levelOfEducation, string departmentId)
        =>await _directionRepository.Update(id,title, groupsIds, formOfEducation, levelOfEducation, departmentId);
}
