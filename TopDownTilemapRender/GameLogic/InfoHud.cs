using SFML.Graphics;
using SFML.System;
using TopDownTilemapRender.Core.Managers;

namespace TopDownTilemapRender.GameLogic
{
    public class InfoHud : Drawable
    {
        private readonly View _infoHudView;

        private RectangleShape _backgroundRect;

        private Text _text;

        public InfoHud()
        {   
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
            _backgroundRect = new RectangleShape(new Vector2f(900, 650))
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
                DisplayedString = "Move - Arrows\n\nZoom - PageUp, PageDown\n\nShow Collisions - C\n\nQ - Exit",
                Font = AssetManager.Instance.Font.Get("arial"),
                CharacterSize = 70,
                FillColor = Color.White,
                Style = Text.Styles.Regular,
                Position = new Vector2f(60, 80)
            };
        }
    }
}