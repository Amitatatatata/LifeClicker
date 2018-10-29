using UnityEngine;

namespace Noripy
{
    public interface IInfiniteScrollSetUp
    {

        void OnPostSetUpItems();
        void OnUpdateItem(int itemCount, GameObject obj);
    }
}

