using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("WeaponStats")]
    public int ammo_count;
    public float shoot_delay;
    public Sprite[] weapons_sprites;
    public GameObject bullet_prefab;
    private float Cooldown;
    public bool unlocked = false;

    public TextMeshProUGUI text;

    public WeaponManager.weapon_type weapon_type;
    public bool equipped = false;

    [Header("Animation vars")]
    [HideInInspector] public bool play_animation_shoot = false;
    private bool animation_finished = true;
    public int animation_index;
    private int animation_index_max;
    public float delay;
    private float current_delay;

    [Header("Player vars")]
    public GameObject player_go;
    public AudioSource audio_source;
    public AudioClip audio_clip;

    public virtual void Start()
    {
        Cooldown = shoot_delay;
        animation_index_max = weapons_sprites.Length-1;
        animation_index = 0;

        player_go = GameObject.FindGameObjectsWithTag("Player")[0];

        ammo_count = 20;
    }
    // Update is called once per frame
    public virtual void Update()
    {
        text.text = ammo_count.ToString();
        if (equipped)
        {
            shoot_cooldown();


            if (checkIfShoot() && animation_finished && ammo_count > 0)
            {
                if (Cooldown <= 0)
                {
                    shoot();
                    Cooldown = shoot_delay;
                }
            }


            if (play_animation_shoot)
            {
                animation_shoot();
            }
        }
    }

    public virtual void animation_shoot()
    {
        if(current_delay <= 0)
        {
            //change sprite

            if(animation_index == animation_index_max)
            {
                animation_finished = true;
                play_animation_shoot = false;
                animation_index = 0;
            }
            else
            {
                animation_index += 1;
                current_delay = delay;
            }
        }
        else
        {
            current_delay -= Time.deltaTime;
        }
    }

    public virtual void shoot()
    {
        play_animation_shoot = true;
        animation_finished = false;

        GameObject bullet = Instantiate(bullet_prefab, player_go.transform.position, player_go.GetComponent<PlayerController>().vert_rot_point.transform.rotation);
        bullet.transform.parent = null;
        audio_source.PlayOneShot(audio_clip);
        ammo_count -= 1;
    }

    public void shoot_cooldown()
    {
        if(Cooldown > 0)
        {
            Cooldown -= Time.deltaTime;
        }
    }

    public virtual bool checkIfShoot()
    {
        bool _is_shooting = Input.GetMouseButton(0);

        if (_is_shooting)
        {
            return true;
        }

        return false;
    }

    public void addAmmo(int amount)
    {
        if (unlocked)
        {
            ammo_count += amount;
        }
        else
        {
            player_go.GetComponent<WeaponManager>().AddWeapon(weapon_type);
        }
    }
}
