﻿using HamsterApi.Domain.Common;
using HamsterApi.Domain.Common.Enum;


namespace HamsterApi.Domain.Models;


public class ScheduledClass
{
    private ScheduledClass(string id, int classNumber, string subjectId, string teacherId, string auditoriumId, ClassType classType)
        => (Id, ClassNumber, SubjectId, TeacherId, AuditoriumId, ClassType) = (id, classNumber, subjectId, teacherId, auditoriumId, classType);

    public string Id { get; }

    public ClassType ClassType { get; }

    public int ClassNumber { get; }

    public string SubjectId { get; }

    public string TeacherId { get; }

    public string AuditoriumId { get; }

    public static Result<ScheduledClass> Create(string id, int classNumber, string subject, string teacher, string auditorium, ClassType classType)
    {

        var scheduledClass = new ScheduledClass(id, classNumber, subject, teacher, auditorium, classType);
        return scheduledClass;
    }
}

