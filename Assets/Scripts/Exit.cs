using UnityEngine;
using UnityEngine.UI;

namespace UGUI
{
    public class Exit : MonoBehaviour
    {
        private Button _button;
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(QuitApp);
        }

        private static void QuitApp()
        {
            Application.Quit();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}

