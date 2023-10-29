using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolkitMessageSystem
{
    public enum MessageColor
    {
        Black,
        Red,
        Blue
    }

    public delegate void DisplayMessage(string msg, MessageColor color = MessageColor.Black);
    public static class MessageHub 
    {

        public static DisplayMessage OnMessage;

    }
}
