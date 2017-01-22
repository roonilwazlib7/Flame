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
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        }

        public double Rotation { get; set; }

        public void Draw()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public void PreSpriteDraw(Sprite sprite)
        {
            double x = sprite.Position.X + sprite.Pivot.X;
            double y = sprite.Position.Y + sprite.Pivot.Y;
            GL.PushMatrix();
            GL.Translate(x, y, _z);
            GL.Rotate(sprite.Rotation.Value, 0, 0, 1);
        }

        public void PostSpriteDraw(Sprite sprite)
        {
            GL.PopMatrix();
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
            GL.Color4(color.R, color.G, color.B, 1.0);

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

        public void DrawLine(Geometry.Vector start, Geometry.Vector end, Color color, float lineWidth = 1)
        {
            GL.LineWidth(lineWidth);
            GL.Begin(PrimitiveType.Lines);

            GL.Color4(color.R, color.G, color.B, color.A);

            GL.Vertex2(start.X, start.Y);
            GL.Vertex2(end.X, end.Y);

            GL.End();
        }

        public void DrawTexture(Assets.Texture texture, Assets.TextureMap textureMap, Geometry.Rectangle textureShape, Color color, double opacity, Geometry.Vector pivot)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texture.Id);
            GL.Begin(PrimitiveType.Triangles);

            double x = -pivot.X;
            double y = -pivot.Y;

            GL.Color4(color.R, color.G, color.B, opacity);

            GL.TexCoord2(textureMap.U, textureMap.V); // top left
            GL.Vertex3(0 + x, 0 + y, _z);
            
            GL.TexCoord2(textureMap.U + textureMap.Width, textureMap.V); //top right
            GL.Vertex3(textureShape.Width + x, 0 + y, _z);

            GL.TexCoord2(textureMap.U, textureMap.V + textureMap.Height); //bottom left
            GL.Vertex3(0 + x, textureShape.Height + y, _z);

            GL.TexCoord2(textureMap.U + textureMap.Width, textureMap.V); //top right
            GL.Vertex3(textureShape.Width + x, 0 + y, _z);

            GL.TexCoord2(textureMap.U + textureMap.Width, textureMap.V + textureMap.Height); // bottom right
            GL.Vertex3(textureShape.Width + x, textureShape.Height + y, _z);

            GL.TexCoord2(textureMap.U, textureMap.V + textureMap.Height); // bottom left
            GL.Vertex3(0 + x, textureShape.Height + y, _z);

            GL.End();        
            GL.Disable(EnableCap.Texture2D);
        }
    }
}
