using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] float upgradeTextToDefaultTime;

    private int upgrade1;
    private int upgrade2;
    private int upgrade3;
    private bool upgrade1Bought = false;
    private bool upgrade2Bought = false;
    private bool upgrade3Bought = false;

    [SerializeField] private List<string> upgradeNames = new List<string>();
    [SerializeField] private List<int> upgradeCosts = new List<int>();

    private TextMeshProUGUI upgrade1Text;
    private TextMeshProUGUI upgrade2Text;
    private TextMeshProUGUI upgrade3Text;
    private TextMeshProUGUI moneyText;

    private PlayerController playerController;

    void Start()
    {
        upgrade1Text = transform.Find("Upgrade 1").GetChild(0).GetComponent<TextMeshProUGUI>();
        upgrade2Text = transform.Find("Upgrade 2").GetChild(0).GetComponent<TextMeshProUGUI>();
        upgrade3Text = transform.Find("Upgrade 3").GetChild(0).GetComponent<TextMeshProUGUI>();
        moneyText = transform.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        playerController = FindObjectOfType<PlayerController>();

        ShuffleUpgrades();
    }

    void Update()
    {
        // TEMPORARY
        if (Input.GetKey("j"))
        {
            ShuffleUpgrades();
        }
    }

    // Shuffles the shop upgrades
    public void ShuffleUpgrades()
    {
        // Set the bought variables to false
        upgrade1Bought = false;
        upgrade2Bought = false;
        upgrade3Bought = false;

        // Generate three different random numbers within the bounds of the arrays
        upgrade1 = Random.Range(0, upgradeNames.Count);
        upgrade2 = Random.Range(0, upgradeNames.Count);
        while (upgrade2 == upgrade1)
        {
            upgrade2 = Random.Range(0, upgradeNames.Count);
        }
        upgrade3 = Random.Range(0, upgradeNames.Count);
        while (upgrade3 == upgrade1 || upgrade3 == upgrade2)
        {
            upgrade3 = Random.Range(0, upgradeNames.Count);
        }

        // Set the three upgrade names and costs
        upgrade1Text.text = $"{upgradeNames[upgrade1]}\n{upgradeCosts[upgrade1]} money";
        upgrade2Text.text = $"{upgradeNames[upgrade2]}\n{upgradeCosts[upgrade2]} money";
        upgrade3Text.text = $"{upgradeNames[upgrade3]}\n{upgradeCosts[upgrade3]} money";
    }

    // Handles upgrade buying
    // Not changing the text to "Not enough money!" at the moment. Very confused.
    public void BuyUpgrade(int upgrade)
    {
        if (upgrade == 1 && !upgrade1Bought)
        {
            int cost = upgradeCosts[upgrade1];
            if (playerController.money >= cost)
            {
                upgrade1Bought = true;
                upgrade1Text.text = "Sold!";
                playerController.money -= cost;
                AddUpgrade(upgradeNames[upgrade1], upgradeCosts[upgrade1]);
            }
            else
            {
                StartCoroutine(UpgradeTextToDefault(upgrade1Text, upgrade1Text.text, upgradeTextToDefaultTime));
                upgrade1Text.text = "Not enough diamonds!";
            }
        }
        else if (upgrade == 2 && !upgrade2Bought)
        {
            int cost = upgradeCosts[upgrade2];
            if (playerController.money >= cost)
            {
                upgrade2Bought = true;
                upgrade2Text.text = "Sold!";
                playerController.money -= cost;
                AddUpgrade(upgradeNames[upgrade2], upgradeCosts[upgrade2]);
            }
            else
            {
                StartCoroutine(UpgradeTextToDefault(upgrade2Text, upgrade2Text.text, upgradeTextToDefaultTime));
                upgrade2Text.text = "Not enough diamonds!";
            }
        }
        else if (upgrade == 3 && !upgrade3Bought)
        {
            int cost = upgradeCosts[upgrade3];
            if (playerController.money >= cost)
            {
                upgrade3Bought = true;
                upgrade3Text.text = "Sold!";
                playerController.money -= cost;
                AddUpgrade(upgradeNames[upgrade3], upgradeCosts[upgrade3]);
            }
            else
            {
                StartCoroutine(UpgradeTextToDefault(upgrade3Text, upgrade3Text.text, upgradeTextToDefaultTime));
                upgrade3Text.text = "Not enough diamonds!";
            }
        }
        moneyText.text = $"You have {playerController.money} money";
    }
    
    // Gives the player the buff
    private void AddUpgrade(string name, int cost)
    {
        if (name.Contains("Speed"))
        {
            if (name.Contains("2"))
            {
                playerController.speed += 2;
            }
            else
            {
                playerController.speed += 1;
            }
        }
        else if (name.Contains("Health"))
        {
            if (name.Contains("2"))
            {
                playerController.maxHealth += 20;
                playerController.health += 20;
                playerController.UpdateHealth();
            }
            else
            {
                playerController.maxHealth += 10;
                playerController.health += 10;
                playerController.UpdateHealth();
            }
        }
        else if (name.Contains("Damage"))
        {
            if (name.Contains("2"))
            {

            }
            else
            {

            }
        }
        else if (name.Contains("Coins"))
        {
            if (name.Contains("2"))
            {

            }
            else
            {

            }
        }
    }

    // Switches the upgrade button's text back to default; connected to BuyUpgrade()
    IEnumerator UpgradeTextToDefault(TextMeshProUGUI upgrade, string defaultText, float time)
    {
        yield return new WaitForSeconds(time);
        upgrade.text = defaultText;
    }
}
