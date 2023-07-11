using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponManager;

public class Machine_Gun : BaseWeapon
{

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        weapon_type = WeaponManager.weapon_type.MACHINE_GUN;
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
