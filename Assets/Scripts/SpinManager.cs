using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpinManager : MonoBehaviour
{
    public Sprite[] Sprites;
    public Image Symbol;
    public float time;
    public RewardManager rewardManager;
    public BonusWinManager bonusWinManager;
    private bool isStopped;

    private void Start()
    {
        isStopped = true;
        rewardManager = FindObjectOfType<RewardManager>();
        bonusWinManager = FindObjectOfType<BonusWinManager>();
    }

    private void Update()
    {
        if (isStopped)
        {
            return;
        }

        if (Sprites.Length > 0)
        {
            int randomIndex = Random.Range(0, Sprites.Length);
            Symbol.sprite = Sprites[randomIndex];
        }
    }

    public void StopSpin()
    {
        StartCoroutine(DelayNum());
    }

    public void StartSpin()
    {
        int balanceBet = rewardManager.balanceManager.GetCurrentBalance();
        if (balanceBet > 0 && rewardManager.credit >= balanceBet)
        {
            isStopped = false;
            bonusWinManager.ResetBonusWin();
        }
    }

    public bool IsStopped()
    {
        return isStopped;
    }

    public string GetSymbolName()
    {
        return Symbol.sprite.name;
    }

    private IEnumerator DelayNum()
    {
        yield return new WaitForSeconds(time);
        isStopped = true;

        if (bonusWinManager != null)
        {
            bonusWinManager.CheckBonusWin();
        }
    }
}
