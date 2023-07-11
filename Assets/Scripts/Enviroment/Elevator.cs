using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    private bool _activate = false;

    public Vector3 end_pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( _activate )
        {
            Vector3 move_pos =Vector3.Normalize(end_pos - transform.localPosition) ;

            transform.localPosition = transform.localPosition + (move_pos * Time.deltaTime);

            if (transform.localPosition == end_pos)
            {
                _activate = false;
            }
        }
    }

    public void startElevator()
    {
        _activate = true;
    }
}
