using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.VR.WSA.Persistence;

namespace UGUI
{
    public class InventoryButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public GameObject Holder { get; set; }
        public static bool DragActive { get; private set; }
        private Button _button;
        private Image _image;
        private Color _fadeColor;
        private bool _isDraging;
        private CanvasGroup _canvasGroup;
        private Transform _parentTransform;
        private Transform _canvasTransform;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _button.onClick.AddListener(ToggleActiveForHolder);
            _fadeColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            _isDraging = false;
            _parentTransform = transform.parent;
            _canvasTransform = GameObject.Find("Main Canvas").transform;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
            if (ContentManager.Instance.CurrentlySelected == this && ContentManager.Instance.InventoryButtons.Count != 0)
            {
                ContentManager.Instance.CurrentlySelected = ContentManager.Instance.InventoryButtons[0]
                    .GetComponent<InventoryButton>();
            }
        }

        public void ToggleActiveForHolder()
        {
            if (_isDraging) return;
            ContentManager.Instance.CurrentlySelected.Holder.SetActive(false);
            ContentManager.Instance.CurrentlySelected._image.color = _fadeColor;
            ContentManager.Instance.CurrentlySelected = this;
            Holder.SetActive(true);
            _image.color = Color.white;
            Manager.Instance.WriteToFile("}");
            Manager.Instance.WriteToFile(Holder.name + ":");
            Manager.Instance.WriteToFile("{");
        }

        public void Remove()
        {
            ContentManager.Instance.RemoveGameobjectFromList(gameObject);
            Destroy(Holder);
            Destroy(gameObject);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDraging = true;
            _canvasGroup.blocksRaycasts = false;
            transform.SetParent(_canvasTransform);
            DragActive = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDraging = false;
            _canvasGroup.blocksRaycasts = true;
            transform.SetParent(_parentTransform);
            DragActive = false;
        }

    }

}

