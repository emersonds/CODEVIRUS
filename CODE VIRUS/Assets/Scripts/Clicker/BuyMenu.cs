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
                context.text = "Increase how contagious your virus is by 10%.\nPrice: " + GameManager.GM.GetSuffix(GameManager.GM.InfectCost);
                break;
            case ("lethality"):
                context.text = "Increase how deadly your virus is by 10%.\nPrice: " + GameManager.GM.GetSuffix(GameManager.GM.LethalCost);
                break;
            case ("resilience"):
                context.text = "Increase how sturdy your virus is by 10%.\nPrice: " + GameManager.GM.GetSuffix(GameManager.GM.ResilienceCost);
                break;
            case ("clicker"):
                context.text = "Double the amount of points earned from one click.\nPrice: " + GameManager.GM.GetSuffix(GameManager.GM.ClickerCost);
                break;
            case ("income"):
                context.text = "Increase the amount of points you earn passively by 10%.\nPrice: " + GameManager.GM.GetSuffix(GameManager.GM.IncomeCost);
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
        string pointsToPrint = GameManager.GM.GetSuffix(GameManager.GM.MutationPoints);
        string infectedToPrint = GameManager.GM.GetSuffix(GameManager.GM.InfectedPoints);
        string deathsToPrint = GameManager.GM.GetSuffix(GameManager.GM.DeathPoints);
        points.text = "[ MUTATION POINTS: " + pointsToPrint + " ]";
        infected.text = "[ INFECTED: " + infectedToPrint + " ]\n" +
                        "[ KILLS: " + deathsToPrint + " ]";
    }

    public void ChangeScene(string scene)
    {
        GameManager.GM.ChangeScenes(scene);
    }
}
