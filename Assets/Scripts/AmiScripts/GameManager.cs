using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ami
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private GameObject heartPrefab;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Text iine;
        [SerializeField] private CharaManager chara;
        [SerializeField] private LifeEventCanvasManager lifeEventCanvasManager;
        [SerializeField] private Text moneyLabel;
        [SerializeField] private Image background;


        AudioSource audioSource;


        public int nowIine { get; private set; }
        public bool isNormal = true;        //仕事ビューやShopビューを出すときはfalse


        // Use this for initialization
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            nowIine = PlayerPrefs.GetInt("いいね", 0);
            iine.text = nowIine.ToString();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnApplicationQuit()
        {
            PlayerPrefs.SetInt("いいね", nowIine);
            PlayerPrefs.SetInt("お金", chara.Money);
            PlayerPrefs.SetString("職業", chara.NowJob.Name);
            PlayerPrefsUtils.SaveList("アイテム", chara.itemNames);
            PlayerPrefs.Save();
        }



        //ハートを生成していいねを増やす
        public void GetHeart()
        {
            GameObject heart = Instantiate(heartPrefab) as GameObject;
            heart.transform.SetParent(canvas.transform.Find("Hearts").transform);

            RectTransform rect = heart.GetComponent<RectTransform>();
            RectTransform canRect = canvas.GetComponent<RectTransform>();
            rect.anchoredPosition3D = new Vector3(Random.Range(0, canRect.rect.width - 100), Random.Range(0, canRect.rect.height - 50), 0);
            heart.transform.localScale = new Vector3(1, 1, 1);
            nowIine++;
            iine.text = nowIine.ToString();
        }

        public void ShowEventDialog(string message)
        {
            isNormal = false;
            lifeEventCanvasManager.SetText(message);
            lifeEventCanvasManager.gameObject.SetActive(true);
        }

        public void UpdateMoneyLabel(int money)
        {
            moneyLabel.text = $"{money.ToString()}円";
        }

        public void SetBackground(Sprite sprite)
        {
            background.sprite = sprite;
        }
    }
}