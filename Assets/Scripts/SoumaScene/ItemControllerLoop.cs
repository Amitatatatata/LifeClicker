using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Noripy
{
    [RequireComponent(typeof(InfinitieScroll))]
    public class ItemControllerLoop : UIBehaviour, IInfiniteScrollSetUp
    {

        private bool isSetuped = false;

        public void OnPostSetUpItems()
        {
            GetComponentInParent<ScrollRect>().movementType = ScrollRect.MovementType.Unrestricted;
            isSetuped = true;
        }

        public void OnUpdateItem(int itemCount, GameObject obj)
        {
            if (isSetuped) return;

            var item = obj.GetComponentInChildren<ItemUnit>();
            item.UpdateItem(itemCount);
        }

    }

}
