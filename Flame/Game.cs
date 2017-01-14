using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Flame
{
    public class Game: GameWindow
    {
        private List<GameThing> _things;
        private List<GameThing> _cleanedThings;
        private OpenGLRenderer _renderer;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Title = "Flame";
            _renderer = new OpenGLRenderer();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            _renderer.Draw();
            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        public void Add(GameThing thing)
        {
            _things.Add(thing);
        }

        public void Update()
        {

        }

        public void Draw()
        {

        }

        public void UpdateThings()
        {
            
        }
    }
}
