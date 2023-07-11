using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class base_bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float start_velocity = 30;
    public float life_span = 1.0f;


    // Update is called once per frame
    void Update()
    {
        Vector3 for_vec = transform.forward * (Time.deltaTime * start_velocity);

        transform.position = new Vector3(for_vec.x + transform.position.x,
                                         for_vec.y + transform.position.y,
                                         for_vec.z + transform.position.z);

        if(life_span < 0)
        {
            Destroy(this.gameObject.gameObject);
        }

        life_span -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Base_AI_Script>() != null)
        {
            other.GetComponent<Base_AI_Script>().takeDamage(1);
            Destroy(this.gameObject.gameObject);
        }
    }
}
