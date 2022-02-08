namespace DiplomaChat.Common.Infrastructure.Selectors
{
    public interface ICoalesceSelector
    {
        TValue? Coalesce<TValue>(params TValue?[] values)
            where TValue : struct;
    }
}