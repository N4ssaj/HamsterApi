

using HamsterApi.Core.Common;
using HamsterApi.Core.Common.Enum;

namespace HamsterApi.Core.Models;


public class Group
{
    private Group(string id , string number,LevelOfEducation levelOfEducation,string directionId)
        => (Id, Number, LevelOfEducation,DirectionId) = (id, number,levelOfEducation,directionId);


    public string Number { get; }


    public string Id { get; }

    public string DirectionId { get; }

    public LevelOfEducation LevelOfEducation { get; }

 
    public static Result<Group> Create(string id , string number,LevelOfEducation levelOfEducation,string directionId)
    {
        // валидация
        var group = new Group(id, number,levelOfEducation,directionId);
        return group;
    }
}
