using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public static class ItemDataBase
{

    //全Itemデータベース
    public static Dictionary<string, ShopItemList> shopItemLists;

    static ItemDataBase()
    {
        //Resources/ItemSpritesフォルダから全Spriteをロード
        Sprite[] sprites = Resources.LoadAll<Sprite>("ItemSprites");

        shopItemLists = LoadItemDataBase("ItemDataBase", sprites);
    }

    static Dictionary<string, ShopItemList> LoadItemDataBase(string fileName, Sprite[] sprites)
    {
        var itemDataBase = new Dictionary<string, ShopItemList>();
        var textAsset = Resources.Load(fileName, typeof(TextAsset)) as TextAsset;
        var sr = new StringReader(textAsset.text);

        var jobName = "";
        while (sr.Peek() > -1)
        {
            while ((jobName = sr.ReadLine()) == "") ;

            sr.ReadLine();
            var normalItems = new List<Item>();
            var itemName = "";
            var price = 0;
            while (true)
            {
                itemName = sr.ReadLine();
                if (itemName == "normalend") break;
                price = int.Parse(sr.ReadLine());
                var item = new Item(itemName, price);
                item.Sprite = sprites.First(s => s.name == itemName);
                normalItems.Add(item);
            }

            sr.ReadLine();
            var rareItems = new List<Item>();
            while (true)
            {
                itemName = sr.ReadLine();
                if (itemName == "rareend") break;
                price = int.Parse(sr.ReadLine());
                var item = new Item(itemName, price);
                item.Sprite = sprites.First(s => s.name == itemName);
                rareItems.Add(item);
            }

            if (!itemDataBase.ContainsKey(jobName)) itemDataBase.Add(jobName, new ShopItemList { NormalItems = normalItems, RareItems = rareItems });
        }

        return itemDataBase;
    }

}

public class ShopItemList
{
    public List<Item> NormalItems { get; set; }

    public List<Item> RareItems { get; set; }
}

