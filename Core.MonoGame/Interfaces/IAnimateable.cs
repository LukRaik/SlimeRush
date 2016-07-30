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
using Core.MonoGame.Animation.Enum;

namespace Core.MonoGame.Interfaces
{
    public interface IAnimateable
    {
        void ChangeAnimation(AnimCode code);

        AnimCode CurrentAnimation { get; }
    }
}