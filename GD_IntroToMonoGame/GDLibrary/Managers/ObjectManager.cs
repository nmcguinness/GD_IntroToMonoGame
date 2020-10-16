using GD_IntroToMonoGame.GDLibrary.Interfaces;
using Microsoft.Xna.Framework;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;

namespace GDLibrary
{
    public class ObjectManager : DrawableGameComponent
    {
        private List<IActor> opaqueList, transparentList;

        public ObjectManager(Game game, int initialOpaqueDrawSize, int initialTransparentDrawSize) : base(game)
        {
            this.opaqueList = new List<IActor>(initialOpaqueDrawSize);
            this.transparentList = new List<IActor>(initialTransparentDrawSize);
        }

        public void Add(IActor actor)
        {

        }

        public bool RemoveIf(Predicate<IActor> predicate)
        {
            return true;
        }

        public int RemoveAllIf(Predicate<IActor> predicate)
        {
            return -1;
        }
    }
}
