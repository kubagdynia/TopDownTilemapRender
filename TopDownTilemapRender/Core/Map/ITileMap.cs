using System.Collections.Generic;
using SFML.Graphics;
using TiledSharp;

namespace TopDownTilemapRender.Core.Map
{
    public interface ITileMap : Drawable
    {
        void Load(MapData data, IList<TmxLayer> layers);
    }
}