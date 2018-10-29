using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Ami
{

    public class NeedItems
    {
        public bool IsOr { get; set; }

        public List<string> ItemNames { get; set; }

        public NeedItems(bool isOr, List<string> itemNames)
        {
            IsOr = isOr;
            ItemNames = itemNames;
        }
    }

    public class Job
    {
        public string Name { get; private set; }

        public int NeedIine { get; private set; }

        //成長に必要なItemを文字列で持つ
        public Dictionary<string, NeedItems> NeedItemNames { get; set; }

        //派生Jobを文字列のListで持つ
        public List<string> NextJobs { get; set; }

        public Sprite JobSprite { get; set; }

        public string BackgroundName { get; private set; }

        public Job(string name, int needIine, Dictionary<string, NeedItems> needItemNames, List<string> nextJobs, string backgroundName)
        {
            Name = name;
            NeedIine = needIine;
            NeedItemNames = needItemNames;
            NextJobs = nextJobs;
            JobSprite = null;
            BackgroundName = backgroundName;
        }

        //いいねと所持アイテムリストを受け取り、このJobに成長できるか返す
        public bool IsGrowable(string jobName, int iine, List<string> itemNames)
        {
            if(NeedItemNames.Keys.Count == 0)
            {
                return NeedIine <= iine;
            }
            if (NeedItemNames.ContainsKey(jobName))
            {
                if (IsGrowable(NeedItemNames[jobName], iine, itemNames)) return true;
            }
            if (NeedItemNames.ContainsKey("any"))
            {
                if (IsGrowable(NeedItemNames["any"], iine, itemNames)) return true;
            }
            

            return false;
        }

        private bool IsGrowable(NeedItems needItems, int nowIine, List<string> nowItemNames)
        {
            if(needItems.IsOr)
            {
                return NeedIine <= nowIine && needItems.ItemNames.Any(item => nowItemNames.Contains(item));
            }
            else
            {
                return NeedIine <= nowIine && needItems.ItemNames.All(item => nowItemNames.Contains(item));
            }
        }
    }

}
