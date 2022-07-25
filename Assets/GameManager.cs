using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public enum EDifficulty
{
    NONE,
    Easy,
    Normal,
    Hard,
    Expert,
    Roby,
    COUNT
}


[System.Serializable]
public struct Strategy
{
    public string Title;
    public string Description;
    public EDifficulty Difficulty;


    public Strategy(string title, string desc, EDifficulty diff)
    {
        this.Title = title;
        this.Description = desc;
        this.Difficulty = diff;
    }
}


public class GameManager : MonoBehaviour
{
    // save ui text element StratTitle

    public TMP_Text StratTitle;
    public TMP_Text StratDescription;
    public TMP_Dropdown DifficultyDropdown;

    public List<Strategy> teenStrategies;
    public List<Strategy> monsterStrategies;

    private void Awake()
    {
        StratTitle.text = "";
        StratDescription.text = "";

        DifficultyDropdown.ClearOptions();

        List<string> options = new List<string>((int)EDifficulty.COUNT);

        for (int i = (int)EDifficulty.NONE + 1; i < (int)EDifficulty.COUNT; i++)
        {
            EDifficulty diff = (EDifficulty)i;
            options.Add(diff.ToString());
        }

        DifficultyDropdown.AddOptions(options);
    }

    public void OnTeenPressed()
    {
        EDifficulty chosenDiff = (EDifficulty)(DifficultyDropdown.value+1);

        IEnumerable<Strategy> strategiesWithDiff = teenStrategies.Where(Strategy => Strategy.Difficulty == chosenDiff);

        // pick a random strategy   
        int randomIndex = Random.Range(0, strategiesWithDiff.Count());
        Strategy randomStrat = new Strategy("", "", EDifficulty.NONE);

        int i = 0;
        foreach(Strategy strat in strategiesWithDiff)
        {
            if (i == randomIndex)
            {
                randomStrat = strat;
                break;
            }
            i++;
        }

        StratTitle.text = randomStrat.Title;
        StratDescription.text = randomStrat.Description;
    }

    public void OnMonsterPressed()
    {
        EDifficulty chosenDiff = (EDifficulty)(DifficultyDropdown.value + 1);

        IEnumerable<Strategy> strategiesWithDiff = monsterStrategies.Where(Strategy => Strategy.Difficulty == chosenDiff);

        // pick a random strategy   
        int randomIndex = Random.Range(0, strategiesWithDiff.Count());
        Strategy randomStrat = new Strategy("", "", EDifficulty.NONE);

        int i = 0;
        foreach (Strategy strat in strategiesWithDiff)
        {
            if (i == randomIndex)
            {
                randomStrat = strat;
                break;
            }
            i++;
        }

        StratTitle.text = randomStrat.Title;
        StratDescription.text = randomStrat.Description;
    }
}
