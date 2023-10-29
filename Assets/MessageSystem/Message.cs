using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace ToolkitMessageSystem
{
    public class Message : MonoBehaviour
    {
        private VisualElement _root;
        private Label _messageLabel;
        private string _offClass;
        public Message(VisualElement root, string msg, float timer, MessageColor color, Font font, int size, string offClass)
        {
            _root = root;
            var body = root.Q<VisualElement>("message");
            body.AddToClassList(color.ToString().ToLower());
            _messageLabel = root.Q<Label>("message-label");

            _messageLabel.text = msg;
            _messageLabel.style.fontSize = size;
            _messageLabel.style.unityFont = font;

            _offClass = offClass;

            //사라지는건 좀 나중에
            Dissapear(timer);
        }
        private async void Dissapear(float time)
        {
            //얘는 ms 단위의 int값을 받는데
            await Task.Delay(Mathf.FloorToInt(time * 1000));
            _root.AddToClassList(_offClass);
            await Task.Delay(1000); //1초면 충분히 사라질꺼니까
            _root.RemoveFromHierarchy();
        }
    }
}
