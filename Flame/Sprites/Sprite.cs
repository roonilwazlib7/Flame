using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Flame.Geometry;
using Flame.Assets;
using Flame.Games;
namespace Flame.Sprites
{
    public class Sprite : GameThing
    {
        private object _renderObject;
        private Triangle _renderTriangle;
        private Geometry.Rectangle _renderRectangle;
        private Texture _renderTexture;
        private OpenGLRenderer _renderer;

        public Sprite(Game game, int x, int y) : base()
        {
            Position = new Vector(x, y);
            Pivot = new Vector(0, 0);
            Color = Color.White;
            TextureMap = new TextureMap();
            Rotation = new SpriteRotation(0.0);
            Opacity = new SpriteOpacity(1.0);
            Game = game;

            Body = new Modules.Body(this);
            Events = new Modules.Events(this);

            _renderer = game.Renderer;
        }

        #region Properties
        public Vector Position { get; set; }
        public Vector Pivot { get; set; }
        public Color Color { get; set; }
        public TextureMap TextureMap { get; set; }
        public Modules.Body Body { get; set; }
        public Modules.Events Events { get; }
        public SpriteRotation Rotation { get; set; }
        public SpriteOpacity Opacity { get; set; }
        public Geometry.Rectangle Rectangle
        {
            get
            {
                return _renderRectangle;
            }
        }

        public Vector Center
        {
            get
            {
                if (_renderObject is Texture)
                {
                    return _renderRectangle.Center;
                }
                else
                {
                    return new Vector(0, 0);
                }
            }
        }
        #endregion

        public override void Draw()
        {
            _renderer.PreSpriteDraw(this);

            if (_renderObject is Triangle)
            {
                _renderer.DrawTriangle(_renderTriangle, Color);
            }
            else if (_renderObject is Texture)
            {
                _renderer.DrawTexture(_renderTexture, TextureMap, _renderRectangle, Color, Opacity.Value, Pivot);
            }
            else
            {
                throw new Exception(string.Format("Unknown render object: {0}", _renderObject.GetType().ToString()));
            }

            _renderer.PostSpriteDraw(this);
        }

        public override void Update()
        {
            Body.Update();
            Events.Update();
        }

        #region Binding
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
            _renderRectangle = new Geometry.Rectangle(Position.X, Position.Y, _renderTexture.Width, _renderTexture.Height);
            return this;
        }
        #endregion

        #region General
        public Sprite PivotCenter()
        {
            Pivot.X = _renderRectangle.HalfWidth;
            Pivot.Y = _renderRectangle.HalfHeight;
            return this;
        }
        #endregion
    }

    public class SpriteOpacity
    {
        public SpriteOpacity(double value)
        {
            Value = value;
        }
        public double Value { get; set; }
    }

    public class SpriteRotation
    {
        public SpriteRotation(double value)
        {
            Value = value;
        }
        public double Value { get; set; }
    }
}
