using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Ami
{

    public static class LifeEventDataBase
    {

        public static Dictionary<string, LifeEvent[]> lifeEvents;

        static LifeEventDataBase()
        {
            lifeEvents = LoadLifeEventFile("LifeEventDataBase");
        }

        static Dictionary<string, LifeEvent[]> LoadLifeEventFile(string fileName)
        {
            var textAsset = Resources.Load(fileName, typeof(TextAsset)) as TextAsset;
            if (textAsset == null) return null;
            var textReader = new StringReader(textAsset.text);

            var lifeEventDataBase = new Dictionary<string, LifeEvent[]>();
            var jobName = "";


            while (textReader.Peek() > -1)
            {
                while ((jobName = textReader.ReadLine()) == "") ;

                var lifeEvents = new List<LifeEvent>();

                var eventName = "";
                while (true)
                {
                    if ((eventName = textReader.ReadLine()) == "end") break;
                    eventName = Regex.Match(eventName, "\"(.*)\"").Groups[1].Value;
                    var wage = int.Parse(Regex.Match(textReader.ReadLine(), @"\d+").Value);
                    var description = Regex.Match(textReader.ReadLine(), "\"(.*)\"").Groups[1].Value;
                    var chance = int.Parse(Regex.Match(textReader.ReadLine(), @"\d+").Value);
                    lifeEvents.Add(new LifeEvent { Name = eventName, Wage = wage, Description = description, Chance = chance });
                    textReader.ReadLine();
                }

                if (!lifeEventDataBase.ContainsKey(jobName)) lifeEventDataBase.Add(jobName, lifeEvents.ToArray());
            }

            return lifeEventDataBase;
        }
    }

}
