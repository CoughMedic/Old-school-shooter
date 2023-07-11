using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_move_script : MonoBehaviour
{

    public Vector3[] movement_points;
    private int movement_index = 1;

    public void Move()
    {
        if (movement_index != movement_points.Length)
        {
            if (movement_points[movement_index] == transform.localPosition)
            {
                movement_index += 1;
            }

            Vector3 dir = movement_points[movement_index] - transform.localPosition;

            transform.localPosition = transform.localPosition + (dir * 4 * Time.deltaTime);

        }
    }
}
