using System.Net;

namespace CompanyName.MyProjectName.BuildingBlocks.API.Exceptions;

public sealed record ExceptionResponse(object Response, HttpStatusCode StatusCode);