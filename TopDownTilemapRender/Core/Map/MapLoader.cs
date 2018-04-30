using SFML.Graphics;
using SFML.System;
using TiledSharp;

namespace TopDownTilemapRender.Core.Map
{
    public static class MapLoader
    {
        public static void LoadMap(string filepath, MapData data)
        {
            TmxMap map = new TmxMap(filepath);

            // Load resources
            LoadMapProperties(map, data);

            UpdateData(data);
        }

        private static void UpdateData(MapData data)
        {
            data.MapRec = new FloatRect(0, 0, data.TileSize.X * data.MapSize.X, data.TileSize.Y * data.MapSize.Y);
        }

        private static bool LoadMapProperties(TmxMap map, MapData data)
        {
            data.MapSize = new Vector2i(map.Width, map.Height);
            data.TileSize = new Vector2i(map.TileWidth, map.TileHeight);

            return true;
        }
    }
}
