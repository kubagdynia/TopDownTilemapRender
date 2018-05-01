using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using TiledSharp;

namespace TopDownTilemapRender.Core.Map
{
    public class MapData
    {
        public int TileWorldDimension { get; } = 2;

        private float _mapZoomFactor = 0.6f;
        public float MapZoomFactor
        {
            get => _mapZoomFactor;
            set => _mapZoomFactor = value > 0.01f ? value : 0.01f;
        }

        public Vector2i MapSize { get; set; }
        public Vector2i TileSize { get; set; }

        public FloatRect MapRec { get; set; }
        
        private List<MapTileset> _tilesets;
        public List<MapTileset> Tilesets => _tilesets ?? (_tilesets = new List<MapTileset>());
        
        private List<TmxLayer> _backgroundTileLayers;
        public List<TmxLayer> BackgroundTileLayers => _backgroundTileLayers ?? (_backgroundTileLayers = new List<TmxLayer>());

        private List<TmxLayer> _foregroundTileLayers;
        public List<TmxLayer> ForegroundTileLayers => _foregroundTileLayers ?? (_foregroundTileLayers = new List<TmxLayer>());
    }
}
