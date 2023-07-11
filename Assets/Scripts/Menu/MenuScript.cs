using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{

    public GameObject level_select_ref;
    public GameObject main_menu_ref;

    // Start is called before the first frame update
    void Start()
    {
        level_select_ref.SetActive(false);
        main_menu_ref.SetActive(true);
    }
}
