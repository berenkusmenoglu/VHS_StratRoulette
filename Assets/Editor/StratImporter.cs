using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.IO;

[CustomEditor(typeof(GameManager))]
public class StratImporter : Editor
{
    public List<Strategy> TranslateCSV(string fileName, ESide side )
    {
        List<Strategy> stratList = new List<Strategy>();

        StreamReader reader = new StreamReader(fileName);
        bool endOfFile = false;
        while (!endOfFile)
        {
            string dataString = reader.ReadLine();
            if (dataString == null)
            {
                endOfFile = true;
                break;
            }
            string[] dataValues = dataString.Split(',');
            if(dataValues.Length == 0)
            {
                continue;
            }
            // side
            // diff
            // title
            // descr
            if (dataValues[0] != "Side" && dataValues[1] != "")
            {
                EDifficulty difficulty = (EDifficulty)System.Enum.Parse(typeof(EDifficulty), dataValues[1], true);
                ESide sideEnum = (ESide)System.Enum.Parse(typeof(ESide), dataValues[0], true);
                if(sideEnum == side)
                {
                    Strategy strat = new Strategy(dataValues[2].Replace(';',','), dataValues[3].Replace(';', ','), difficulty);
                    stratList.Add(strat);
                }
                
            }
            
        }

        return stratList;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Import Strategies"))
        {
            GameManager manager = (GameManager)target;
            List<Strategy> teenStrats = TranslateCSV("Assets/Resources/strats.csv", ESide.Teen);
            List<Strategy> mosterStrats = TranslateCSV("Assets/Resources/strats.csv", ESide.Monster);

            manager.SetStrats(teenStrats, mosterStrats);
        }
    }

}