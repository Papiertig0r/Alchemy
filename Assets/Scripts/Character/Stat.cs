using UnityEngine;

[System.Serializable]
public class Stat
{
    public float max;
    public float min;
    public float value;

    public delegate void HandleOutOfRange();
    public HandleOutOfRange exceed;
    public HandleOutOfRange fallBelow;

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

    public void Max()
    {
        this.value = this.max;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(this.min, this.value, this.max);
    }

    public void FromVector3(Vector3 v)
    {
        this.min = v.x;
        this.value = v.y;
        this.max = v.z;
    }

    public Vector3 Vector3
    {
        get
        {
            return ToVector3();
        }

        set
        {
            FromVector3(value);
        }
    }

    #region operator
    public static Stat operator +(Stat s1, Stat s2)
    {
        return s1 + s2.value;
    }

    public static Stat operator +(Stat s, float change)
    {
        s.value += change;
        if(s.value >= s.max && s.exceed != null)
        {
            s.exceed.Invoke();
        }
        s.value = Mathf.Clamp(s.value, s.min, s.max);
        return s;
    }

    public static float operator *(Stat s, float multiplier)
    {
        return s.value * multiplier;
    }

    public static Stat operator -(Stat s1, Stat s2)
    {
        return s1 - s2.value;
    }

    public static Stat operator -(Stat s, float change)
    {
        s.value -= change;
        if (s.value <= s.min && s.fallBelow != null)
        {
            s.fallBelow.Invoke();
        }
        s.value = Mathf.Clamp(s.value, s.min, s.max);
        return s;
    }
    #endregion
}
