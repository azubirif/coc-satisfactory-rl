using UnityEngine;
using UnityEngine.Scripting;

public class PlayerResources : MonoBehaviour
{
    public static PlayerResources instance;
    public int money = 0;

    public void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);
    }

    public void WinMoney(int pay)
    {
        money += pay;
    }
}
