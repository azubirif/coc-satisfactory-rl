using System.Collections.Generic;
using System.Linq;
using LemEngine;
using UnityEngine;

public class Objective : MonoBehaviour, IAttackable
{
    public static List<Objective> objectives;
    public static int remainingObjectives;
    public float maxHealth = 50f;
    private float currentHealth;

    void Awake()
    {
        if (objectives == null) objectives = new List<Objective>();

        objectives.Add(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        remainingObjectives = objectives.Count;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float RecieveDamage(float damage)
    {
        float newHealth = currentHealth - damage;
        currentHealth = newHealth;
        if (currentHealth <= 0f) DestroyObjective();

        return newHealth;
    }

    private void DestroyObjective()
    {
        remainingObjectives--;
        objectives.Remove(this);
        Debug.Log("Objective destroyed!");
        Destroy(gameObject);
    }

    public static Transform[] GetObjectiveTransforms()
    {
        return objectives.ConvertAll<Transform>(x => x.gameObject.transform).ToArray();
    }
}
