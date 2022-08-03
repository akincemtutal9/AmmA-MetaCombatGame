using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    
    private float verticalVelocity;

    public Vector3 movement => Vector3.up * verticalVelocity;

    // Update is called once per frame
    void Update()
    {
        if (verticalVelocity<0f && controller.isGrounded)
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }
}
