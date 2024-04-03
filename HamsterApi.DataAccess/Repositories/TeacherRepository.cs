
using AutoMapper;
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores;
using HamsterApi.DataAccess.Entites.Interfaces;
using System;

namespace HamsterApi.DataAccess.Repositories;

public class TeacherRepository : ITeacherStore
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;
    private readonly IMapper _mapper;

    public TeacherRepository(HamsterApiDbContext hamsterApiDbContext, IMapper mapper)
            => (_hamsterApiDbContext, _mapper) = (hamsterApiDbContext, mapper);

    public async Task<bool> AddChair(string id, string chairId)
    {
        var teacher = await Read(id);
        if (teacher is null) return false;
        return await Update(id,teacher.Name,teacher.Surname,teacher.Patronymic,teacher.SubjectsIds,chairId,teacher.TeacherLoadId);
    }

    public async Task<bool> AddRangeSubjectById(string id, IEnumerable<string> subjectId)
    {
        var teacher = await Read(id);
        if (teacher is null) return false;
        foreach (var subject in subjectId)
            if (!teacher.SubjectsIds.Contains(subject))
                teacher.AddSubject(subject);

        return await Update(teacher.Id, teacher.Name, teacher.Surname, teacher.Patronymic, teacher.SubjectsIds, teacher.ChairId, teacher.TeacherLoadId);
    }

    public async Task<bool> AddSubjectById(string id, string subjectId)
    {
        var teacher = await Read(id);
        if (teacher is null) return false;
        teacher.AddSubject(subjectId);
        return await Update(teacher.Id, teacher.Name, teacher.Surname, teacher.Patronymic, teacher.SubjectsIds, teacher.ChairId, teacher.TeacherLoadId);
    }

    public async Task<string> Create(Teacher item)
    {
        var teacherEntity = _mapper.Map<TeacherEntity>(item);
        await Task.Run(() =>
        {
            _hamsterApiDbContext.TeacherEntities.Add(teacherEntity);
            _hamsterApiDbContext.SaveChanges();
        });
        return teacherEntity.Id;
    }

    public async Task<bool> Delete(string id)
    {
        ITeacherEntity teacherEntity = null;
        bool state = false;
        await Task.Run(() =>
        {
            teacherEntity = _hamsterApiDbContext.TeacherEntities.FirstOrDefault(a => a.Id == id)!;
            if (teacherEntity is not null)
            {
                _hamsterApiDbContext.DeleteObject(teacherEntity);
                _hamsterApiDbContext.SaveChanges();
                state = true;
            }
        });
        return state;
    }

    public async Task<Teacher?> Read(string id)
    {
        ITeacherEntity teacherEntity=null!;
        await Task.Run(() =>
        {
            teacherEntity = _hamsterApiDbContext.TeacherEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (teacherEntity is null) return null;

        var teacher = _mapper.Map<Teacher>(teacherEntity);

        return teacher;
    }

    public async Task<List<Teacher>> ReadAll()
    {
        var teacherEntityList = new List<ITeacherEntity>();
        await Task.Run(() =>
        {
            teacherEntityList = _hamsterApiDbContext.TeacherEntities.ToList();
        }
        );
        if (teacherEntityList is null) return [];
        var subjectList = teacherEntityList.Select(a => _mapper.Map<Teacher>(a)).ToList();

        return subjectList;
    }

    public async Task<List<Teacher>> ReadByIds(IEnumerable<string> ids)
    {
        var teacherEntityList = new List<ITeacherEntity>();
        await Task.Run(() =>
        {
            teacherEntityList = _hamsterApiDbContext.TeacherEntities
            .Where(g => ids.Contains(g.Id))
            .ToList();
        }
        );
        if (teacherEntityList is null) return [];
        var subjectList = teacherEntityList.Select(a => _mapper.Map<Teacher>(a)).ToList();

        return subjectList;
    }

    public async Task<bool> RemoveChair(string id)
    {
        var teacher = await Read(id);
        if (teacher is null) return false;

        return await Update(id, teacher.Name, teacher.Surname, teacher.Patronymic, teacher.SubjectsIds, string.Empty, teacher.TeacherLoadId);
    }

    public async Task<bool> RemoveRangeSubjectById(string id, IEnumerable<string> subjectId)
    {
        var teacher = await Read(id);
        if (teacher is null) return false;
        foreach (var subject in subjectId)
            if (teacher.SubjectsIds.Contains(subject))
                teacher.RemoveSubject(subject);

        return await Update(teacher.Id, teacher.Name, teacher.Surname, teacher.Patronymic, teacher.SubjectsIds, teacher.ChairId, teacher.TeacherLoadId);
    }

    public async Task<bool> RemoveSubjectById(string id, string subjectId)
    {
        var teacher = await Read(id);
        if (teacher is null) return false;
        teacher.RemoveSubject(subjectId);
        return await Update(teacher.Id, teacher.Name, teacher.Surname, teacher.Patronymic, teacher.SubjectsIds, teacher.ChairId, teacher.TeacherLoadId);
    }

    public async Task<bool> Update(string id, string name, string surname, string patronymic, IReadOnlyCollection<string> subjectsIds, string chairId, string teacherLoadId)
    {
        ITeacherEntity teacherEntity = null;
        await Task.Run(() =>
        {
            teacherEntity = _hamsterApiDbContext.TeacherEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (teacherEntity is null) return false;
        teacherEntity.Name = name;
        teacherEntity.Surname= surname;
        teacherEntity.Patronymic= patronymic;
        teacherEntity.SubjectsIds = subjectsIds.ToList();
        teacherEntity.ChairId = chairId;
        teacherEntity.TeacherLoadId = teacherLoadId;
        
        await Task.Run(() =>
        {
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }
}
