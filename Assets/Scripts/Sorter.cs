using UnityEngine;

public class Sorter : MonoBehaviour
{
    void Awake ()
    {
        SetPosition();
    }
    
    void Update ()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        Vector3 newPos = transform.position;
        newPos.z = newPos.y;
        transform.position = newPos;
    }
}
