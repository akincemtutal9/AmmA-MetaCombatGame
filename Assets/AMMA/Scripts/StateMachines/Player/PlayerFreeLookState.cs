using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

    private const float AnimatorDampTime = 0.1f;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnTarget;    
    }
    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();
       
        stateMachine.Controller.Move(movement.normalized * stateMachine.FreeLookMovementSpeed * deltaTime);
        
        if (stateMachine.InputReader.MovementValue == Vector2.zero) 
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            
            return; 
        }
        else 
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
            FaceMovementDirection(movement.normalized , deltaTime); // Player will look at the axis which he/she is going
        }

    }
    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }
    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y + right * stateMachine.InputReader.MovementValue.x; 
    }
    private void FaceMovementDirection(Vector3 movement , float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationDamping);
    }

    private void OnTarget()
    {
        if (!stateMachine.Targeter.SelectTarget()) { return; }
        
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }
    
}
