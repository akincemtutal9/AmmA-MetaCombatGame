using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// We make this class abstract so we can inherit the states for example jumping
public abstract class State //: MonoBehaviour This class won't need to be inherit Unity
{
    
    // Any State will have these methods
    
    public abstract void Enter();
    
    //This method will be update of States it will be using time.deltatime
    public abstract void Tick(float deltaTime);
    
    
    public abstract void Exit();

}
