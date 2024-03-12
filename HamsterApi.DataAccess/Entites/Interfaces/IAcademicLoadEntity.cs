
using BrightstarDB.EntityFramework;
using HamsterApi.Core.Common.Enum;

namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface IAcademicLoadEntity
{
    public string Id { get; }

    public int Lectures { get; set; }

    public int Laboratory { get; set; }

    public int Practice { get; set; }

    public int ControlWork { get; set; }

    public int IndependentWork { get; set; }

    public int Credits { get; set; }

    public int Total { get; set; }

    public AcademicEvaluationType AcademicEvaluationType { get; set; }
}
