using System.Collections.Generic;

namespace TwainProxy
{
    internal sealed class TwainIdentityComparer : IComparer<TwainStructs.Identity>
    {
        public int Compare(TwainStructs.Identity x, TwainStructs.Identity y)
        {
            return StringLogicalComparer.Compare(x.ProductName, y.ProductName);
        }
    }
}
