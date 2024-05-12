namespace CompanyName.MyProjectName.BuildingBlocks.API.Exceptions;

public interface IExceptionToResponseMapper
{
    ExceptionResponse Map(Exception exception);
}