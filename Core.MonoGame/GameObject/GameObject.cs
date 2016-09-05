using System;
using System.Collections.Generic;
using System.Linq;
using Core.Errors;
using Core.MonoGame.Animation;
using Core.MonoGame.Animation.Enum;
using Core.MonoGame.Animation.Impl;
using Core.MonoGame.Events;
using Core.MonoGame.Interfaces;
using Core.MonoGame.ObjectAction;
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
        ICollidable,
        IAnimateable
    {
        private ICollection<ICollidable> _collidables;

        private readonly IAnimation _animation;

        private IObjectAction _action;

        protected Vector2 Position { get; private set; }

        protected event GameObjectEvent OnUpdate;

        public AnimCode CurrentAnimation => _animation.CurrentAnimation;

        public float Speed { get; private set; }

        protected GameObject(Vector2 position, IAnimation animation = null)
        {
            Position = position;

            _animation = animation ?? new BlankAnimation();

            SetSpeed();
        }

        protected T GetAnimation<T>() where T : IAnimation
        {
            var type = typeof(T);
            var animType = typeof(T);
            if (type == animType || type.IsSubclassOf(animType)) return (T)_animation;
            throw new GameException(GameErrorCode.InvalidTypeOfAnimation);
        }

        public void Register(GameObjectEvent handler)
        {
            OnUpdate += handler;
        }

        public void UnRegister(GameObjectEvent handler)
        {
            OnUpdate -= handler;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _animation.Draw(spriteBatch, Position, gameTime);

#if DEBUG
            spriteBatch.DrawRectangle(GetBoundry(), Color.Red);
            spriteBatch.DrawCircle(Position, 1f, 360, Color.Yellow);
#endif
        }

        public void SetSpeed(float speed = 1f)
        {
            Speed = speed;
        }

        public void Move(Vector2 vector)
        {
            Position += Vector2.Multiply(vector, Speed);
        }

        public void SetPosition(Vector2 vector)
        {
            Position = vector;
        }

        public Vector2 CurPosition()
        {
            return Position;
        }

        public void Update(GameTime gameTime)
        {
            OnUpdate?.Invoke(this, gameTime);

            if (_action != null)
            {
                _action.Do(this, gameTime);
                if (_action.IsFinished) _action = null;
            }

            _animation.Update(gameTime);
        }

        public void SetAction(IObjectAction action)
        {
            _action = action;
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

        public bool TrySetPosition(Vector2 position)
        {
            foreach (var obj in _collidables.Select(x => x.GetBoundry()))
            {
                if (obj.Contains(position)) return false;
            }
            this.Position = position;
            return true;
        }

        public Rectangle GetBoundry()
        {
            var myRectangle = _animation.GetRectangle();

            myRectangle.X += (int)Position.X;
            myRectangle.Y += (int)Position.Y;

            return myRectangle;
        }

        public ICollection<ICollidable> Collides(Vector2 moveVector)
        {
            List<ICollidable> collidedObjects = new List<ICollidable>();
            if (!_collidables.Any()) return collidedObjects;

            var myBoundry = GetBoundry();

            var multipledMoveVector = Vector2.Multiply(moveVector, Speed);

            myBoundry.X += (int)multipledMoveVector.X;
            myBoundry.Y += (int)multipledMoveVector.Y;

            foreach (var obj in _collidables.Where(x => x != this))
            {
                var hisBoundry = obj.GetBoundry();
                if (hisBoundry.Intersects(myBoundry)) collidedObjects.Add(obj);
            }

            return collidedObjects;
        }

        public void ChangeAnimation(AnimCode code)
        {
            _animation.SetAnim(code);
        }
    }
}