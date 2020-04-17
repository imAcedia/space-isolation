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

    public float power = 1f;
    public float oxygenLevel = 1f;
    public int availableAction = 5;
    public bool haveEaten = true;

    public static string playerName = "Bryan";

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

    public bool SurvivedNextDay()
    {
        if (power <= 0f)
            return false;

        if (oxygenLevel < .2f)
            return false;

        if (!haveEaten)
            return false;

        return true;
    }
}
