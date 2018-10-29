using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace Ami
{

    public static class JobDataBase
    {

        //全Jobデータベース
        public static Dictionary<string, Job> jobs_fromBabyToUniv;

        public static Dictionary<string, Job> jobs_HSG;

        public static Dictionary<string, Job> jobs_Univ;

        //staticコンストラクタ staticフィールドが参照されると一度だけ呼ばれる
        static JobDataBase()
        {
            jobs_fromBabyToUniv = LoadJobDataBase("JobDataBase_FromBaby");
            jobs_HSG = LoadJobDataBase("JobDataBase_HSG");
            jobs_Univ = LoadJobDataBase("JobDataBase_Univ");

            /*  job用画像がそろっていないため保留
            //Resources/JobSpritesフォルダ内のすべてのスプライトを読み込む
            Sprite[] sprites = Resources.LoadAll<Sprite>("JobSprites");
            
            //すべてのJobに対してスプライトを登録
            foreach (var sprite in sprites)
            {
                jobs[sprite.name].JobSprite = sprite;
            }
            */

            foreach (var job in jobs_fromBabyToUniv.Values)
            {
                job.JobSprite = Resources.Load<Sprite>("JobSprites/baby");
            }
            foreach (var job in jobs_HSG.Values)
            {
                job.JobSprite = Resources.Load<Sprite>("JobSprites/baby");
            }
            foreach (var job in jobs_Univ.Values)
            {
                job.JobSprite = Resources.Load<Sprite>("JobSprites/baby");
            }
        }


        static Dictionary<string, Job> LoadJobDataBase(string fileName)
        {
            var jobDataBase = new Dictionary<string, Job>();
            var textAsset = Resources.Load(fileName, typeof(TextAsset)) as TextAsset;
            var textReader = new StringReader(textAsset.text);
            var jobName = "";
            while (textReader.Peek() > -1)
            {
                while ((jobName = textReader.ReadLine()) == "") ;
                var iine = int.Parse(Regex.Match(textReader.ReadLine(), @"\d+").Value);

                textReader.ReadLine();
                var needItemNames = new Dictionary<string, NeedItems>();
                var itemList = new List<string>();
                var itemName = "";
                var prevJobName = "";
                while ((prevJobName = textReader.ReadLine()) != "itemend")
                {
                    var isOr = false;
                    if (Regex.IsMatch(prevJobName, @".*=.*"))
                    {
                        prevJobName = Regex.Match(prevJobName, @"\w*").Value;
                        isOr = true;
                    }
                    while ((itemName = textReader.ReadLine()) != "jobend") itemList.Add(itemName);
                    needItemNames.Add(prevJobName, new NeedItems(isOr, itemList));
                }

                textReader.ReadLine();
                var nextJobList = new List<string>();
                var nextJob = "";
                while ((nextJob = textReader.ReadLine()) != "nextjobend") nextJobList.Add(nextJob);
                
                var background = Regex.Match(textReader.ReadLine(), @"\w+$").Value;

                if (!jobDataBase.ContainsKey(jobName)) jobDataBase.Add(jobName, new Job(jobName, iine, needItemNames, nextJobList, background));
            }

            return jobDataBase;
        }

        public static Job GetJob(string jobName)
        {
            Job job = null;

            if (jobs_fromBabyToUniv.ContainsKey(jobName)) job = jobs_fromBabyToUniv[jobName];
            else if (jobs_HSG.ContainsKey(jobName)) job = jobs_HSG[jobName];
            else if (jobs_Univ.ContainsKey(jobName)) job = jobs_Univ[jobName];

            return job;
        }
    }

    

}
