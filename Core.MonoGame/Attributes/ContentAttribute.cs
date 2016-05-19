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

namespace Core.MonoGame.Attributes
{
    public class ContentAttribute : Attribute
    {
        public Dictionary<string,Type> Contents = new Dictionary<string, Type>();

        public ContentAttribute(Type type, string contentName)
        {
            Contents.Add(contentName,type);
        }
        public ContentAttribute(Dictionary<string,Type> contents)
        {
            Contents = contents;
        }
    }
}