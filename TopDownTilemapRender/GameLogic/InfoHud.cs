using SFML.Graphics;
using SFML.System;

namespace TopDownTilemapRender.GameLogic
{
    public class InfoHud : Drawable
    {
        private readonly View _infoHudView;

        private readonly Font _font;

        private RectangleShape _backgroundRect;

        private Text _text;

        public InfoHud(Font font)
        {
            _font = font;
            
            _infoHudView = new View()
            {
                // Top right corner
                Viewport = new FloatRect(0.75f, 0, 0.25f, 0.25f),
            };

            CreateBackgroundRect();
            CreateText();
        }
        
        public void Draw(RenderTarget target, RenderStates states)
        {
            target.SetView(_infoHudView);
            
            target.Draw(_backgroundRect);
            
            target.Draw(_text);
        }

        private void CreateBackgroundRect()
        {
            _backgroundRect = new RectangleShape(new Vector2f(900, 500))
            {
                Position = new Vector2f(50, 50),
                OutlineColor = new Color(48, 48, 48, 230),
                OutlineThickness = 10,
                FillColor = new Color(48, 48, 48, 180)
            };
        }

        private void CreateText()
        {
            _text = new Text()
            {
                DisplayedString = "Move - Arrows\n\nZoom - PageUp, PageDown\n\nQ - Exit",
                Font = _font,
                CharacterSize = 70,
                FillColor = Color.White,
                Style = Text.Styles.Regular,
                Position = new Vector2f(60, 80)
            };
        }
    }
}