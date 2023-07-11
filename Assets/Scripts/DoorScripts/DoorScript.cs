using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : Interactables
{
    bool open = false;
    public Keycard_script.CardType door_type;

    private Door_move_script[] doors;

    // Start is called before the first frame update
    void Start()
    {
        doors = GetComponentsInChildren<Door_move_script>();
    }

    // Update is called once per frame
    void Update()
    {
        if(open)
        {
            for(int i = 0; i < doors.Length; i++)
            {
                doors[i].Move();
            }
        }
    }

    override public void interact(GameObject _playerCharacter)
    {
        var _keyCards = _playerCharacter.GetComponents<Player_card_script>();
        if (_keyCards.Length > 0)
        {
            for (int i = _keyCards.Length-1; i >= 0; i --)
            {
                print("Interact");
                //checks each keycard for correct one

                if (_keyCards[i].card_colour == door_type)
                {
                    open = true;
                }
            }
        }
    }
}
