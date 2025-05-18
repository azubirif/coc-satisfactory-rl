using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UnityEvent onEndRound;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        onEndRound = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnEnemyDeath()
    {
        if (SimpleEnemy.enemyCount == 0) OnEndRound();
    }

    public void OnEndRound()
    {
        onEndRound.Invoke();
        print("Round Won");
    }
}
