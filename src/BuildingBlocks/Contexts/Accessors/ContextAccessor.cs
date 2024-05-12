namespace CompanyName.MyProjectName.BuildingBlocks.Contexts.Accessors;

#nullable enable
public sealed class ContextAccessor : IContextAccessor
{
    private static readonly AsyncLocal<ContextHolder> Holder = new();

    public IContext? Context
    {
        get => Holder.Value?.Context;
        set
        {
            var holder = Holder.Value;
            if (holder != null)
            {
                holder.Context = null;
            }

            if (value is not null)
            {
                Holder.Value = new ContextHolder { Context = value };
            }
        }
    }

    private class ContextHolder
    {
        private IContext? context;

        public IContext? Context
        {
            get => context;
            set => context = value;
        }
    }
}
