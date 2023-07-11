using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basictrap : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject _player;
    private float damage_timer = 1f;
    private float counter = 1f;

    public int amount;
    public bool ignore_armour;

    // Update is called once per frame
    void Update()
    {
        if( _player != null )
        {
            counter -= Time.deltaTime;

            if(counter < 0)
            {
                //hurt player 
                _player.GetComponent<PlayerController>().damagePlayer(amount, ignore_armour);
                counter = damage_timer;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerController>())
        {
            _player = other.gameObject;
            _player.GetComponent<PlayerController>().damagePlayer(amount, ignore_armour);
            counter = damage_timer;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            _player = null;
        }
    }
}
