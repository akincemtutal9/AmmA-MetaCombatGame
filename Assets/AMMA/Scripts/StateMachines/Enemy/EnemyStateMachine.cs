using System;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    
    [field: SerializeField]
    public Animator Animator { get; private set; }

    [field: SerializeField]
    public float PlayerChasingRange { get; private set; }
    public GameObject Player { get; private set; }
    
    [field: SerializeField]
    public ForceReceiver ForceReceiver { get; set; }
     
    [field: SerializeField]
    public CharacterController Controller { get; set; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag(nameof(Player));
        SwitchState(new EnemyIdleState(this));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,PlayerChasingRange);
    }
}