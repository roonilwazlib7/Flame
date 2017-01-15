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

namespace Flame
{
    public class Game: GameWindow
    {
        private List<GameThing> _things;
        private List<GameThing> _cleanedThings;
        private OpenGLRenderer _renderer;
        private AssetManager _assetManager = new AssetManager();

        public Game(): base()
        {
            _cleanedThings = new List<GameThing>();
            _things = new List<GameThing>();
            _renderer = new OpenGLRenderer();
        }

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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Title = "Flame";
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
            _things.Add(thing);
        }

        public void Update()
        {
            foreach(GameThing thing in _things)
            {
                thing.Update();
            }
        }

        public void Draw()
        {
            foreach(GameThing thing in _things)
            {
                thing.Draw();
            }
        }

        public void UpdateThings()
        {
            
        }
    }
}
