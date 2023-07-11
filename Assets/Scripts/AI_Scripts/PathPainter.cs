using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]

public class PathPainter : MonoBehaviour
{



    public Base_AI_Script ai_script;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < ai_script.patrol_points.Length - 1; i++)
        {
            if (i != ai_script.patrol_points.Length - 1)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(ai_script.patrol_points[i], ai_script.patrol_points[i + 1]);
            }
        }
    }
}
