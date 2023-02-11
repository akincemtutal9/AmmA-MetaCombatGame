using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgingState : PlayerBaseState
{
    private readonly int DodgingBlendTreeHash = Animator.StringToHash("DodgeBlendTree");
    private readonly int DodgeForwardHash = Animator.StringToHash("DodgeForward");
    private readonly int DodgeRightHash = Animator.StringToHash("DodgeRight");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    private List<PlayerBaseState> state = new List<PlayerBaseState>();
    
    private float remainingDodgeTime;
    private Vector3 dodgingDirectionInput;
    public PlayerDodgingState(PlayerStateMachine stateMachine , Vector3 dodgingDirectionInput) : base(stateMachine)
    {
        this.dodgingDirectionInput = dodgingDirectionInput;
    }
    public override void Enter()
    {
        state.Add(new PlayerTargetingState(stateMachine));
        state.Add(new PlayerFreeLookState(stateMachine));
        
        remainingDodgeTime = stateMachine.DodgeDuration;
        
        stateMachine.Animator.SetFloat(DodgeForwardHash,dodgingDirectionInput.y);
        stateMachine.Animator.SetFloat(DodgeRightHash,dodgingDirectionInput.x);
        stateMachine.Animator.CrossFadeInFixedTime(DodgingBlendTreeHash,CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();
        var transform = stateMachine.transform;
        
        movement += transform.right * (dodgingDirectionInput.x * stateMachine.DodgeLength) / stateMachine.DodgeDuration;
        movement += transform.forward * (dodgingDirectionInput.y * stateMachine.DodgeLength) / stateMachine.DodgeDuration; 
        
        Move(movement,deltaTime);
        FaceTarget();
        remainingDodgeTime -= deltaTime;
        if (remainingDodgeTime <= 0)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }
}
