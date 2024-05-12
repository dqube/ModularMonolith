using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Pagination;

public interface IPagedQuery<T> : IQuery<T>
{
    int Page { get; set; }

    int Results { get; set; }
}