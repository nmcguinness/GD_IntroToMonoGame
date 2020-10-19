﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GDLibrary
{
    public class ObjectManager : DrawableGameComponent
    {
        private Camera3D camera3D;
        private List<DrawnActor3D> opaqueList, transparentList;

        public ObjectManager(Game game, 
            int initialOpaqueDrawSize, int initialTransparentDrawSize,
            Camera3D camera3D) : base(game)
        {
            this.camera3D = camera3D;
            this.opaqueList = new List<DrawnActor3D>(initialOpaqueDrawSize);
            this.transparentList = new List<DrawnActor3D>(initialTransparentDrawSize);
        }

        public void Add(DrawnActor3D actor)
        {
            if (actor.EffectParameters.Alpha < 1)
                this.transparentList.Add(actor);
            else
                this.opaqueList.Add(actor);
        }

        public bool RemoveIf(Predicate<DrawnActor3D> predicate)
        {

         //   RemoveIf(actor => actor.ID.Equals("dungeon powerup key"));

            int position = this.opaqueList.FindIndex(predicate);

            if (position != -1)
            {
                this.opaqueList.RemoveAt(position);
                return true;
            }

            return false;
        }

        public int RemoveAllIf(Predicate<DrawnActor3D> predicate)
        {
            //to do...
            return -1;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (DrawnActor3D actor in this.opaqueList)
            {
                if((actor.StatusType & StatusType.Update) == StatusType.Update)
                    actor.Update(gameTime);
            }
               
        //    base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (DrawnActor3D actor in this.opaqueList)
            {
                if ((actor.StatusType & StatusType.Drawn) == StatusType.Drawn)
                    actor.Draw(gameTime, this.camera3D, this.GraphicsDevice);
            }

        //    base.Draw(gameTime);
        }




    }
}
