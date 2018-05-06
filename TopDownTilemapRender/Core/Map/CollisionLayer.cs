using System.Collections.ObjectModel;

namespace TopDownTilemapRender.Core.Map
{
    public class CollisionLayer : TiledObjectLayer
    {
        private Collection<DataStructures.IntRect> _collisionRects;

        public Collection<DataStructures.IntRect> CollisionRects
        {
            get
            {
                if (_collisionRects == null)
                {
                    _collisionRects = new Collection<DataStructures.IntRect>();
                }
                return _collisionRects;
            }
            set => _collisionRects = value;
        }
    }
}