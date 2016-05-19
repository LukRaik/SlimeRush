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

namespace Core.MonoGame.ContentManagement
{
    public interface IContentManager
    {
        void LoadContent<T>() where T : class;
        T GetContent<T>(string name) where T : class;
        void ClearContent();
    }
}