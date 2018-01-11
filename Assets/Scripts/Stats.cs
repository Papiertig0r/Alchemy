using UnityEngine;

public class Stats : MonoBehaviour
{
    public Stat health;
    public Stat mana;

    public Stat healthRegen;
    public Stat manaRegen;

    public float speed;
    public float runningSpeed;
    public float targetingSpeed;

    public float range;

    private void Update()
    {
        health += healthRegen * Time.deltaTime;
        mana += manaRegen * Time.deltaTime;
    }
}
