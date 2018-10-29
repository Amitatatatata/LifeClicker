using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Noripy
{
    public class ItemUnit : UIBehaviour
    {

        [SerializeField] Text uiText;
        [SerializeField] Image uiBackGround;
        [SerializeField] Image uiIcon;

        private readonly Color[] colors = new Color[]
        {
        new Color(1,1,1,1),
        new Color(0.9f,0.9f,1,1),
        };
        public void UpdateItem(int count)
        {
            uiText.text = (count + 1).ToString("00");
            uiBackGround.color = colors[Mathf.Abs(count) % colors.Length];
            uiIcon.sprite = Resources.Load<Sprite>("ShopItems");
        }
    }

}
