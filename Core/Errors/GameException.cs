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

namespace Core.Errors
{
    public class GameException:Exception
    {
        public GameException(GameErrorCode code) : this(code, null, null)
        {
        }

        public GameException(GameErrorCode code, string message) : this(code, message, null)
        {
        }

        public GameException(GameErrorCode code, string message, Exception inner) : base(message, inner)
        {
            GameErrorCode = code;
        }

        public GameErrorCode GameErrorCode { get; private set; }
    }
}