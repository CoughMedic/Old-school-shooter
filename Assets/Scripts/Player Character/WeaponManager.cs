using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public enum weapon_type
    {
        PISTOL_1 = 0,
        PISTOL_2 = 1,
        SHOTGUN = 2,
        MACHINE_GUN = 3,
    }

    [Header("ShootingStats")]
    public weapon_type equiped_weapon = 0;
    public BaseWeapon pistol_1;
    public BaseWeapon pistol_2;
    public BaseWeapon shotgun;
    public BaseWeapon machinegun;

    public List<BaseWeapon> weaponsUnlocked;
    private int weapon_select_index = 0;

    [HideInInspector] public BaseWeapon current_weapon;
    private Sprite current_sprite;
    private SpriteRenderer sprite_renderer;

    private BaseWeapon weapon;
    private bool swap_weapon = true;

    // Start is called before the first frame update
    void Start()
    {
        //adds pistol as starter weapon
        weaponsUnlocked.Add(pistol_1);
        pistol_1.unlocked = true;

        //sets starter weapon which is pistol and gets sprite from class
        current_weapon = weaponsUnlocked[weapon_select_index];
        current_sprite = current_weapon.weapons_sprites[0];
        current_weapon.equipped = true;

        //grabs sprite renderer and first sprite in list and sets to the sprite renderer
        sprite_renderer = GetComponentInChildren<SpriteRenderer>();

        sprite_renderer.sprite = current_sprite;


    }

    // Update is called once per frame
    void Update()
    {
        animationManager();
        weapon_type_manager();
    }

    public void AddWeapon(weapon_type type)
    {
        switch(type)
        {
            case weapon_type.PISTOL_2:
                weaponsUnlocked.Add(pistol_2);
                break;
            case weapon_type.MACHINE_GUN:
                weaponsUnlocked.Add(machinegun);
                break;
        }
    }

    void weapon_type_manager()
    {
        float weapon_swap = Input.GetAxis("swap_weapon");

        if(weapon_swap != 0 && swap_weapon == true)
        {

            swap_weapon = false;
            if(weapon_swap > 0.1)
            {
                current_weapon.equipped = false;
                print("Swap");
                //resets to 0 if equal if about to overflow otherwise go up 1
                if (weapon_select_index >= weaponsUnlocked.Count-1)
                {
                    weapon_select_index = 0;
                }
                else
                {
                    weapon_select_index += 1;
                }
            }

            current_weapon = weaponsUnlocked[weapon_select_index];
            equiped_weapon = current_weapon.weapon_type;

            current_weapon.equipped = true;
        }
        else if(weapon_swap == 0)
        {
            swap_weapon = true;
        }
    }

    void animationManager()
    {
        if (current_weapon.play_animation_shoot)
        {
            current_sprite = current_weapon.weapons_sprites[current_weapon.animation_index];
            sprite_renderer.sprite = current_sprite;
        }
        else
        {
            current_sprite = current_weapon.weapons_sprites[0];
            sprite_renderer.sprite = current_sprite;
        }

    }

}
