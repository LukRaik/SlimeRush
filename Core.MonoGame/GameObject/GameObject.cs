using System;
using System.Collections.Generic;
using System.Linq;
using Core.MonoGame.Animation;
using Core.MonoGame.Events;
using Core.MonoGame.Interfaces;
using Core.MonoGame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Core.MonoGame.Interfaces.IDrawable;
using IUpdateable = Core.MonoGame.Interfaces.IUpdateable;

namespace Core.MonoGame.GameObject
{
    public abstract class GameObject :
        IDrawable,
        IUpdateable,
        ICollidable
    {
        private ICollection<ICollidable> _collidables;

        private readonly IAnimation _animation;

        private Vector2 _position;

        private event OnGameObjectUpdateEvent _onUpdate;

        private float _speed;


        protected GameObject(Vector2 position, IAnimation animation)
        {
            _position = position;

            _animation = animation;

            SetSpeed();
        }

        public void Register(OnGameObjectUpdateEvent handler)
        {
            _onUpdate += handler;
        }

        public void UnRegister(OnGameObjectUpdateEvent handler)
        {
            _onUpdate -= handler;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _animation.Draw(spriteBatch, _position);

#if DEBUG
            spriteBatch.DrawRectangle(GetBoundry(), Color.Red);
#endif
        }

        public void SetSpeed(float speed = 1f)
        {
            _speed = speed;
        }

        public void Move(Vector2 vector)
        {
            _position += Vector2.Multiply(vector, _speed);
        }

        public Vector2 CurPosition()
        {
            return _position;
        }

        public void Update(GameTime gameTime)
        {
            _onUpdate?.Invoke(this, gameTime);

            _animation.Update(gameTime);
        }

        public void SetCollisionObjects(IList<ICollidable> collidableObjects)
        {
            _collidables = collidableObjects;
        }

        public ICollection<ICollidable> ColidingMove(Vector2 moveVector)
        {
            var collidingWith = Collides(moveVector);

            if (collidingWith.Any()) return collidingWith;

            this.Move(moveVector);

            return collidingWith;
        }

        public Rectangle GetBoundry()
        {
            var myRectangle = _animation.GetRectangle();

            myRectangle.X += (int)_position.X;
            myRectangle.Y += (int)_position.Y;

            return myRectangle;
        }

        public ICollection<ICollidable> Collides(Vector2 moveVector)
        {
            List<ICollidable> collidedObjects = new List<ICollidable>();
            if (!_collidables.Any()) return collidedObjects;

            var myBoundry = GetBoundry();

            var multipledMoveVector = Vector2.Multiply(moveVector, _speed);

            myBoundry.X += (int)multipledMoveVector.X;
            myBoundry.Y += (int)multipledMoveVector.Y;

            foreach (var obj in _collidables.Where(x => x != this))
            {
                var hisBoundry = obj.GetBoundry();
                if (hisBoundry.Intersects(myBoundry)) collidedObjects.Add(obj);
            }

            return collidedObjects;
        }
    }
}