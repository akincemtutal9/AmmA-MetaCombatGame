using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponLogic;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void EnableWeapon()
    {
        weaponLogic.SetActive(true);
    }

    private void DisableWeapon()
    {
        weaponLogic.SetActive(false);
    }
}
