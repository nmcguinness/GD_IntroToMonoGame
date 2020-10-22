﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace GDLibrary
{
    public class FirstPersonCameraController : IController
    {
        private KeyboardManager keyboardManager;
        private MouseManager mouseManager;
        private float moveSpeed, strafeSpeed, rotationSpeed;

        public FirstPersonCameraController(KeyboardManager keyboardManager,
            MouseManager mouseManager,
            float moveSpeed,
            float strafeSpeed, float rotationSpeed)
        {
            this.keyboardManager = keyboardManager;
            this.mouseManager = mouseManager;
            this.moveSpeed = moveSpeed;
            this.strafeSpeed = strafeSpeed;
            this.rotationSpeed = rotationSpeed;
        }

        public void Update(GameTime gameTime, IActor actor)
        {
            Actor3D parent = actor as Actor3D;

            if(parent != null)
            {
                HandleKeyboardInput(gameTime, parent);
                HandleMouseInput(gameTime, parent);
            }
        }

        private void HandleKeyboardInput(GameTime gameTime, Actor3D parent)
        {
            Vector3 moveVector = Vector3.Zero;

            if (this.keyboardManager.IsKeyDown(Keys.W))
                moveVector = parent.Transform3D.Look * this.moveSpeed;
            else if (this.keyboardManager.IsKeyDown(Keys.S))
                moveVector = -1*parent.Transform3D.Look * this.moveSpeed;

            if (this.keyboardManager.IsKeyDown(Keys.A))
                moveVector -= parent.Transform3D.Right * this.strafeSpeed;
            else if (this.keyboardManager.IsKeyDown(Keys.D))
                moveVector += parent.Transform3D.Right * this.strafeSpeed;

            //constrain movement in Y-axis
            moveVector.Y = 0;

            parent.Transform3D.TranslateBy(moveVector * gameTime.ElapsedGameTime.Milliseconds);
        }

        private void HandleMouseInput(GameTime gameTime, Actor3D parent)
        {
            Vector2 mouseDelta = this.mouseManager.GetDeltaFromCentre(new Vector2(512, 384));
            mouseDelta *= this.rotationSpeed * gameTime.ElapsedGameTime.Milliseconds;

            if (mouseDelta.Length() != 0)
                parent.Transform3D.RotateBy(new Vector3(-1 * mouseDelta, 0));

        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

    }
}
