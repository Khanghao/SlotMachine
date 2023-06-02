using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusWinManager : MonoBehaviour
{
    public Text BonusWin;
    public Text Credit;
    public SpinManager[] spinManagers;
    public RewardManager rewardManager;
    private int currentBonusWin;
    private int stoppedSlotsCount;

    private void Start()
    {
        ResetBonusWin();
    }

    public void ResetBonusWin()
    {
        currentBonusWin = 0;
        stoppedSlotsCount = 0;
        BonusWin.text = currentBonusWin.ToString();
    }

    public void CheckBonusWin()
    {
        stoppedSlotsCount++;

        if (stoppedSlotsCount < spinManagers.Length)
        {
            return;
        }

        int betAmount = rewardManager.balanceManager.GetCurrentBalance();
        int credit = rewardManager.credit;

        int matchingSymbolCount = GetMatchingSymbolCount();

        int rewardMultiplier = 0;

        if (matchingSymbolCount >= 2 && matchingSymbolCount <= 5)
        {
            rewardMultiplier = matchingSymbolCount;
        }
        else if (matchingSymbolCount == 5)
        {
            rewardMultiplier = 10;
        }
        int rewardAmount = betAmount * rewardMultiplier;
        currentBonusWin = rewardAmount;
        credit += currentBonusWin;
        rewardManager.credit = credit;
        BonusWin.text = currentBonusWin.ToString();
        Credit.text = credit.ToString();
    }

    private int GetMatchingSymbolCount()
    {
        Dictionary<string, int> symbolCounts = new Dictionary<string, int>();

        foreach (var spinManager in spinManagers)
        {
            string symbolName = spinManager.GetSymbolName();

            if (symbolCounts.ContainsKey(symbolName))
            {
                symbolCounts[symbolName]++;
            }
            else
            {
                symbolCounts[symbolName] = 1;
            }
        }

        int matchingSymbolCount = 0;

        foreach (var count in symbolCounts)
        {
            if (count.Value > matchingSymbolCount)
            {
                matchingSymbolCount = count.Value;
            }
        }

        return matchingSymbolCount;
    }
}
