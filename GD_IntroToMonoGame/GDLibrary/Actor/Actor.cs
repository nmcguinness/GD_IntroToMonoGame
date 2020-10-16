using GD_IntroToMonoGame.GDLibrary.Interfaces;
using Microsoft.Xna.Framework;

namespace GDLibrary
{
    public class Actor : IActor
    {
        private string id;
        //private string description;
       // private ActorType actorType;
  
        public string ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        public Actor(string id)
        {
            this.id = id;
        }

        public object Clone()
        {
            return new Actor(this.id);
        }

        public virtual float GetAlpha()
        {
            return -1;
        }

        public void Update(GameTime gameTime)
        {
            //does nothing
        }

        
    }
}
