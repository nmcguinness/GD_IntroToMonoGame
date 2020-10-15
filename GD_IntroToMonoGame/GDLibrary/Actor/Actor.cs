using GD_IntroToMonoGame.GDLibrary.Interfaces;
using Microsoft.Xna.Framework;

namespace GDLibrary
{
    public class Actor : IActor
    {
        private string id;
        private string description;
       // private ActorType actorType;
  
        public Actor(string id)
        {
            this.id = id;
        }

        public void Update(GameTime gameTime)
        {
            //does nothing
        }
    }
}
