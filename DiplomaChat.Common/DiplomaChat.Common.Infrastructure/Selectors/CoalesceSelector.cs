using System.Linq;

namespace DiplomaChat.Common.Infrastructure.Selectors;

public class CoalesceSelector : ICoalesceSelector
{
    public TValue? Coalesce<TValue>(params TValue?[] values)
        where TValue : struct
    {
        return values.FirstOrDefault(v => v.HasValue && !v.Value.Equals(default(TValue)));
    }
}