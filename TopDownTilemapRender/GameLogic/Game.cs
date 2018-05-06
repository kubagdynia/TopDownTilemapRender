using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using TopDownTilemapRender.Core;
using TopDownTilemapRender.Core.Map;
using TopDownTilemapRender.Core.Extensions;
using TopDownTilemapRender.Core.Managers;

namespace TopDownTilemapRender.GameLogic
{
    public class Game : BaseGame
    {
        private readonly Map _map;
        
        private Vector2f _cameraPosition;

        private Player _player;

        private InfoHud _infoHud;

        private bool _showCollisions = true;

        public Game()
            : base(new Vector2u(1440, 810), "My World", Color.Black, 60, false, true)
        {
            _map = new Map();

            Console.WriteLine(Window.Size);
            
            _cameraPosition = GetCameraStartPosition();
        }

        protected override void LoadContent()
        {            
            AssetManager.Instance.Map.LoadOnly("jungleMap", AssetManager.Instance.GetMapPath("tf_jungle_map.tmx"));
            _map.Load("jungleMap");
            
            AssetManager.Instance.Font.LoadOnly("arial", AssetManager.Instance.GetFontPath("arial.ttf"));
        }

        protected override void Initialize()
        {
            _player = new Player(_map.MapData.TileSize, _map.MapData.TileWorldDimension);
            
            _infoHud = new InfoHud();
        }

        protected override void Update(float deltaTime)
        {
            MapZoom(deltaTime);
            
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                MoveCamera(deltaTime, 0, 1);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                MoveCamera(deltaTime, 0, -1);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                MoveCamera(deltaTime, -1, 0);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                MoveCamera(deltaTime, 1, 0);
            }
        }

        private void MapZoom(float deltaTime)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.PageUp) || Keyboard.IsKeyPressed(Keyboard.Key.PageDown))
            {
                var changeValue = 0.01f;

                if (Keyboard.IsKeyPressed(Keyboard.Key.PageUp))
                {
                    changeValue = -changeValue;
                }
                
                _map.MapData.MapZoomFactor += changeValue * deltaTime * 50;
#if DEBUG
                $"Map zoom: {_map.MapData.MapZoomFactor}".Log();
#endif
            } 
        }

        protected override void Render(float deltaTime)
        {
            DrawTileMap();

            DrawCollisions();
            
            DrawInfoHud();
        }
        
        protected override void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Q)
            {
                Window.Close();
            }

            if (e.Code == Keyboard.Key.C)
            {
                _showCollisions = !_showCollisions;
            }
            
        }
        
        protected override void KeyReleased(object sender, KeyEventArgs e)
        {

        }

        protected override void JoystickButtonPressed(object sender, JoystickButtonEventArgs arg)
        {

        }

        protected override void JoystickButtonReleased(object sender, JoystickButtonEventArgs arg)
        {

        }

        protected override void JoystickConnected(object sender, JoystickConnectEventArgs arg)
        {

        }

        protected override void JoystickDisconnected(object sender, JoystickConnectEventArgs arg)
        {

        }

        protected override void JoystickMoved(object sender, JoystickMoveEventArgs arg)
        {

        }

        protected override void Resize(uint width, uint height)
        {

        }
        
        protected override void Quit()
        {
            Console.WriteLine("Quit Game :(");
        }
        
        private void MoveCamera(float deltaTime, float x, float y)
        {
            const int speed = 200;

            _cameraPosition += new Vector2f(x * speed * deltaTime, y * speed * deltaTime);
            
            if (_cameraPosition.X < 0)
            {
                _cameraPosition.X = 0;
            }
            
            if (_cameraPosition.Y < 0)
            {
                _cameraPosition.Y = 0;
            }
            
            if (_cameraPosition.X > (_map.MapData.MapRec.Width - _map.MapData.TileSize.X * _map.MapData.MapZoomFactor) * _map.MapData.TileWorldDimension)
            {
                _cameraPosition.X = (_map.MapData.MapRec.Width - _map.MapData.TileSize.X * _map.MapData.MapZoomFactor) * _map.MapData.TileWorldDimension;
            }
            
            if (_cameraPosition.Y > (_map.MapData.MapRec.Height - _map.MapData.TileSize.Y * _map.MapData.MapZoomFactor) * _map.MapData.TileWorldDimension)
            {
                _cameraPosition.Y = (_map.MapData.MapRec.Height - _map.MapData.TileSize.Y * _map.MapData.MapZoomFactor) * _map.MapData.TileWorldDimension;
            }
        }
        
        private Vector2f GetCameraStartPosition()
        {   
            return new Vector2f(
                (Window.Size.X / 2 - _map.MapData.TileSize.X / 2) / _map.MapData.TileWorldDimension / _map.MapData.MapZoomFactor,
                (Window.Size.Y / 2 - _map.MapData.TileSize.Y / 2) / _map.MapData.TileWorldDimension / _map.MapData.MapZoomFactor
            );
        }

        private void DrawTileMap()
        {
            _map.SetWorldView(Window, _cameraPosition);
            
            Window.Draw(_map.GetBackgroundTileMap());
            
            DrawPlayer();
            
            Window.Draw(_map.GetForegroundTileMap());
        }
        
        private void DrawPlayer()
        {
            _player.SetPosition(_cameraPosition);
            Window.Draw(_player);
        }

        private void DrawInfoHud()
        {
            Window.Draw(_infoHud);
        }

        private void DrawCollisions()
        {
            if (!_showCollisions)
            {
                return;
            }
            
            foreach (var item in _map.MapData.CollisionLayer.CollisionRects)
            {
                var colRectangle = new RectangleShape(new Vector2f(item.Width, item.Height))
                {
                    Position = new Vector2f(item.Left, item.Top),
                    OutlineColor = new Color(255, 0, 0, 200),
                    OutlineThickness = 2,
                    FillColor = new Color(255, 0, 0, 50)
                };

                Window.Draw(colRectangle);
            }
        }
    }
}
