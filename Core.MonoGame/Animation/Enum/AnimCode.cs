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

namespace Core.MonoGame.Animation.Enum
{
    public enum AnimCode
    {
        Static = -1,
        MovLeft,
        MovRight,
        Jump,
        AttackAnim
    }
}