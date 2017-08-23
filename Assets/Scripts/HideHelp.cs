using UnityEngine;
using UnityEngine.UI;

namespace UGUI
{
    public class HideHelp : MonoBehaviour
    {
        public static bool IsShown;
        private Button _button;
        private GameObject _hideHelpGameobject;
        private void Start()
        {
            _hideHelpGameobject = GameObject.Find("Hide Help Text Button");
            _hideHelpGameobject.SetActive(false);
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ShowCredit);
            IsShown = false;
        }

        private void ShowCredit()
        {
            IsShown = true;
            _hideHelpGameobject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}

