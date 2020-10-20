using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace GDLibrary
{
    public class FirstPersonCameraController : IController
    {
        private KeyboardManager keyboardManager;

        public FirstPersonCameraController(KeyboardManager keyboardManager)
        {
            this.keyboardManager = keyboardManager;
        }

        public void Update(GameTime gameTime, IActor actor)
        {
            Actor3D parent = actor as Actor3D;

            if(parent != null)
            {
                if (this.keyboardManager.IsKeyDown(Keys.W))
                    parent.Transform3D.TranslateBy(parent.Transform3D.Look * 0.1f);

            }
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

    }
}
