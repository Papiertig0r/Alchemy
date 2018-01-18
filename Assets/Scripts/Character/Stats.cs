using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Alchemy/Stats")]
public class Stats : ScriptableObject
{
    public Stat health;
    public Stat mana;

    public Stat healthRegen;
    public Stat manaRegen;

    public float speed;
    public float runningSpeed;
    public float targetingSpeed;

    public float range;

    public float attack;
    public float defense;

    public float accuracy;

    public List<Buff> activeBuffs = new List<Buff>();

    public delegate void UpdateHandler();
    public UpdateHandler onUpdate;

    public void Start()
    {
        health.Max();
        mana.Max();
    }

    public void Update()
    {
        health += healthRegen * Time.deltaTime;
        mana += manaRegen * Time.deltaTime;

        if(onUpdate != null)
        {
            onUpdate.Invoke();
        }
    }
}
