using System.Collections.Generic;
using UnityEngine;

namespace UGUI
{
    public class ContentManager : MonoBehaviour
    {
        public static ContentManager Instance
        {
            get
            {
                return _instance ?? (_instance = FindObjectOfType<ContentManager>());
            }
        }
        private static ContentManager _instance;
        public List<GameObject> InventoryButtons { get; private set; }

        public InventoryButton CurrentlySelected { get; set; }

        private void Start()
        {
            InventoryButtons = new List<GameObject>();
        }

        public void AddGameobjectToList(GameObject button)
        {
            InventoryButtons.Add(button);
            if (CurrentlySelected == null)
            {
                CurrentlySelected = button.GetComponent<InventoryButton>();
            }
            else
            {
                button.GetComponent<InventoryButton>().ToggleActiveForHolder();
            }
        }

        public void RemoveGameobjectFromList(GameObject button)
        {
            InventoryButtons.Remove(button);
        }

    }

}

