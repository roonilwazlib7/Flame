using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Flame.Games;
using Flame.Sprites;
using Newtonsoft.Json;

namespace Flame.Sprites
{
    public enum TextureMapType { Torch }
    public class TextBox
    {
        List<Sprite> _characters = new List<Sprite>();
        TorchTextureMap _textureMap;
        Color _color;
        Game _game;
        string _text = "";
        string _textureId = "";

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

        private void CreateTextSprites()
        {
            char[] splits = _text.ToCharArray();
            double currentX = 50;
            foreach (char c in splits)
            {
                if (c == ' ' || c == '\n' || c == '\t' || c == '!')
                {
                    continue;
                }
                Sprite sp = new Sprite(_game, (int)currentX, 50);
                TorchTextureMapSection sc = _textureMap.Textures[c.ToString()];
                sp.BindToTexture(_textureId);

                sp.TextureMap.ClipX = sc.ClipX;
                sp.TextureMap.ClipY = sc.ClipY;
                sp.Rectangle.Width = sp.TextureMap.ClipWidth = sc.ClipWidth;
                sp.Rectangle.Height = sp.TextureMap.ClipHeight = sc.ClipHeight;

                sp.Color = _color;
                _game.Add(sp);
                _characters.Add(sp);
                currentX += sc.ClipWidth;
            }
        }

        public double Width { get; set; }
        public double height { get; set; }
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
