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
        private bool _logPan;

        private void Start()
        {
            _enablePanToggle = GameObject.Find("Pan Toggle").GetComponent<Toggle>();
            _enablePanToggle.onValueChanged.AddListener(TogglePanMode);
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
            if (InventoryButton.DragActive) return;
            Vector3 direction = Input.mousePosition - _mouseOrigin;
            Vector3 move = direction.normalized * PanSpeed * Time.deltaTime;
            transform.Translate(move, Space.Self);
            _logPan = direction.sqrMagnitude > 0.0f;
            if (!_logPan) return;
            Manager.Instance.WriteToFile("     User is panning");
            Manager.Instance.WriteToFile("     X: " +
                                         transform.position.x.ToString("F3") + " Y: " +
                                         transform.position.y.ToString("F3") + " Z: " +
                                         transform.position.z.ToString("F3"));
        }
    }
}

