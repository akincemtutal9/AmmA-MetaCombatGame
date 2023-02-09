using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyChaseState : EnemyBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    
    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash,CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {        
        if (!IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
        else if (IsInAttackRange())
        {
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
            return;
        }
        FacePlayer();
        MoveToPlayer(deltaTime);
        stateMachine.Animator.SetFloat(SpeedHash,1f,AnimatorDampTime,deltaTime);
    }

    public override void Exit()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }

    private void MoveToPlayer(float deltaTime)
    {
        if (stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.Agent.destination = stateMachine.Player.transform.position;
            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
        }
        stateMachine.Agent.velocity = stateMachine.Controller.velocity;

    }

    private bool IsInAttackRange()
    {
        if (stateMachine.Player.isDead) { return false;}
        
        var playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;
        return playerDistanceSqr <= Mathf.Pow(stateMachine.AttackRange,2);
    }

}