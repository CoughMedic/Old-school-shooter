using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Pickup : MonoBehaviour
{
    public WeaponManager.weapon_type pickup_type;
    public int amount;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            var weapons = other.gameObject.GetComponentsInChildren<BaseWeapon>();

            print(weapons.Length);
            foreach (var weapon in weapons)
            {
                if (weapon.weapon_type == pickup_type)
                {
                    weapon.addAmmo(amount);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}

