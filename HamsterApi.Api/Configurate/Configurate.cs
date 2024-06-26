﻿using AutoMapper;
using BrightstarDB.Client;
using HamsterApi.Application.Service;
using HamsterApi.Core.ServiceInterface;
using HamsterApi.Core.Stores;
using HamsterApi.DataAccess;
using HamsterApi.DataAccess.MappingProfile;
using HamsterApi.DataAccess.Repositories;


namespace HamsterApi.Api.Configurate;

public static class Configurate
{

    public static void RegisterMapper(this IServiceCollection _services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile<GeneralProfile>();
        });
        var mapper = mapperConfig.CreateMapper();
        _services.AddSingleton(mapper);
    }
    private static void RegisterStores(this IServiceCollection _services)
    {
        _services.AddSingleton<IAuditoriumStore, AuditoriumRepository>();
        _services.AddSingleton<IGroupStore, GroupRepository>();
        _services.AddSingleton<ISubjectStore, SubjectRepository>();
        _services.AddSingleton<ITeacherStore, TeacherRepository>();
        _services.AddSingleton<IDirectionStore,DirectionRepository>();
        _services.AddSingleton<IChairStore,ChairRepository>();
        _services.AddSingleton<IDepartmentStore,DepartmentRepository>();
        _services.AddSingleton<IScheduleStore, ScheduleRepository>();
    }

    private static void RegisterService(this IServiceCollection _services)
    {
        _services.AddSingleton<IAuditoriumService, AuditoruimService>();
        _services.AddSingleton<IGroupService, GroupService>();
        _services.AddSingleton<ISubjectService, SubjectService>();
        _services.AddSingleton<ITeacherService,TeacherService>();
        _services.AddSingleton<IDirectionService, DirectionService>();
        _services.AddSingleton<IChairService, ChairService>();
        _services.AddSingleton<IDepartmentService, DepartmentService>();
        _services.AddSingleton<IScheduleService, ScheduleService>();
    }
    private static void RegisterDb(this IServiceCollection _services,string connectionString,string dir)
    {
        var hamsterApiDbContext = new HamsterApiDbContext(connectionString);
        _services.AddSingleton(hamsterApiDbContext);
        var client=BrightstarService.GetClient(dir);
        _services.AddSingleton(client);
    }
    public static void Register(this IServiceCollection _services, string connectionString,string dir)
    {
        _services.RegisterDb(connectionString,dir);
        _services.RegisterMapper();
        _services.RegisterStores();
        _services.RegisterService();
    }
}