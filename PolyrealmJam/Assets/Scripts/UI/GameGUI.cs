using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class GameGUI : MonoBehaviour
{
    [Header("Energy")]
    [SerializeField] RectTransform energyFill;
    [SerializeField] Text energyPercent;

    [Header("Oxygen")]
    [SerializeField] RectTransform oxyFill;
    [SerializeField] Text oxyPercent;

    [Header("Actions")]
    [SerializeField] List<Image> actionDots;
    [SerializeField] Sprite actionOnSprite;
    [SerializeField] Sprite actionOffSprite;

    [Header("Food")]
    [SerializeField] Text hungerText;
    [SerializeField] Image hungerBar;
    [SerializeField] Image hungerFill;
    [SerializeField] Image hungerIcon;
    [SerializeField] Sprite hungryIconSprite;
    [SerializeField] Sprite fedIconSprite;
    [SerializeField] Color fillHungryColor;
    [SerializeField] Color barHungryColor;
    [SerializeField] Color fillFedColor;
    [SerializeField] Color barFedColor;

    private void Update()
    {
        UpdateEnergy();
        UpdateOxy();
        UpdateAction();
        UpdateHunger();
    }

    private void UpdateEnergy()
    {
        Vector2 anchorTgt = energyFill.anchorMax;
        anchorTgt.x = GameStats.Instance.powerLevel;
        energyFill.anchorMax = anchorTgt;

        int power = Mathf.FloorToInt(GameStats.Instance.powerLevel * 100f);
        energyPercent.text = string.Format(power + "%");
    }

    private void UpdateOxy()
    {
        Vector2 anchorTgt = oxyFill.anchorMax;
        anchorTgt.x = GameStats.Instance.oxygenLevel;
        oxyFill.anchorMax = anchorTgt;

        int oxy = Mathf.FloorToInt(GameStats.Instance.oxygenLevel * 100f);
        oxyPercent.text = string.Format(oxy + "%");
    }

    private void UpdateAction()
    {
        for (int i = 0; i < actionDots.Count; i++)
        {
            if (i < GameStats.Instance.availableAction)
                actionDots[i].sprite = actionOnSprite;

            else
                actionDots[i].sprite = actionOffSprite;
        }
    }

    private void UpdateHunger()
    {
        if (GameStats.Instance.haveEaten)
        {
            hungerIcon.sprite = fedIconSprite;
            hungerFill.color = fillFedColor;
            hungerBar.color = barFedColor;
            hungerText.text = "Fed";
        }
        else
        {
            hungerIcon.sprite = hungryIconSprite;
            hungerFill.color = fillHungryColor;
            hungerBar.color = barHungryColor;
            hungerText.text = "Hungry";
        }
    }
}
