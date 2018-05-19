using SFML.Graphics;
using SFML.System;

namespace TopDownTilemapRender.GameLogic
{   
    public class Player : Drawable
    {
        private RectangleShape _playerRect;
        
        public Player(Vector2i size, int tileDimension)
        {
            CreatePlayerBody(size, new Vector2f(100, 100), tileDimension);
        }

        public void SetPosition(Vector2f position)
        {
            _playerRect.Position =
                new Vector2f(position.X, position.Y);
        }
        
        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_playerRect);
        }

        private void CreatePlayerBody(Vector2i size, Vector2f position, int tileDimension)
        {
            _playerRect =
                new RectangleShape(new Vector2f(size.X * tileDimension, size.Y * tileDimension))
                {
                    Position = new Vector2f(position.X, position.Y),
                    OutlineColor = new Color(25, 86, 255, 200),
                    OutlineThickness = 2,
                    FillColor = new Color(25, 86, 255, 100)
                };
        }
    }
}