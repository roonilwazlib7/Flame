using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace Flame.Sprites.Modules
{
    public class Events: Module
    {
        MouseState _oldMouse;
        bool _mouseWasInRec;

        public Events(Sprite sprite):base(sprite)
        {
           _oldMouse  = Mouse.GetState();
        }
        public void Update()
        {
            MouseState mouse = Mouse.GetState();
            bool mouseInRec = MouseInRec();
            if (mouseInRec && !_mouseWasInRec)
            {
                Sprite.Emit("MouseEnter", new Message(Sprite));
            }
            if (!mouseInRec && _mouseWasInRec)
            {
                Sprite.Emit("MouseLeave", new Message(Sprite));
            }
            if (mouse.LeftButton == ButtonState.Released  )
            {
                if (mouseInRec && _oldMouse.LeftButton == ButtonState.Pressed)
                {
                    Sprite.Emit("Click", new Message(Sprite));
                }
            }
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if( mouseInRec && _oldMouse.LeftButton == ButtonState.Released)
                {
                    Sprite.Emit("MouseDown", new Message(Sprite));
                }
            }
            if (mouse.LeftButton == ButtonState.Released)
            {
                if (!mouseInRec && _oldMouse.LeftButton == ButtonState.Pressed)
                {
                    Sprite.Emit("ClickAway", new Message(Sprite));
                }
            }


            _mouseWasInRec = mouseInRec;
            _oldMouse = mouse;
        }
        private bool MouseInRec()
        {
            return (Sprite.Game.Mouse.X >= Sprite.Position.X && Sprite.Game.Mouse.X <= Sprite.Position.X + Sprite.Rectangle.Width)
                && (Sprite.Game.Mouse.Y >= Sprite.Position.Y && Sprite.Game.Mouse.Y <= Sprite.Position.Y + Sprite.Rectangle.Height);
        }
    }
}
