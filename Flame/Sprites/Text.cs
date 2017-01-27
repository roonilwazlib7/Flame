using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Flame.Games;
using Flame.Sprites;
using Flame.Geometry;
using Newtonsoft.Json;
using Flame.Assets;

namespace Flame.Sprites
{
    public enum TextureMapType { Torch }
    public class TextBox
    {
        List<Character> _characters = new List<Character>();
        TorchTextureMap _textureMap;
        Color _color;
        Game _game;
        string _text = "";
        string _textureId = "";
        double _x = 0;
        double _y = 0;

        public TextBox(Game game, string textureMapJsonId, string textureId, Color color, TextureMapType textureMapType = TextureMapType.Torch)
        {
            _textureMap = JsonConvert.DeserializeObject<TorchTextureMap>(game.Assets.GetFile(textureMapJsonId));
            _color = color;
            _game = game;
            _textureId = textureId;
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                CreateTextSprites();

            }
        }

        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                foreach (Character c in _characters)
                {
                    c.Position.X = c.OffsetX + _x;
                }
            }
        }

        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                foreach (Character c in _characters)
                {
                    c.Position.Y = c.OffsetY + _y;
                }
            }
        }

        private void CreateTextSprites()
        {
            if (_characters != null && _characters.Count > 0 )
            {
                foreach(Character c in _characters)
                {
                    c.Trash();
                }
                _characters.Clear();
            }
            char[] splits = _text.ToCharArray();
            double currentX = _x;
            double currentY = _y;
            Texture texture = _game.Assets.GetTexture(_textureId);
            foreach (char c in splits)
            {
                switch (c)
                {
                    case ' ':
                    case '\t':
                    case '!':
                    case ':':
                        continue;

                    case '\n':
                        currentY += texture.Height;
                        currentX = _x;
                        break;

                    default:
                        Character sp = new Character(_game, (int)currentX, (int)currentY);
                        TorchTextureMapSection sc = _textureMap.Textures[c.ToString()];
                        sp.BindToTexture(_textureId);

                        sp.OffsetX = currentX;
                        sp.OffsetY = currentY;
                        sp.TextureMap.ClipX = sc.ClipX;
                        sp.TextureMap.ClipY = sc.ClipY;
                        sp.Rectangle.Width = sp.TextureMap.ClipWidth = sc.ClipWidth;
                        sp.Rectangle.Height = sp.TextureMap.ClipHeight = sc.ClipHeight;
                        sp.LayerIndex = 1000;

                        sp.Color = _color;
                        _game.Add(sp);
                        _characters.Add(sp);
                        currentX += sc.ClipWidth;
                        break;
                }

            }
        }

        public void SetPosition(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Width { get; set; }
        public double height { get; set; }
    }

    class Character:Sprite
    {
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
        public Character(Game game, int x, int y):base(game, x, y)
        {
            OffsetX = 0;
            OffsetY = 0;
        }
    }

    class TorchTextureMap
    {
        public Dictionary<string, TorchTextureMapSection> Textures { get; set; }
    }

    class TorchTextureMapSection
    {
        public string Name { get; set; }
        public double ClipX { get; set; }
        public double ClipY { get; set; }
        public double ClipWidth { get; set; }
        public double ClipHeight { get; set; }
    }

}
