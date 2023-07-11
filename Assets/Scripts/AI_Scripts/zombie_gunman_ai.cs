using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class zombie_gunman_ai : Base_AI_Script
{

    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    override public void Update()
    {
        base.Update();

        if(_hasSpottedPlayer && !_is_dead)
        {
            attackPlayer();
        }
    }

    void attackPlayer()
    {
        float dist = Vector3.Distance(Camera.main.transform.position, transform.position);

        if (dist > 15f)
        {
            _is_moving = true;
            var direction = Camera.main.transform.position - transform.position;
            direction.Normalize();
            transform.position = transform.position + (direction * MovementModifier);
        }
        else
        {
            _is_moving = false;
            if (attack_cooldown > 0 && attack_cooldown < attack_speed)
            {
                _spriteRenderer.sprite = Attack_Sprites[0];
            }
            else if(attack_cooldown <= 0)
            {
                _spriteRenderer.sprite = Attack_Sprites[1];
                attack_cooldown = attack_speed + 0.1f;
                DamagePlayer();
            }

            attack_cooldown -= Time.deltaTime;
        }

    }

    private void DamagePlayer()
    {
        PlayerRef.GetComponent<PlayerController>().damagePlayer(10, false);
    }
}
