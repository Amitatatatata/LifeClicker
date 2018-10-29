using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Noripy
{
    [RequireComponent(typeof(InfinitieScroll))]
    public class ItemControllerInfinite : UIBehaviour, IInfiniteScrollSetUp
    {
        public void OnPostSetUpItems()
        {
            GetComponent<InfinitieScroll>().onUpdateItem.AddListener(OnUpdateItem);
            GetComponentInParent<ScrollRect>().movementType = ScrollRect.MovementType.Unrestricted;
        }

        public void OnUpdateItem(int itemCount, GameObject obj)
        {
            var item = obj.GetComponentInChildren<ItemUnit>();
            item.UpdateItem(itemCount);
        }
    }

}
