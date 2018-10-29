using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ami
{

    public class LifeEvent
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Wage { get; set; }       //獲得するお金 マイナスもある

        public string[] GettingItemNames { get; set; }

        public int Chance { get; set; }


        public string Occur(CharaManager chara)
        {
            chara.Money += Wage;
            if (chara.Money < 0) chara.Money = 0;
            return Description;
        }
    }

}
