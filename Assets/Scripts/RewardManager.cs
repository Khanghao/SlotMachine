using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    public class BalanceManager
    {
        private int currentBalance;

        public BalanceManager(int initialBalance)
        {
            currentBalance = initialBalance;
        }

        public int GetCurrentBalance()
        {
            return currentBalance;
        }

        public void AddBalance(int amount)
        {
            currentBalance += amount;
        }

        public bool SubtractBalance(int amount)
        {
            if (currentBalance >= amount)
            {
                currentBalance -= amount;
                return true;
            }
            return false;
        }
    }

    public Text BalanceBet;
    public Text CreditText;
    public int amount = 100;
    public int credit = 10000;
    public BalanceManager balanceManager;
    public bool bet;

    void Start()
    {
        bet = false;
        balanceManager = new BalanceManager(0);
        UpdateBalanceText();
    }

    public void AddBet()
    {
        int currentBalance = balanceManager.GetCurrentBalance();
        if(currentBalance < credit) {
            bet = true;
            balanceManager.AddBalance(amount);
            UpdateBalanceText();
        }
       
    }

    public void SubtractBet()
    {
        bool subtracted = balanceManager.SubtractBalance(amount);
        if (subtracted)
        {
            UpdateBalanceText();
        }
    }

    public void SubtractCredit()
    {
        int currentBalance = balanceManager.GetCurrentBalance();
        if (credit >= currentBalance && bet)
        {
            credit -= currentBalance;
            UpdateBalanceText();
        }
    }

    public void AllIn()
    {
        if (credit > 0)
        {
            bet = true;
            int currentBalance = balanceManager.GetCurrentBalance();
            int betAmount = credit - currentBalance;
            balanceManager.AddBalance(betAmount);
            UpdateBalanceText();
        }
    }

    public void AddCredit(int amount)
    {
        credit += amount;
        UpdateBalanceText();
    }

    private void UpdateBalanceText()
    {
        int currentBalance = balanceManager.GetCurrentBalance();
        BalanceBet.text = currentBalance.ToString();
        CreditText.text = credit.ToString();
    }
}
