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
        private Sprite _parent;
        private Vector _position;
        #region Events
        public event FlameMessageHandler OnClick;
        public event FlameMessageHandler OnMouseEnter;
        public event FlameMessageHandler OnMouseLeave;
        public event FlameMessageHandler OnMouseDown;
        public event FlameMessageHandler OnClickAway;
        #endregion

        public Sprite(Game game, int x, int y) : base()
        {
            _position = new Vector(x, y);
            Pivot = new Vector(0, 0);
            Repeat = new Vector(0, 0);
            Color = Color.White;
            TextureMap = new TextureMap();
            Rotation = new SpriteRotation(0.0);
            Opacity = new SpriteOpacity(1.0);
            Game = game;

            Body = new Modules.Body(this);
            Events = new Modules.Events(this);
            Path = new Modules.Path(this);

            State = new StateMachine<Sprite>(this);

            _renderer = game.Renderer;
        }

        #region Event Triggers
        public void TriggerClick(Message m)
        {
            OnClick?.Invoke(this, m);
        }
        public void TriggerMouseEnter(Message m)
        {
            OnMouseEnter?.Invoke(this, m);
        }
        public void TriggerMouseLeave(Message m)
        {
            OnMouseLeave?.Invoke(this, m);
        }
        public void TriggerMouseDown(Message m)
        {
            OnMouseDown?.Invoke(this, m);
        }
        public void TriggerClickAway(Message m)
        {
            OnClickAway?.Invoke(this, m);
        }
        #endregion

        #region Properties
        public Vector Pivot { get; set; }
        public Vector Repeat { get; set; }
        public Color Color { get; set; }
        public TextureMap TextureMap { get; set; }
        public Modules.Body Body { get; set; }
        public Modules.Events Events { get; }
        public Modules.Path Path { get; }
        public StateMachine<Sprite> State { get; set; }
        public SpriteRotation Rotation { get; set; }
        public SpriteOpacity Opacity { get; set; }
        public Sprite Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }
        public Vector Position
        {
            set
            {
                _position = value;
            }
            get
            {
                if (_parent != null)
                {
                    return _parent.Position + _position;
                }
                else
                {
                    return _position;
                }
            }
        }
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
                    Vector c = new Vector(Position.X, Position.Y);
                    c.X += Rectangle.HalfWidth;
                    c.Y += Rectangle.HalfHeight;
                    return c;
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
                Geometry.Rectangle drawRec = new Geometry.Rectangle(0, 0, Rectangle.Width, Rectangle.Height);

                Geometry.Rectangle viewRec = new Geometry.Rectangle(0, 0, Game.ClientSize.Width, Game.ClientSize.Height);

                if (Position.X < 0 || Position.X > viewRec.Width || Position.Y < 0 || Position.Y > viewRec.Height)
                {
                    _renderer.PostSpriteDraw(this);
                    return;
                }

                for (int i = 0; i < Repeat.X + 1; i++)
                {
                    for (int j = 0; j < Repeat.Y + 1; j++)
                    {
                        drawRec.X = i * drawRec.Width;
                        drawRec.Y = j * drawRec.Height;
                        _renderer.DrawTexture(_renderTexture, TextureMap, drawRec, Color, Opacity.Value, Pivot);
                    }
                }                
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
            State.Update();
            Path.Update();
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
            TextureMap.Texture = _renderTexture;
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

        public void Add(Sprite sp)
        {
            sp.Parent = this;
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
