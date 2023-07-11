using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Button : Interactables
{
    public Elevator Elevator_to_use;

    private bool lower_elevator = false;

    public override void interact(GameObject _playerCharacter)
    {
        base.interact(_playerCharacter);

        //lower elevator
        Elevator_to_use.startElevator();
    }
}
