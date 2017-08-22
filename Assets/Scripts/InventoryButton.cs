using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UGUI
{
    public class InventoryButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public GameObject Holder { private get; set; }
        private Button _button;
        private Image _image;
        private Color _fadeColor;
        private Vector3 _startPosition;
        private bool _isDraging;
        private CanvasGroup _canvasGroup;
        private Transform _parentTransform;
        private Transform _canvasTransform;

        private void Start()
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
        }

        public void ToggleActiveForHolder()
        {
            if (_isDraging) return;
            Holder.SetActive(!Holder.activeSelf);
            _image.color = Holder.activeSelf ? Color.white : _fadeColor;
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
            _startPosition = transform.position;
            transform.SetParent(_canvasTransform);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDraging = false;
            _canvasGroup.blocksRaycasts = true;
            transform.position = _startPosition;
            transform.SetParent(_parentTransform);
        }

    }

}

