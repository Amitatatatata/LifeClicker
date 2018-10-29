using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ami
{
    public class HeartManager : MonoBehaviour
    {
        [SerializeField] private float upHeight = 50f;

        // Use this for initialization
        void Start()
        {
            RectTransform rect = GetComponent<RectTransform>();
            Vector2 endPos = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y + upHeight);
            rect.MoveTo(endPos, 1.0f, 0.0f, iTween.EaseType.easeInOutSine, Destroy);
        }


        public void Destroy()
        {
            Destroy(gameObject);
        }
    }

}