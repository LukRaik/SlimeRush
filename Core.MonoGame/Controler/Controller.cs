using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using IUpdateable = Core.MonoGame.Interfaces.IUpdateable;

namespace Core.MonoGame.Controler
{
    public class Controller : IController
    {
        private TouchCollection _touchCollection = new TouchCollection();

        private TouchLocation _lastLocation;

        public Controller()
        {

        }


    
    }
}