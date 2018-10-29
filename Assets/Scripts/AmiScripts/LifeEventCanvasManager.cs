using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ami
{

    public class LifeEventCanvasManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Text text;

        public void OnClick()
        {
            gameObject.SetActive(false);
            gameManager.isNormal = true;
            text.text = "";
        }

        public void SetText(string text)
        {
            this.text.text = text;
        }
    }

}
