using UnityEngine;
using UnityEngine.UI;

namespace UGUI
{
    public class CameraPan : MonoBehaviour
    {
        private const float PanSpeed = 5.0f;
        public static bool EnablePan { get; private set; }
        private Toggle _enablePanToggle;
        private Vector3 _mouseOrigin;
        private Camera _camera;

        private void Start()
        {
            _enablePanToggle = GameObject.Find("Pan Toggle").GetComponent<Toggle>();
            _enablePanToggle.onValueChanged.AddListener(TogglePanMode);
            _camera = GetComponent<Camera>();
        }

        private static void TogglePanMode(bool enablePan)
        {
            EnablePan = enablePan;
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mouseOrigin = Input.mousePosition;
            }
            if (!EnablePan) return;
            if (!Input.GetMouseButton(0)) return;
            Vector3 position = Input.mousePosition - _mouseOrigin;
            Vector3 move = position.normalized * PanSpeed * Time.deltaTime;
            transform.Translate(move, Space.Self);
        }
    }
}

