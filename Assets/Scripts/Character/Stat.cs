using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [HideInInspector]
    public float max
    {
        get
        {
            float ret = _max;
            foreach(Buff buff in buffMax)
            {
                ret += buff.value;
            }
            return ret;
        }
        set
        {
            _max = value;
            MaxChanged();
            StatChanged();
        }
    }

    [HideInInspector]
    public float min
    {
        get
        {
            float ret = _min;
            foreach (Buff buff in buffMin)
            {
                ret += buff.value;
            }
            return ret;
        }
        set
        {
            _min = value;
            MinChanged();
            StatChanged();
        }
    }

    public float current;

    public delegate void HandleOutOfRange();
    public HandleOutOfRange exceed;
    public HandleOutOfRange fallBelow;

    public delegate void OnChange(float value);
    public OnChange currentChanged;
    public OnChange maxChanged;
    public OnChange minChanged;

    public delegate void OnStatChange(Stat stat);
    public OnStatChange statChanged;

    public List<Buff> buffMax = new List<Buff>();
    public List<Buff> buffMin = new List<Buff>();

    [SerializeField]
    private float _max;
    [SerializeField]
    private float _min;

    public Stat()
    {
        current = max;
    }

    public Stat(float value)
    {
        this.max = value;
        this.current = value;
    }

    public Stat(float min, float max)
    {
        this.min = min;
        this.max = max;
        this.current = max;
    }

    public Stat(float value, float min, float max)
    {
        this.current = value;
        this.min = min;
        this.max = max;
    }

    public void Max()
    {
        this.current = this.max;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(this.min, this.current, this.max);
    }

    public void FromVector3(Vector3 v)
    {
        this.min = v.x;
        this.current = v.y;
        this.max = v.z;
    }

    public void CurrentChanged()
    {
        if (currentChanged != null)
        {
            currentChanged.Invoke(this.current);
        }
    }

    public void MaxChanged()
    {
        if (maxChanged != null)
        {
            maxChanged.Invoke(this.current);
        }
    }

    public void MinChanged()
    {
        if (minChanged != null)
        {
            minChanged.Invoke(this.current);
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

    public void CheckOutOfBounds()
    {
        if (current >= max && exceed != null)
        {
            exceed.Invoke();
        }
        if (current <= min && fallBelow != null)
        {
            fallBelow.Invoke();
        }
    }

    #region operator
    public static Stat operator +(Stat s1, Stat s2)
    {
        return s1 + s2.current;
    }

    public static Stat operator +(Stat s, float change)
    {
        s.current += change;
        s.CheckOutOfBounds();
        s.current = Mathf.Clamp(s.current, s.min, s.max);

        s.CurrentChanged();
        s.StatChanged();
        return s;
    }

    public static float operator *(Stat s, float multiplier)
    {
        return s.current * multiplier;
    }

    public static Stat operator -(Stat s1, Stat s2)
    {
        return s1 - s2.current;
    }

    public static Stat operator -(Stat s, float change)
    {
        s.current -= change;
        s.CheckOutOfBounds();
        s.current = Mathf.Clamp(s.current, s.min, s.max);

        s.CurrentChanged();
        s.StatChanged();
        return s;
    }
    #endregion
}
