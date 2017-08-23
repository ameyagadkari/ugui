using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UGUI
{
    public class Movement : MonoBehaviour, IDragHandler
    {
        private const float RotationSpeed = 200.0f;
        private const float ScaleSpeed = 100.0f;
        private const float MinScale = 0.1f;
        private const float MaxScale = 5.0f;
        private const float MinViewport = 0.0f;
        private const float MaxViewport = 1.0f;
        private Text _name;
        private Text _position;
        private Text _rotation;
        private Text _scale;
        private bool _logPosition;
        private bool _logRotation;
        private bool _logScale;

        private void Start()
        {
            Transform informationPanelTransform = GameObject.FindGameObjectWithTag("InformationPanel").transform;
            _name = informationPanelTransform.GetChild(0).GetComponent<Text>();
            _position = informationPanelTransform.GetChild(1).GetComponent<Text>();
            _rotation = informationPanelTransform.GetChild(2).GetComponent<Text>();
            _scale = informationPanelTransform.GetChild(3).GetComponent<Text>();
        }

        private void Update()
        {
            _name.text = gameObject.name;
            RotateObject();
            ScaleObject();
            _position.text = "Position\nX: " +
                          transform.position.x.ToString("F3") + " Y: " +
                          transform.position.y.ToString("F3") + " Z: " +
                          transform.position.z.ToString("F3");
        }

        private void RotateObject()
        {
            _rotation.text = "Rotation\nX: " +
                             (transform.eulerAngles.x > 180.0f ? (-transform.eulerAngles.x + 180.0f).ToString("F3") : transform.eulerAngles.x.ToString("F3"))
                             + " Y: " +
                             (transform.eulerAngles.y > 180.0f ? (-transform.eulerAngles.y + 180.0f).ToString("F3") : transform.eulerAngles.y.ToString("F3"))
                             + " Z: " +
                             (transform.eulerAngles.z > 180.0f ? (-transform.eulerAngles.z + 180.0f).ToString("F3") : transform.eulerAngles.z.ToString("F3"));
            if (!Input.GetMouseButton(1)) return;
            float mouseXAxisValue = Input.GetAxis("Mouse X");
            float mouseYAxisValue = Input.GetAxis("Mouse Y");
            transform.Rotate(Vector3.forward, -Mathf.Deg2Rad * mouseXAxisValue * RotationSpeed);
            transform.Rotate(Vector3.right, Mathf.Deg2Rad * mouseYAxisValue * RotationSpeed);
            _logRotation = mouseXAxisValue > 0.0f || mouseXAxisValue < 0.0f || mouseYAxisValue > 0.0f ||
                           mouseYAxisValue < 0.0f;
            if (!_logRotation) return;
            Manager.Instance.WriteToFile("     User is rotating");
            Manager.Instance.WriteToFile("     X: " +
                                         (transform.eulerAngles.x > 180.0f
                                             ? (-transform.eulerAngles.x + 180.0f).ToString("F3")
                                             : transform.eulerAngles.x.ToString("F3"))
                                         + " Y: " +
                                         (transform.eulerAngles.y > 180.0f
                                             ? (-transform.eulerAngles.y + 180.0f).ToString("F3")
                                             : transform.eulerAngles.y.ToString("F3"))
                                         + " Z: " +
                                         (transform.eulerAngles.z > 180.0f
                                             ? (-transform.eulerAngles.z + 180.0f).ToString("F3")
                                             : transform.eulerAngles.z.ToString("F3")));
        }

        private void ScaleObject()
        {
            float mouseScrollWheelAxisValue = Input.GetAxis("Mouse ScrollWheel");
            float scale = transform.localScale.x;
            scale += mouseScrollWheelAxisValue * Time.deltaTime * ScaleSpeed;
            if (scale > MaxScale) scale = MaxScale;
            else if (scale < MinScale) scale = MinScale;
            transform.localScale = new Vector3(scale, scale, scale);
            _scale.text = "Scale\nX: " +
                          transform.localScale.x.ToString("F3") + " Y: " +
                          transform.localScale.y.ToString("F3") + " Z: " +
                          transform.localScale.z.ToString("F3");
            _logScale = mouseScrollWheelAxisValue > 0.0f || mouseScrollWheelAxisValue < 0.0f;
            if (!_logScale) return;
            Manager.Instance.WriteToFile("     User is scaling");
            Manager.Instance.WriteToFile("     X: " +
                                         transform.localScale.x.ToString("F3") + " Y: " +
                                         transform.localScale.y.ToString("F3") + " Z: " +
                                         transform.localScale.z.ToString("F3"));
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (!Input.GetMouseButton(0)) return;
            if (CameraPan.EnablePan) return;
            Plane plane = new Plane(Vector3.forward, transform.position);
            Ray ray = eventData.pressEventCamera.ScreenPointToRay(eventData.position);
            float distance;
            if (!plane.Raycast(ray, out distance)) return;
            Vector3 newPosition = ray.origin + ray.direction * distance;
            Vector3 newPositionViewport = eventData.pressEventCamera.WorldToViewportPoint(newPosition);
            Vector3 originalPosition = transform.position;
            if (newPositionViewport.x > MaxViewport || newPositionViewport.x < MinViewport)
            {
                newPosition.x = originalPosition.x;
                _logPosition = false;
            }
            else
            {
                _logPosition = true;
            }
            if (newPositionViewport.y > MaxViewport || newPositionViewport.y < MinViewport)
            {
                newPosition.y = originalPosition.y;
                _logPosition = false;
            }
            else
            {
                _logPosition = true;
            }
            transform.position = newPosition;
            if (!_logPosition) return;
            Manager.Instance.WriteToFile("     User is moving");
            Manager.Instance.WriteToFile("     X: " +
                                         transform.position.x.ToString("F3") + " Y: " +
                                         transform.position.y.ToString("F3") + " Z: " +
                                         transform.position.z.ToString("F3"));
        }
    }
}