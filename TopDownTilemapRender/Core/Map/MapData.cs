using SFML.Graphics;
using SFML.System;

namespace TopDownTilemapRender.Core.Map
{
    public class MapData
    {
        public int TileWorldDimension { get; } = 2;

        public float MapZoomFactor { get; set; } = 0.7f;

        public Vector2i MapSize { get; set; }
        public Vector2i TileSize { get; set; }

        public FloatRect MapRec { get; set; }
    }
}
