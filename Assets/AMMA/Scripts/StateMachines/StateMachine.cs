using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State currentState; // holds current state

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.Tick(Time.deltaTime); //Tick method is our update method it will be updating states
        }
        // we can just write currentState?.Tick(Time.deltaTime);
    
    }
    public void SwitchState(State newState)
    {
        currentState?.Exit();// make null check else how do you know which state to exit

        currentState = newState;

        currentState?.Enter();// we just give newState but always check if isNull?
    }

}
