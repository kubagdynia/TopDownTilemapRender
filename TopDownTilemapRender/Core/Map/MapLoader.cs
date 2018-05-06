using System;
using System.Linq;
using SFML.Graphics;
using SFML.System;
using TiledSharp;
using TopDownTilemapRender.Core.Extensions;
using TopDownTilemapRender.Core.Managers;
using TopDownTilemapRender.Core.Map.Exceptions;

namespace TopDownTilemapRender.Core.Map
{
    public static class MapLoader
    {
        private const string BackgroundGroupLayerName = "Background";

        private const string ForegroundGroupLayerName = "Foreground";

        public static void LoadMap(string mapResourceName, MapData data)
        {
            var map = AssetManager.Instance.Map.Get(mapResourceName);
            
            // Load resources
            LoadMapProperties(map, data);
            LoadTileLayers(map, data);
            LoadMapTileset(map, data);

            UpdateData(data);
            
#if DEBUG
            "Map loaded".Log();
#endif
        }

        private static void UpdateData(MapData data)
        {
            data.MapRec = new FloatRect(0, 0, data.TileSize.X * data.MapSize.X, data.TileSize.Y * data.MapSize.Y);
        }

        private static void LoadMapProperties(TmxMap map, MapData data)
        {
            data.MapSize = new Vector2i(map.Width, map.Height);
            data.TileSize = new Vector2i(map.TileWidth, map.TileHeight);
            
#if DEBUG
            "Map properties loaded".Log();
#endif
        }
        
        private static void LoadTileLayers(TmxMap map, MapData data)
        {
            if (!BackgroundTileLayersExists(map))
            {
                throw new MapException("Map file could not be loaded because no background map layers found.", "Add Background layer to the map file.");
            }

            foreach (var group in map.Groups)
            {
                LoadBackgroundLayer(data, group);
                LoadForegroundLayer(data, group);
            }
            
#if DEBUG
            "Tile layers loaded".Log();
#endif
        }
        
        private static void LoadMapTileset(TmxMap map, MapData data)
        {
            if (!TilesetsExists(map))
            {
                throw new MapException("Map file could not be loaded because no tileset found.", "Add tileset map file.");
            }

            foreach (var item in map.Tilesets)
            {
                var mapTileset = new MapTileset
                {
                    Firstgid = item.FirstGid,
                    Name = item.Name,
                    TileWidth = item.TileWidth,
                    TileHeight = item.TileHeight,
                    TileCount = item.TileCount ?? 0,
                    Columns = item.Columns ?? 0,
                    ImagePath = item.Image.Source,
                    ImageWidth = item.Image.Width ?? 0,
                    ImageHeight = item.Image.Height ?? 0
                };
                data.Tilesets.Add(mapTileset);
            }
            
#if DEBUG
            "Map tileset loaded".Log();
#endif
        }
        
        private static void LoadBackgroundLayer(MapData data, TmxGroup group)
        {
            if (!group.Name.Equals(BackgroundGroupLayerName, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            foreach (var layer in group.Layers)
            {
                data.BackgroundTileLayers.Add(layer);
            }
            
#if DEBUG
            "Background layer loaded".Log();
#endif
        }
        
        private static void LoadForegroundLayer(MapData data, TmxGroup group)
        {
            if (!group.Name.Equals(ForegroundGroupLayerName, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            foreach (var layer in group.Layers)
            {
                data.ForegroundTileLayers.Add(layer);
            }
            
#if DEBUG
            "Foreground layer loaded".Log();
#endif
        }
        
        private static bool BackgroundTileLayersExists(TmxMap map)
        {
            return map.Groups != null && map.Groups.Any(x => x.Name.Equals(BackgroundGroupLayerName, StringComparison.InvariantCultureIgnoreCase));
        }
        
        private static bool TilesetsExists(TmxMap map)
        {
            return map.Tilesets != null && map.Tilesets.Any();
        }
    }
}
