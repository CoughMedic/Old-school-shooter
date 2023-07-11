using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol_2 : BaseWeapon
{
    public override void Start()
    {
        base.Start();
        weapon_type = WeaponManager.weapon_type.PISTOL_2;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void shoot()
    {
        base.shoot();

    }
}
