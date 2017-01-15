using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Flame.Geometry;
namespace Flame.Sprites
{
    public class Sprite: GameThing
    {
        private object _renderObject;
        private Triangle _renderTriangle;
        private OpenGLRenderer _renderer;

        public Sprite(Game game, int x, int y)
        {
            Position = new Vector(x, y);
            Color = Color.White;
            Rotation = 0;
            Game = game;

            _renderer = game.Renderer;
        }
        public Vector Position { get; set; }
        public Color Color { get; set; }
        public double Rotation { get; set; }

        public override void Draw()
        {
            _renderer.PreSpriteDraw(this);

            if(_renderObject is Triangle)
            {
                _renderer.DrawTriangle(_renderTriangle, Color);
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
    }
}
