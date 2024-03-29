﻿using BrightstarDB.EntityFramework;


namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface IAuditoriumEntity
{
    public string Number { get; set; }

    public string Id { get; }
}
