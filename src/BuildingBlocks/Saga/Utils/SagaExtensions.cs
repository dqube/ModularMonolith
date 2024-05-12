namespace CompanyName.MyProjectName.BuildingBlocks.Saga.Utils
{
#pragma warning disable SA1649 // File name should match first type name
    internal static class Extensions
#pragma warning restore SA1649 // File name should match first type name
    {
        public static Type GetSagaDataType(this ISaga saga)
            => saga
                .GetType()
                .GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISaga<>))
               ?.GetGenericArguments()
                .FirstOrDefault();

        public static object InvokeGeneric(this ISaga saga, string method, params object[] args)
            => saga
                .GetType()
                .GetMethod(method, args.Select(arg => arg.GetType()).ToArray())
                ?.Invoke(saga, args);
    }
}
