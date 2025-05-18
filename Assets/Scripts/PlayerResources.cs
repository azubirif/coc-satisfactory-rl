using UnityEngine;
using TMPro;

public class PlayerResources : MonoBehaviour
{
    public static PlayerResources instance;
    public TMP_Text moneyText;
    public int money = 0;

    public void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);
    }

    void Start()
    {
        UpdateMoney();
    }

    public void WinMoney(int pay)
    {
        money += pay;
        UpdateMoney();
    }

    private void UpdateMoney()
    {
        moneyText.text = "Money: " + money.ToString();
    }
}
