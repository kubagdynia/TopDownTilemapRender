namespace TopDownTilemapRender.Core.Map
{
    public class Map
    {
        private ITileMap _backgroundTileMap;
        private ITileMap _foregroundTileMap;
        
        public MapData MapData { get; } = new MapData();

        public ITileMap GetBackgroundTileMap() => _backgroundTileMap;

        public ITileMap GetForegroundTileMap() => _foregroundTileMap;
        
        public void Load(string filename)
        {
            // Load map data to _mapData
            MapLoader.LoadMap(filename, MapData);
            
            LoadTileMaps();
        }
        
        private void LoadTileMaps()
        {
            _backgroundTileMap = new TileMap();
            _backgroundTileMap.Load(MapData, MapData.BackgroundTileLayers);

            _foregroundTileMap = new TileMap();
            _foregroundTileMap.Load(MapData, MapData.ForegroundTileLayers);
        }
    }
}
