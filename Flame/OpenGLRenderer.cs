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
    class OpenGLRenderer
    {
        public OpenGLRenderer()
        {
            Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            GL.ClearColor(Color.BlueViolet);          
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);
        }

        public void Draw()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            DrawTriangle();
        }

        public void DrawTriangle()
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.Green);
            GL.Vertex3(-1.0f, -1.0f, 4.0f);
            GL.Color3(Color.Blue);
            GL.Vertex3(1.0f, -1.0f, 4.0f);
            GL.Color3(Color.Red);
            GL.Vertex3(0.0f, 1.0f, 4.0f);

            GL.End();
        }
    }
}
