﻿using HamsterApi.Core.Common.Enum;

namespace HamsterApi.Api.Contracts.Request;

public record GroupRequest(string Number, LevelOfEducation LevelOfEducation, string DirectionId);
