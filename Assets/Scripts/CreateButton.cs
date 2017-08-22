using UnityEngine;
using UnityEngine.UI;

namespace UGUI
{
    public class CreateButton : MonoBehaviour
    {
        public enum ItemToCreate { Cube, Sphere, Cylinder, Capsule }

        public ItemToCreate ItemName;
        private static readonly string[] ItemPaths =
        {
            "Prefabs/3D/CubePrefab",
            "Prefabs/3D/SpherePrefab",
            "Prefabs/3D/CylinderPrefab",
            "Prefabs/3D/CapsulePrefab"
        };

        private static readonly string[] ItemSpritePaths =
        {
            "Sprites/CubeIcon",
            "Sprites/SphereIcon",
            "Sprites/CylinderIcon",
            "Sprites/CapsuleIcon"
        };

        private GameObject _inventoryButtonPrefab;
        private GameObject _holderPrefab;
        private Sprite _holderPrefabSprite;
        private Transform _inventoryButtonParentTransform;
        private Button _button;

        private void Start()
        {
            _inventoryButtonPrefab = Resources.Load<GameObject>("Prefabs/UI/Inventory Button");
            _holderPrefab = Resources.Load<GameObject>(ItemPaths[(int)ItemName]);
            _holderPrefabSprite = Resources.Load<Sprite>(ItemSpritePaths[(int)ItemName]);
            _inventoryButtonParentTransform = GameObject.FindGameObjectWithTag("InventoryButtonParent").transform;
            _button = GetComponent<Button>();
            _button.onClick.AddListener(CreateObjectAndButton);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void CreateObjectAndButton()
        {
            GameObject inventoryButton = Instantiate(_inventoryButtonPrefab, _inventoryButtonParentTransform);
            inventoryButton.GetComponent<InventoryButton>().Holder =
                Instantiate(_holderPrefab, Vector3.zero, Quaternion.identity);
            inventoryButton.GetComponent<Image>().sprite = _holderPrefabSprite;
            ContentManager.Instance.AddGameobjectToList(inventoryButton);
        }
    }
}
