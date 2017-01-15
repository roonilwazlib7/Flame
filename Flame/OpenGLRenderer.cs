using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Flame.Sprites;

namespace Flame
{
    public class OpenGLRenderer
    {
        float _z = 0; // we're in 2D
        double _DEGREES_TO_RADIANS = Math.PI / 180;
        public OpenGLRenderer()
        {
            Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            GL.ClearColor(Color.BlueViolet);          
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            Rotation = 0;
        }

        public double Rotation { get; set; }

        public void Draw()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public void PreSpriteDraw(Sprite sprite)
        {
            GL.Rotate(sprite.Rotation, Vector3d.UnitZ);
        }

        public void PostSpriteDraw(Sprite sprite)
        {
            GL.Rotate(-sprite.Rotation, Vector3d.UnitZ);
        }

        public void DrawTriangle(Geometry.Triangle triangle, Color color)
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(color);

            GL.Vertex3(triangle.Vertex1.X, triangle.Vertex1.Y, _z);
            GL.Vertex3(triangle.Vertex2.X, triangle.Vertex2.Y, _z);
            GL.Vertex3(triangle.Vertex3.X, triangle.Vertex3.Y, _z);

            GL.End();
        }

        public void DrawEmptyCircle(Geometry.Circle circle, Color color)
        {
            GL.Begin(PrimitiveType.LineLoop);
            GL.Color3(color);

            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * _DEGREES_TO_RADIANS;
                double x = Math.Cos(degInRad) * circle.Radius;
                double y = Math.Sin(degInRad) * circle.Radius;

                x += circle.Radius + circle.X;
                y += circle.Radius + circle.Y;

                GL.Vertex2(x, y);
            }

            GL.End();
        }

        public void DrawRectangle()
        {

        }

        public void DrawTexture(Flame.Assets.Texture texture)
        {

        }
    }
}
