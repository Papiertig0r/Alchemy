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

    public delegate void OnChange(float value);
    public OnChange valueChanged;
    public OnChange maxChanged;
    public OnChange minChanged;

    public delegate void OnStatChange(Stat stat);
    public OnStatChange statChanged;

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
        if(valueChanged != null)
        {
            valueChanged.Invoke(this.value);
        }
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

        ValueChanged();
        MaxChanged();
        MinChanged();
        StatChanged();
    }

    public void ValueChanged()
    {
        if (valueChanged != null)
        {
            valueChanged.Invoke(this.value);
        }
    }

    public void MaxChanged()
    {
        if (maxChanged != null)
        {
            maxChanged.Invoke(this.value);
        }
    }

    public void MinChanged()
    {
        if (minChanged != null)
        {
            minChanged.Invoke(this.value);
        }
    }

    public void StatChanged()
    {
        if (statChanged != null)
        {
            statChanged.Invoke(this);
        }
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
        s1.ValueChanged();
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
        s.ValueChanged();
        s.StatChanged();
        return s;
    }

    public static float operator *(Stat s, float multiplier)
    {
        return s.value * multiplier;
    }

    public static Stat operator -(Stat s1, Stat s2)
    {
        s1.ValueChanged();
        s1.StatChanged();
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
        s.ValueChanged();
        s.StatChanged();
        return s;
    }
    #endregion
}
