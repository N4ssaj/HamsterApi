
using HamsterApi.Domain.Models;
using HamsterApi.Persistence.Entites.Interfaces;

namespace HamsterApi.Persistence.MappingExtensions;

internal static class DepartmentMapping
{
    public static IDepartmentEntity ToEntity(this Department department)
    {
        IDepartmentEntity departmentEntity = new DepartmentEntity
        {
            Id = department.Id,
            ChairsIds = department.ChairsIds.ToList(),
            DirectionsIds = department.DirectionsIds.ToList(),
            Title = department.Title
        };

        return departmentEntity;
    }

    public static Department ToModel(this IDepartmentEntity departmentEntity)
    {
        Department department = Department.Create(departmentEntity.Id,
            departmentEntity.Title,
            departmentEntity.ChairsIds.ToList(),
            departmentEntity.DirectionsIds.ToList()).Value;

        return department;
    }
}
