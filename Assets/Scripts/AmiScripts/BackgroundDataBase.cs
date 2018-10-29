using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Ami
{

    public static class BackgroundDataBase
    {
        public static Dictionary<string, Sprite> backgrounds;

        static BackgroundDataBase()
        {
            backgrounds = new Dictionary<string, Sprite>();
            var sprites = Resources.LoadAll<Sprite>("Backgrounds");
            sprites.ToList()
                .ForEach(s => backgrounds.Add(s.name, s));
        }
    }

}