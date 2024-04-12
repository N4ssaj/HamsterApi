using HamsterApi.Core.Common;
using System.Text.RegularExpressions;

namespace HamsterApi.Core.Models;


public class Auditorium
{
    private Auditorium(string id, string number)
        => (Id, Number) = (id, number);

    public string Number { get; }


    public string Id { get; }

    public static Result<Auditorium> Create(string id, string number)
    {
       
        var auditorium = new Auditorium(id, number);
        return auditorium;
    }
}
