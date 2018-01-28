using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public static StateController instance;
    public delegate void OnActionButtonDown();
    public OnActionButtonDown worldAction;
    public OnActionButtonDown inventoryUiAction;
    public OnActionButtonDown toolUiAction;

    private State state = State.WORLD;

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

        //toolUiAction += Log;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Action"))
        {
            HandleActions();
        }

        if(Input.GetButtonDown("Dodge/Abort"))
        {
            HandleDodgeAbort();
        }
    }

    private void HandleActions()
    {
        switch (state)
        {
            default:
            case State.WORLD:
                Invoke(worldAction);
                break;
            case State.INVENTORY:
                Invoke(inventoryUiAction);
                break;
            case State.TOOL_UI:
                Invoke(toolUiAction);
                break;
            case State.INVENTORY_AND_TOOL:
                break;
        }
    }

    private void HandleDodgeAbort()
    {
        switch (state)
        {
            default:
            case State.WORLD:
                Debug.Log("Dodge");
                break;
            case State.INVENTORY:
                
                break;
            case State.TOOL_UI:
                Invoke(worldAction);
                break;
            case State.INVENTORY_AND_TOOL:
                break;
        }
    }

    public void Transition(State state)
    {
        this.state = state;
    }

    private void Invoke(OnActionButtonDown del)
    {
        if(del != null)
        {
            del.Invoke();
        }
    }

    private void Log()
    {
        Debug.Log("Action on tool ui");
    }
}
