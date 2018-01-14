using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 rotation;
    private AudioSource oneShotSource;
    private Vector3 targetPosition;
    private bool isThrown = false;

    // Use this for initialization
    void Start ()
    {
        
    }

    // Update is called once per frame
    void Update ()
    {
        if(isThrown)
        {
            transform.Translate((targetPosition - transform.position).normalized * Time.deltaTime * speed, Space.World);
            transform.Rotate(rotation, Space.Self);

            if ((targetPosition - transform.position).magnitude < 0.2f)
            {
                Land();
            }
        }
    }

    public void Throw(Vector3 targetPosition, float accuracy)
    {
        this.transform.SetParent(ItemPool.itemPool.transform);
        this.gameObject.SetActive(true);
        Vector2 randomArea = Random.insideUnitCircle * accuracy;
        this.targetPosition = targetPosition + new Vector3(randomArea.x, randomArea.y, 0f);
        isThrown = true;

        if(oneShotSource == null)
        {
            oneShotSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Land()
    {
        isThrown = false;
        transform.rotation = Quaternion.identity;
        //Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        BoxCollider2D hit = coll.GetComponent<BoxCollider2D>();
        if(isThrown && hit != null && coll.tag != "Player")
        {
            Debug.Log("Hit " + coll.name);
            Land();
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (isThrown)
        {
            Debug.Log("Exit " + coll.name);
        }
    }
}
