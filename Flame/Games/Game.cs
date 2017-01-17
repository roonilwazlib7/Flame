using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Flame;
using Flame.Sprites;
using Flame.Games.Modules;
using OpenTK.Input;

namespace Flame.Games
{
    public class Game: GameWindow
    {
        private List<GameThing> _things;
        private List<GameThing> _cleanedThings;
        private OpenGLRenderer _renderer;

        private AssetManager _assetManager = new AssetManager();

        private int _uidCounter = 0;

        public Game(string title = "FlameGame"): base()
        {
            _cleanedThings = new List<GameThing>();
            _things = new List<GameThing>();
            _renderer = new OpenGLRenderer();

            Particles = new Particles(this);
            Tween = new Tween(this);
            Timer = new Timer(this);
            Factory = new Factory(this);

            Title = title;
            WindowState = WindowState.Maximized;

            LoadAssets();
            Initialize();
        }

        #region Properties
        public Particles Particles { get; }
        public Tween Tween { get; }
        public Timer Timer { get; }
        public Factory Factory { get; }

        public AssetManager Assets
        {
            get
            {
                return _assetManager;
            }
        }

        public OpenGLRenderer Renderer
        {
            get
            {
                return _renderer;
            }
        }

        public double Delta
        {
            get
            {
                return RenderTime;
            }
        }
        #endregion

        #region Events
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetUpGL(0, 0, ClientRectangle.Width, ClientRectangle.Height);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            _renderer.Draw();
            Draw();
            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            Update();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetUpGL(0, 0, ClientRectangle.Width, ClientRectangle.Height);
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
            
        }
        #endregion

        private void SetUpGL(double x, double y, double width, double height)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;

            GL.Viewport((int)x, (int)y, (int)width, (int)height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, width, height, 0, -100, 100);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        public void Add(GameThing thing)
        {
            thing.Uid = _uidCounter++;
            _things.Add(thing);
        }

        public virtual void Update()
        {
           _cleanedThings.Clear();
            foreach(GameThing thing in _things)
            {
                thing.Update();
                if (!thing.Trashed)
                {
                   _cleanedThings.Add(thing);
                }
            }
            _things.Clear();
            _things.AddRange(_cleanedThings);
        }

        public virtual void Draw()
        {
            //_things.Sort();
            foreach(GameThing thing in _things)
            {
                thing.Draw();
            }
        }

        public virtual void LoadAssets()
        {

        }

        public virtual void UnLoadAssets()
        {

        }

        public virtual void Initialize()
        {

        }
    }
}
