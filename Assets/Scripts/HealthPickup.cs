using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public PlayerController controller;

    public int armour_value;
    public int health_falue;

    private void Update()
    {
        facePlayer();
    }

    private void Start()
    {
        controller = Camera.main.GetComponentInParent<PlayerController>();
    }

    public void facePlayer()
    {

        transform.LookAt(controller.transform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerController>())
        {
            controller.healPlayer(health_falue, armour_value);
            Destroy(this.gameObject);
        }
    }
}
