using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public static StateController instance;
    public delegate void OnActionButtonDown();
    public OnActionButtonDown worldAction;

    private static State state = State.WORLD;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        if(instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Action") && state == State.WORLD)
        {
            Invoke(worldAction);
        }
    }

    public static void Transition(State state)
    {
        StateController.state = state;
    }

    public static bool IsInState(State state)
    {
        return StateController.state == state;
    }

    public static State GetState()
    {
        return StateController.state;
    }

    private void Invoke(OnActionButtonDown del)
    {
        if(del != null)
        {
            del.Invoke();
        }
    }
}
