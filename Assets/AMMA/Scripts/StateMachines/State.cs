// We make this class abstract so we can inherit the states for example jumping

using UnityEngine;

public abstract class State //: MonoBehaviour This class won't need to be inherit Unity
{
    
    // Any State will have these methods
    
    public abstract void Enter();
    
    //This method will be update of States it will be using time.deltatime
    public abstract void Tick(float deltaTime);
    
    
    public abstract void Exit();
    protected float GetNormalizedTime(Animator anim)
    {
        AnimatorStateInfo currentInfo = anim.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = anim.GetNextAnimatorStateInfo(0);

        if (anim.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!anim.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0; }
    }
}
