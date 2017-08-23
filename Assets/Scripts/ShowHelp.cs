using UnityEngine;
using UnityEngine.UI;

namespace UGUI
{
    public class ShowHelp : MonoBehaviour
    {
        private const float RectSpeedX = 1000.0f;
        private const float RectSpeedY = RectSpeedX / 10.0f;
        private const float ColorSpeed = 1.25f;
        private RectTransform _helpRectTransform;
        private Text _helpText;
        private Button _button;
        private GameObject _showHelpGameobject;
        private void Awake()
        {
            _showHelpGameobject = GameObject.Find("Show Help Text Button");
            _helpRectTransform = transform.GetComponent<RectTransform>();
            _helpText = transform.GetChild(0).GetComponent<Text>();
            
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ShowCredit);
        }

        private void ShowCredit()
        {
            HideHelp.IsShown = false;
            _helpRectTransform.sizeDelta = new Vector2(1.0f, 1.0f);
            _helpText.color = new Color(_helpText.color.r, _helpText.color.g, _helpText.color.b, 0.0f);
            _showHelpGameobject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
        private void Update()
        {
            if (HideHelp.IsShown)
            {
                if (_helpRectTransform.sizeDelta.x < 480.0f)
                {
                    _helpRectTransform.sizeDelta += new Vector2(Time.deltaTime * RectSpeedX, 0.0f);
                }
                else if (_helpRectTransform.sizeDelta.y < 160.0f)
                {
                    _helpRectTransform.sizeDelta += new Vector2(0.0f, Time.deltaTime * RectSpeedY);
                }
                else if (_helpText.color.a < 1.0f)
                {
                    _helpText.color += new Color(0.0f, 0.0f, 0.0f, Time.deltaTime / ColorSpeed);
                }
            }
            else
            {
                _helpRectTransform.sizeDelta = new Vector2(1.0f, 1.0f);
                _helpText.color = new Color(_helpText.color.r, _helpText.color.g, _helpText.color.b, 0.0f);
            }
        }
    }
}
