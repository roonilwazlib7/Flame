using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Flame.Assets;
using System.IO;

namespace Flame
{
    public class AssetManager
    {
        Dictionary<string, Texture> Textures;
        Dictionary<string, string> Files;
        public AssetManager()
        {
            Textures = new Dictionary<string, Texture>();
            Files = new Dictionary<string, string>();
        }
        public void LoadTexture(string path, string id)
        {
            Bitmap bitmap = new Bitmap(path);

            int tex;
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out tex);
            GL.BindTexture(TextureTarget.Texture2D, tex);

            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);


            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            Texture texture = new Texture(tex);
            texture.Width = data.Width;
            texture.Height = data.Height;

            Textures.Add(id, texture);
        }

        public void LoadFile(string path, string id)
        {
            StreamReader r = new StreamReader(path);

            Files.Add(id, r.ReadToEnd());

            r.Close();
        }

        public Texture GetTexture(string id)
        {
            return Textures[id];
        }

        public bool TextureExists(string id)
        {
            return Textures.ContainsKey(id);
        }

        public string GetFile(string id)
        {
            return Files[id];
        }
    }
}
