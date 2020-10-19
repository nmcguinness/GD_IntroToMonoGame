﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;

namespace GDLibrary
{
    public class Actor : IActor
    {
        #region Fields
        private string id, description;
        private ActorType actorType;
        private StatusType statusType;
        #endregion

        #region Properties
        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        public ActorType ActorType
        {
            get
            {
                return actorType;
            }
            set
            {
                actorType = value;
            }
        }

        public StatusType StatusType
        {
            get
            {
                return statusType;
            }
            set
            {
                statusType = value;
            }
        }
        #endregion

        #region Constructors
        public Actor(string id, ActorType actorType, StatusType statusType)
        {
            this.id = id;
            this.actorType = actorType;
            this.statusType = statusType;
        }
        #endregion

        public virtual void Update(GameTime gameTime)
        {
            //do nothing - see child implementation
        }

        public virtual void Draw(GameTime gameTime, Camera3D camera, GraphicsDevice graphicsDevice)
        {
             //do nothing - see child implementation
        }

        public override bool Equals(object obj)
        {
            return obj is Actor actor &&
                   id == actor.id &&
                   description == actor.description &&
                   actorType == actor.actorType &&
                   statusType == actor.statusType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, description, actorType, statusType);
        }

        public object Clone()
        {
            //deep-copy
            return new Actor(this.id, this.actorType, this.statusType);
        }
    }
}
