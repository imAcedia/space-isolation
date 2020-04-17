using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    #region Statics

    private static GameStats instance = null;
    public static GameStats Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameStats>();

            if (instance == null)
                Debug.LogErrorFormat("Cannot find GameStats in the game. Please make sure there is initialized GameStats.");

            return instance;
        }

        set => instance = value;
    }

    #endregion

    [System.Serializable]
    public struct Machine
    {
        public bool isOn;
        public bool isFixed;
    }

    public int currentDays = 1;

    public float powerLevel = 1f;
    public float oxygenLevel = 1f;
    public bool haveEaten = true;
    public int powerCell = 0;

    public DayData[] dayDatas = new DayData[0];

    public int availableAction = 5;
    public int AvailableAction
    {
        get => availableAction;
        set
        {
            availableAction = value;

            if (availableAction <= 0)
                EndDay();
        }
    }
    public static string playerName = "Bryan";

    public Machine engine = new Machine()
    {
        isOn = false,
        isFixed = false,
    };

    public Machine oxygenGenerator = new Machine()
    {
        isOn = false,
        isFixed = false,
    };

    public Machine navigationSystem = new Machine()
    {
        isOn = false,
        isFixed = false,
    };

    public Machine materializer = new Machine()
    {
        isOn = true,
        isFixed = false,
    };

    public bool SurvivedTheDay()
    {
        if (powerLevel <= 0f)
            return false;

        if (oxygenLevel < .2f)
            return false;

        if (!haveEaten)
            return false;

        return true;
    }

    public void EndDay()
    {
        StartCoroutine(NextDayCo());
    }

    private IEnumerator NextDayCo()
    {
        if (!SurvivedTheDay())
        {
            GameOver();
            yield break;
        }

        currentDays++;

        if (currentDays >= dayDatas.Length)
        {
            RandomStat();
        }
        else
        {
            DayData currentDayData = dayDatas[currentDays];

            if (currentDayData.navBroken) navigationSystem.isFixed = false;
            if (currentDayData.oxGenBroken) oxygenGenerator.isFixed = false;
            if (currentDayData.materializerBroken) materializer.isFixed = false;
        }

        DayFader.instance.FadeOutToBlack("Day " + currentDays, powerLevel + "% Power remaining...");
    }

    public void RandomStat()
    {
        if (Random.value <= 0.1f) navigationSystem.isFixed = false;
        if (Random.value <= 0.1f) oxygenGenerator.isFixed = false;
        if (Random.value <= 0.1f) materializer.isFixed = false;
    }

    public void GameOver()
    {
        DayFader.instance.FadeOutToBlackEnd("Game Over", string.Format("You survived for {0} days without anybody besides yourself...", currentDays));
    }

    public void EndGame()
    {
        DayFader.instance.FadeOutToBlackEnd("Game Over", string.Format("You survived for {0} days without anybody besides yourself...", currentDays));
    }
}
