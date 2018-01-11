using UnityEngine;

[System.Serializable]
public class Stat
{
    public float max;
    public float min;
    public float value;

    public Stat()
    {
        value = max;
    }

    public Stat(float value)
    {
        this.max = value;
        this.value = value;
    }

    public Stat(float min, float max)
    {
        this.min = min;
        this.max = max;
        this.value = max;
    }

    public Stat(float value, float min, float max)
    {
        this.value = value;
        this.min = min;
        this.max = max;
    }

    public static Stat operator +(Stat s1, Stat s2)
    {
        return s1 + s2.value;
    }

    public static Stat operator +(Stat s1, float change)
    {
        s1.value += change;
        s1.value = Mathf.Clamp(s1.value, s1.min, s1.max);
        return s1;
    }

    public static float operator *(Stat s1, float multiplier)
    {
        return s1.value * multiplier;
    }
}
