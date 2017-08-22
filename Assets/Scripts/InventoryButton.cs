using UnityEngine;
using UnityEngine.UI;

namespace UGUI
{
    public class InventoryButton : MonoBehaviour
    {
        public GameObject Holder { private get; set; }
        private Button _button;
        private Image _image;
        private Color _fadeColor;

        private void Start()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
            _button.onClick.AddListener(ToggleActiveForHolder);
            _fadeColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void ToggleActiveForHolder()
        {
            Holder.SetActive(!Holder.activeSelf);
            _image.color = Holder.activeSelf ? Color.white : _fadeColor;
        }
    }

}

