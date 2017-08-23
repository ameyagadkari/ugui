using System;
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
        private Transform _inventory3DObjectParentTransform;
        private Button _button;

        private void Start()
        {
            _inventoryButtonPrefab = Resources.Load<GameObject>("Prefabs/UI/Inventory Button");
            _holderPrefab = Resources.Load<GameObject>(ItemPaths[(int)ItemName]);
            _holderPrefabSprite = Resources.Load<Sprite>(ItemSpritePaths[(int)ItemName]);
            _inventoryButtonParentTransform = GameObject.FindGameObjectWithTag("InventoryButtonParent").transform;
            _inventory3DObjectParentTransform = GameObject.FindGameObjectWithTag("Inventory3DObjectParent").transform;
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
            inventoryButton.GetComponent<Image>().sprite = _holderPrefabSprite;        
            GameObject inventoryObject = Instantiate(_holderPrefab, Vector3.zero, Quaternion.identity,
                _inventory3DObjectParentTransform);
            inventoryButton.GetComponent<InventoryButton>().Holder = inventoryObject;
            ContentManager.Instance.AddGameobjectToList(inventoryButton);
            int number;
            switch (ItemName)
            {
                case ItemToCreate.Cube:
                    number = MainManager.Instance.CubeNumber;
                    inventoryObject.name = "Cube " + number;
                    inventoryButton.name = "Cube " + number + " button";
                    break;
                case ItemToCreate.Sphere:
                    number = MainManager.Instance.SphereNumber;
                    inventoryObject.name = "Sphere " + number;
                    inventoryButton.name = "Sphere " + number + " button";
                    break;
                case ItemToCreate.Cylinder:
                    number = MainManager.Instance.CylinderNumber;
                    inventoryObject.name = "Cylinder " + number;
                    inventoryButton.name = "Cylinder " + number + " button";
                    break;
                case ItemToCreate.Capsule:
                    number = MainManager.Instance.CapsuleNumber;
                    inventoryObject.name = "Capsule " + number;
                    inventoryButton.name = "Capsule " + number + " button";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
