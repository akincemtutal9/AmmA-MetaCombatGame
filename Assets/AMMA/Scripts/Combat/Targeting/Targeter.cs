using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    public List<Target> targets = new List<Target>();

    private void OnTriggerEnter(Collider other)
    {
        Target target = other.GetComponent<Target>();     
        if(target == null) { return; }
        else { targets.Add(target); }
    }

    private void OnTriggerExit(Collider other)
    {
        Target target = other.GetComponent<Target>();
        if (target == null) { return; }
        else { targets.Remove(target); }
    }
}
