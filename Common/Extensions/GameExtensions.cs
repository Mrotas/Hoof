using System.Collections.Generic;
using Common.Enums;

namespace Common.Extensions
{
    public static class GameExtensions
    {
        public static bool IsIn(this int gameKind, IList<GameKind> gameKinds)
        {
            foreach (GameKind kind in gameKinds)
            {
                if (kind == (GameKind) gameKind)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
