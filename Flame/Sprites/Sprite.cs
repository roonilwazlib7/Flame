using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Flame.Geometry;
using Flame.Assets;
namespace Flame.Sprites
{
    public class Sprite: GameThing
    {
        private object _renderObject;
        private Triangle _renderTriangle;
        private Geometry.Rectangle _renderRectangle;
        private Texture _renderTexture;
        private OpenGLRenderer _renderer;

        public Sprite(Game game, int x, int y): base()
        {
            Position = new Vector(x, y);
            Pivot = new Vector(0, 0);
            Color = Color.White;
            Rotation = 0;
            Game = game;

            _renderer = game.Renderer;
        }
        public Vector Position { get; set; }
        public Vector Pivot { get; set; }
        public Color Color { get; set; }
        public double Rotation { get; set; }
        public double Opacity { get; set; }

        public override void Draw()
        {
            _renderer.PreSpriteDraw(this);

            if(_renderObject is Triangle)
            {
                _renderer.DrawTriangle(_renderTriangle, Color);
            }
            else if (_renderObject is Texture)
            {
                _renderer.DrawTexture(_renderTexture);
            }
            else
            {
                throw new Exception(string.Format("Unknown render object: {0}", _renderObject.GetType().ToString()));
            }

            _renderer.PostSpriteDraw(this);
        }

        public override void Update()
        {
            Rotation += 20 * Game.Delta;
        }

        public Sprite BindToTriangle(Triangle triangle)
        {
            _renderObject = _renderTriangle = triangle;
            return this;
        }

        public Sprite BindToTexture(Texture texture)
        {
            _renderObject = _renderTexture = texture;
            return this;
        }

        public Sprite BindToTexture(string textureId)
        {
            _renderObject = _renderTexture = Game.Assets.GetTexture(textureId);
            return this;
        }
    }
}
