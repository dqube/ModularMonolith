namespace CompanyName.MyProjectName.BuildingBlocks.Saga;

public interface ISagaContextMetadata
{
    string Key { get; }

    object Value { get; }
}
