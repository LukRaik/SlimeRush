using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;

namespace Core.MonoGame.Interfaces
{
    public interface ICollidable:IMovable
    {
        void SetCollisionObjects(IList<ICollidable> collidableObjects);

        ICollection<ICollidable> ColidingMove(Vector2 moveVector);

        Rectangle GetBoundry();

        ICollection<ICollidable> Collides(Vector2 moveVector);
    }
}