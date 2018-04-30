namespace TopDownTilemapRender.Core.Map
{
    public class Map
    {
        public MapData MapData { get; } = new MapData();

        public void Load(string filename)
        {
            // Load map data to _mapData
            MapLoader.LoadMap(filename, MapData);
        }
    }
}
