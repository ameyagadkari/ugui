using UnityEngine;
using UnityEngine.EventSystems;

namespace UGUI
{
    public class GarbageBin : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            eventData.pointerDrag.GetComponent<InventoryButton>().Remove();
        }
    }
}

