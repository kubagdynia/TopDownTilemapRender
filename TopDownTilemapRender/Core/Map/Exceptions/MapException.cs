using TopDownTilemapRender.Core.Exceptions;

namespace TopDownTilemapRender.Core.Map.Exceptions
{
    public class MapException : BaseCoreException
    {
        public MapException(string message, string hint)
            : base(message, hint)
        {

        }
    }
}
