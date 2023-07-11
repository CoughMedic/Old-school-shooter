using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard_script : MonoBehaviour
{

    private Camera _camera;
    public enum CardType
    {
        BLUE = 0,
        GREEN = 1,
        RED = 2,
    }
    [Header("Card Type")]
    public CardType card_colour;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_camera.transform, Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerController>() != null)
        {
            //add key card to player

            var card = other.gameObject.AddComponent<Player_card_script>();
            card.card_colour = card_colour;

            Destroy(this.gameObject);
        }
    }
}
