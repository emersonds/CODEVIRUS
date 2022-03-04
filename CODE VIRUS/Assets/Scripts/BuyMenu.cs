using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyMenu : MonoBehaviour
{
    [SerializeField]
    private Text points;        // Mutation points for upgrades
    [SerializeField]
    private Text context;       // Context tips like price, what points mean
    [SerializeField]
    private Text infected;      // Contains infected and death count

    // Start is called before the first frame update
    void Start()
    {
        context.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        FormatText();
    }

    public void SetContext(string menuItem)
    {
        switch (menuItem)
        {
            case ("points"):
                context.text = "The points you currently have.\nThese points can be used to purchase upgrades.";
                break;
            case ("infected points"):
                context.text = "How many people you have infected or killed with your virus.";
                break;
            case ("infectivity"):
                context.text = "Increase how contagious your virus is by 10%.\nPrice: " + GameManager.GM.InfectCost;
                break;
            case ("lethality"):
                context.text = "Increase how deadly your virus is by 10%.\nPrice: " + GameManager.GM.LethalCost;
                break;
            case ("resilience"):
                context.text = "Increase how sturdy your virus is by 10%.\nPrice: " + GameManager.GM.ResilienceCost;
                break;
            case ("clicker"):
                context.text = "Double the amount of points earned from one click.\nPrice: " + GameManager.GM.ClickerCost;
                break;
            case ("income"):
                context.text = "Increase the amount of points you earn passively by 10%.\nPrice: " + GameManager.GM.IncomeCost;
                break;
        }
    }

    public void ClearContext()
    {
        context.text = "";
    }

    public void UpgradeVirus(string upgrade)
    {
        GameManager.GM.UpgradeVirus(upgrade);
    }

    public void ButtonHover()
    {
        AudioManager.AM.Play("Button Hover");
    }

    public void ButtonClick()
    {
        AudioManager.AM.Play("Button Click");
    }

    void FormatText()
    {
        // Format points text
        string pointsToPrint = GetSuffix(GameManager.GM.MutationPoints);
        string infectedToPrint = GetSuffix(GameManager.GM.InfectedPoints);
        string deathsToPrint = GetSuffix(GameManager.GM.DeathPoints);
        points.text = "[ MUTATION POINTS: " + pointsToPrint + " ]";
        infected.text = "[ INFECTED: " + infectedToPrint + " ]\n" +
                        "[ KILLS: " + deathsToPrint + " ]";
    }

    /// <summary>
    /// Determines how a number should be printed.
    /// This should display 1-999999, 1M, 1B, 1T, etc.
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    string GetSuffix(float points)
    {
        string value = "";

        // Check which suffix to use
        if (points <= 999999)
            value = points.ToString();
        else if (points > 999999 && points <= 999999999)
            value = (points / 1000000f).ToString("0.##") + "M";
        else if (points > 999999999 && points <= 999999999999)
            value = (points / 1000000000f).ToString("0.##") + "B";
        else if (points > 999999999999)
            value = (points / 1000000000000f).ToString("0.##") + "T";

        return value;
    }
}
