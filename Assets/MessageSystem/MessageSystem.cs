using UnityEngine.UIElements;
using UnityEngine;

namespace ToolkitMessageSystem
{

    public enum MessagePosition : short
    {
        TopLeft = 1,
        TopRight = 2,
        BottomLeft = 3,
        BottomRight = 4
    }

    public class MessageSystem : MonoBehaviour
    {
        [SerializeField] private MessagePosition _position;
        [SerializeField] private VisualTreeAsset _messageTemplate;
        [SerializeField] private float _dissapearTime = 1.5f;
        [SerializeField] private Font _font;
        [SerializeField] [Range(10, 45)] private int _fontSize;

        private UIDocument _doc;
        private VisualElement _msgBoxElement;

        private void Awake()
        {
            _doc = GetComponent<UIDocument>();
        }
        private void OnEnable()
        {
            MessageHub.OnMessage += HandleMessage;

            var root = _doc.rootVisualElement;
            _msgBoxElement = root.Q<VisualElement>("message-box");
            switch (_position)
            {
                case MessagePosition.TopLeft:
                    _msgBoxElement.style.left = 20;
                    _msgBoxElement.style.top = 20;
                    break;
                case MessagePosition.TopRight:
                    _msgBoxElement.style.right = 20;
                    _msgBoxElement.style.top = 20;

                    break;
                case MessagePosition.BottomLeft:
                    _msgBoxElement.style.left = 20;
                    _msgBoxElement.style.bottom = 20;
                    break;
                case MessagePosition.BottomRight:
                    _msgBoxElement.style.right = 20;
                    _msgBoxElement.style.bottom = 20;
                    break;
            }

        }

        private void OnDisable()
        {
            MessageHub.OnMessage -= HandleMessage;
        }

        private void HandleMessage(string msg, MessageColor color)
        {
            //메세지 출력 부분만 있으면 된다. 이말씀!
            TemplateContainer template = _messageTemplate.Instantiate();
            string offClass = (ushort)_position <= 2 ? "left-off" : "right-off";
            Message message = new Message(template, msg, _dissapearTime, color, _font, _fontSize, offClass);
            _msgBoxElement.Add(template);
        }

    }
}