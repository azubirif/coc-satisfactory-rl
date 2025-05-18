using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    public int moneyPerRound = 4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance.onEndRound.AddListener(OnEndRound);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEndRound()
    {
        PlayerResources.instance.WinMoney(moneyPerRound);
    }
}
