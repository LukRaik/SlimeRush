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

namespace Core.MonoGame.Utils
{
    public static class ConditionHelper
    {
        public static bool IsBeetwen(this float curValue, float item1, float item2)
        {
            if (item1 > item2)
            {
                return curValue < item1 && curValue > item2;
            }
            if (item2 > item1)
            {
                return curValue > item1 && curValue < item2;
            }
            return curValue == item1;
        }
    }
}