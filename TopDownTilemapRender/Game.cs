using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using TopDownTilemapRender.Core;
using TopDownTilemapRender.Core.Map;
using System.IO;
using TopDownTilemapRender.Core.Extensions;

namespace TopDownTilemapRender
{
    public class Game : BaseGame
    {
        private readonly Map _map;

        private Font _font;

        private View _infoHudView;
        
        private Vector2f _cameraPosition;

        public Game()
            : base(new Vector2u(1440, 810), "My World", Color.Black, 60, false, true)
        {
            _map = new Map();

            Console.WriteLine(Window.Size);
            
            _cameraPosition = GetCameraStartPosition();
            
        }

        protected override void LoadContent()
        {
            var mapPath = Path.Combine(
                Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "res", "maps", "tf_jungle_map.tmx");

            _map.Load(mapPath);
            
            var fontPath = Path.Combine(
                Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "res", "fonts", "arial.ttf");
            
            _font = new Font(fontPath);
        }

        protected override void Initialize()
        {
            _infoHudView = new View()
            {
                Viewport = new FloatRect(0.75f, 0, 0.25f, 0.25f),
            };
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
            _map.SetWorldView(Window, _cameraPosition);
            
            Window.Draw(_map.GetBackgroundTileMap());
            Window.Draw(_map.GetForegroundTileMap());
            
            DrawPlayer();

            DrawInfoHud();
        }

        protected override void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Q)
            {
                Window.Close();
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
        
        private void DrawPlayer()
        {
            var colRectangle =
                new RectangleShape(new Vector2f(
                    _map.MapData.TileSize.X * _map.MapData.TileWorldDimension,
                    _map.MapData.TileSize.Y * _map.MapData.TileWorldDimension))
                {
                    Position = new Vector2f(_cameraPosition.X, _cameraPosition.Y),
                    OutlineColor = new Color(25, 86, 255, 200),
                    OutlineThickness = 2,
                    FillColor = new Color(25, 86, 255, 100)
                };
            Window.Draw(colRectangle);
        }

        private void DrawInfoHud()
        {
            //TODO: this should be optimized
            
            Window.SetView(_infoHudView);
            
            var rectangle =
                new RectangleShape(new Vector2f(900, 500))
                {
                    Position = new Vector2f(50, 50),
                    OutlineColor = new Color(48, 48, 48, 230),
                    OutlineThickness = 10,
                    FillColor = new Color(48, 48, 48, 180)
                };
            
            Window.Draw(rectangle);
            
            var text = new Text()
            {
                DisplayedString = "Move - Arrows\n\nZoom - PageUp, PageDown\n\nQ - Exit",
                Font = _font,
                CharacterSize = 70,
                FillColor = Color.White,
                Style = Text.Styles.Regular,
                Position = new Vector2f(60, 80)
            };
            
            Window.Draw(text);
        }
    }
}
