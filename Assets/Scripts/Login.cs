using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UGUI
{
    public class Login : MonoBehaviour
    {
        private Button _button;
        private InputField _inputField;
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(SignIn);
            _inputField = GameObject.Find("Username InputField").GetComponent<InputField>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SignIn();
            }
        }

        private void SignIn()
        {
            if (_inputField.text.Length == 0) return;
            Manager.Instance.Username = _inputField.text;
            SceneManager.LoadScene((int) Manager.SceneNames.Main);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}

