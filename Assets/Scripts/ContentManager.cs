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
        private List<GameObject> _inventoryButtons;


        private void Start()
        {
            _inventoryButtons = new List<GameObject>();
        }

        public void AddGameobjectToList(GameObject button)
        {
            _inventoryButtons.Add(button);
        }
    }

}

