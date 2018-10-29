using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

namespace Noripy
{
    //ref:https://github.com/tsubaki/Unity_UI_Samples/blob/master/Assets/InfiniteScroll/InfiniteScroll.cs
    public class InfinitieScroll : UIBehaviour
    {

        [SerializeField] private RectTransform itemPrototype;

        [SerializeField, Range(0, 30)] int instantateItemCount = 4;

        public enum Direction
        {
            Vertical,
            Horizontal,
        }

        [SerializeField] private Direction direction;

        public OnItemPositionChange onUpdateItem = new OnItemPositionChange();

        [System.NonSerialized]
        public LinkedList<RectTransform> itemList = new LinkedList<RectTransform>();

        protected float diffPerFramePosition = 0;

        protected int currentItemNo = 0;

        private RectTransform _rectTransform;

        protected RectTransform rectTransform
        {
            get
            {
                if (_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
                return _rectTransform;
            }
        }

        private float anchoredPosition
        {
            get
            {
                return direction == Direction.Vertical ? -rectTransform.anchoredPosition.y : rectTransform.anchoredPosition.x;
            }
        }

        private float _itemScale = -1;
        public float ItemScale
        {
            get
            {
                if (itemPrototype != null && _itemScale == -1)
                {
                    _itemScale = direction == Direction.Vertical ? itemPrototype.sizeDelta.y : itemPrototype.sizeDelta.x;
                }
                return _itemScale;
            }
        }
        
        public List<int> price = new List<int>();
        public List<string> keyList = new List<string>();

        // Use this for initialization
        protected override void Start()
        {
            var controllers = GetComponents<MonoBehaviour>()
                .Where(item => item is IInfiniteScrollSetUp)
                .Select(item => item as IInfiniteScrollSetUp)
                .ToList();
            controllers.ForEach(v => Debug.Log(v));
            //is : 右にある型であるか否か
            //as : 右の型へのキャスト

            var scrollRect = GetComponentInParent<ScrollRect>();
            scrollRect.horizontal = direction == Direction.Horizontal;
            scrollRect.vertical = direction == Direction.Vertical;
            scrollRect.content = rectTransform;

            itemPrototype.gameObject.SetActive(false);


            for (int i = 0; i < instantateItemCount; i++)
            {
                var item = GameObject.Instantiate(itemPrototype) as RectTransform;
                item.SetParent(transform, false);
                item.name = i.ToString();

                item.anchoredPosition = direction ==
                    Direction.Vertical ? new Vector2(0, -ItemScale * i) : new Vector2(ItemScale * i, 0);

                itemList.AddLast(item);

                item.gameObject.SetActive(true);

                //Debug.Log(ItemDataBase.items.ToList());
                //PrepareStringID();
 
                RandomPut(item);
                //PutRarity(item, instantateItemCount);

                Debug.Log(i);

                /*
                foreach (var controller in controllers)
                {
                    //Debug.Log("pass");
                    controller.OnUpdateItem(i, item.gameObject);
                }*/
            }

            foreach (var controller in controllers)
            {
                controller.OnPostSetUpItems();
            }
        }

        void PrepareStringID()
        {
            int i = 0;
            foreach (KeyValuePair<string, ShopItemList> kvp in ItemDataBase.shopItemLists)
            {
                string id = kvp.Key;
                keyList.Add(id);
                price.Add(5 + 50 * i);
                i++;
            }
           
        }

        void RandomPut(RectTransform item)//初期生成を完全ランダム配置
        {
            int normalTotal = ItemDataBase.shopItemLists["赤ちゃん"].NormalItems.Count;
            int rareTotal = ItemDataBase.shopItemLists["赤ちゃん"].RareItems.Count;

            int key = Random.Range(0, normalTotal);//ランダム抽選


            item.transform.Find("ItemText").GetComponent<Text>().text
                = ItemDataBase.shopItemLists["赤ちゃん"].NormalItems[key].Name;//ショップアイテムをランダムに配置
            item.transform.Find("MoneyText").GetComponent<Text>().text
                = ItemDataBase.shopItemLists["赤ちゃん"].NormalItems[key].Price.ToString();
            item.transform.Find("ItemImage").GetComponent<Image>().sprite
                = ItemDataBase.shopItemLists["赤ちゃん"].NormalItems[key].Sprite;
            //ItemDataBase.items["apple"]

        }

        void PutRarity(RectTransform item, int instantateItemCount)//金額に応じて初期配置を調整する
        {
            if(itemList.Count < instantateItemCount)
            {
                int ratio = instantateItemCount / itemList.Count;
                int rarity = instantateItemCount % itemList.Count;


            }
            else if (itemList.Count == instantateItemCount)
            {

            }
            else//itemList.Count > instantateItemCount
            {
                int ratio = itemList.Count / instantateItemCount;
                int rarity = itemList.Count % instantateItemCount;

            }

        }
        // Update is called once per frame
        void Update()
        {
            if (itemList.First == null) return;

            while (anchoredPosition - diffPerFramePosition < -ItemScale * 2)
            {
                diffPerFramePosition -= ItemScale;

                var item = itemList.First.Value;
                itemList.RemoveFirst();
                itemList.AddLast(item);

                var pos = ItemScale * instantateItemCount + ItemScale * currentItemNo;
                item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);

                onUpdateItem.Invoke(currentItemNo + instantateItemCount, item.gameObject);

                currentItemNo++;
            }

            while (anchoredPosition - diffPerFramePosition > 0)
            {
                diffPerFramePosition += ItemScale;

                var item = itemList.Last.Value;
                itemList.RemoveLast();
                itemList.AddFirst(item);

                currentItemNo--;

                var pos = ItemScale * currentItemNo;
                item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, pos) : new Vector2(pos, 0);
                onUpdateItem.Invoke(currentItemNo, item.gameObject);
            }
        }

        [System.Serializable]
        public class OnItemPositionChange : UnityEngine.Events.UnityEvent<int, GameObject> { }
    }

}
