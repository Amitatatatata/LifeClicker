using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Ami
{

    public class CharaManager : MonoBehaviour
    {

        [SerializeField] private GameManager gameManager;


        private SpriteRenderer mySprite { get; set; }

        private int money;
        public int Money
        {
            get
            {
                return money;
            }
            set
            {
                money = value;
                gameManager.UpdateMoneyLabel(money);
            }
        }
        public Job NowJob { get; set; }
        public List<string> itemNames; //持っているアイテム

        // Use this for initialization
        void Start()
        {
            Money = PlayerPrefs.GetInt("お金", 0);
            itemNames = PlayerPrefsUtils.LoadList<string>("アイテム");
            NowJob = JobDataBase.GetJob(PlayerPrefs.GetString("職業", "赤ちゃん"));
            mySprite = GetComponent<SpriteRenderer>();

            SetJobSprite();
        }

        // Update is called once per frame
        void Update()
        {

        }

        //クリック時にすること
        //1.キャラをクリックしたときにハートをゲットする
        //2.派生可能なJobがあれば成長する
        //3.仕事の抽選を行う
        public void OnClick()
        {
            gameManager.GetHeart();
            Job nextJob = null;
            if ((nextJob = GrowableJob()) != null) GrowUp(nextJob);

            EventChallenge();
        }

        public void EventChallenge()
        {
            int rondomNum = Random.Range(0, 100);

            int nowLower = 0;
            foreach (var e in LifeEventDataBase.lifeEvents[NowJob.Name])
            {
                if (nowLower <= rondomNum && rondomNum < nowLower + e.Chance)
                {
                    //ゲームマネージャーに仕事をしたことを伝える
                    gameManager.ShowEventDialog(e.Occur(this));
                    return;
                }
                nowLower += e.Chance;
            }

        }

        //キャラのSpriteを更新する
        private void SetJobSprite()
        {
            mySprite.sprite = NowJob.JobSprite;
        }

        //現在のJobから成長可能なJobがあるか調べる
        private Job GrowableJob()
        {
            var list = NowJob.NextJobs.Where(nextJob => JobDataBase.GetJob(nextJob).IsGrowable(NowJob.Name, gameManager.nowIine, itemNames));
            if (list.Count() == 0) return null;
            else return JobDataBase.GetJob(list.ElementAt(Random.Range(0, list.Count())));
        }

        //Jobを派生Jobに更新する
        private void GrowUp(Job nextJob)
        {
            NowJob = nextJob;
            SetJobSprite();
            gameManager.SetBackground(BackgroundDataBase.backgrounds[NowJob.BackgroundName]);
        }
    }

}