using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Header("Movement system")]
    public float movement_multiplier = 0;
    public GameObject rot_point;

    public float move_bob_min = -1;
    public float move_bob_max = 1;
    public float move_bob_speed = 0.1f;
    private float current_move_bob = 0;
    private bool bob_up = true;
    private Vector3 camera_default_pos;

    [Header("PlayerStats")]
    public TextMeshProUGUI health;
    public TextMeshProUGUI armour;

    private int _health = 100;
    private int _armour = 100;

    private Camera player_camera;

    private Vignette PlayerVig;
    public Volume PlayerCamVolume;

    private float DamageVigCooldown = 5.0f;

    [Header("Death Settings")]
    public Image death_text;
    public Image win_text;
    public SpriteRenderer FadeToBlack;
    private bool _fadeToBlack = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player_camera = GetComponentInChildren<Camera>();
        camera_default_pos = player_camera.transform.localPosition;

        Vignette tmp;
        if(PlayerCamVolume.profile.TryGet<Vignette>(out tmp))
        {
            PlayerVig = tmp;
        }

        death_text.gameObject.SetActive(false);
        win_text.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        updateMovement();
        updateRotation();
        CheckInteract();
        VigenetteCooldown();
        if(UpdateFadeToBlack())
        {
            SceneManager.LoadScene("Scenes/MenuScene");
            Cursor.lockState = CursorLockMode.None;
        }

        health.text = _health.ToString();
        armour.text = _armour.ToString();
    }

    public void StartFadeToBlack()
    {
        _fadeToBlack = true;

    }

    private bool UpdateFadeToBlack()
    {
        if (_fadeToBlack == true)
        {
            Color fade = FadeToBlack.material.color;
            fade.a += 1 * Time.deltaTime;

            FadeToBlack.material.color = fade;
            print(FadeToBlack.material.color.a);

            if (FadeToBlack.material.color.a > 2)
            {
                return true;
            }
        }
        return false;
    }

    private void VigenetteCooldown()
    {
        if (PlayerVig.intensity.value != 0)
        {
            if (DamageVigCooldown > 0)
            {
                DamageVigCooldown -= Time.deltaTime;
            }
            else
            {
                //reduce vig gradually
                DamageVigCooldown = 0.5f;
                PlayerVig.intensity.value = PlayerVig.intensity.value - 0.1f;
            }
        }
    }

    void CheckInteract()
    {
        bool interact_now = Input.GetButtonDown("Interact");

        if(interact_now)
        {
            LayerMask layer_to_hit = 1 << 6;
            RaycastHit hit;
            if(Physics.Raycast(rot_point.transform.position, rot_point.transform.forward, out hit, 150, layer_to_hit))
            {
                hit.transform.GetComponent<Interactables>().interact(this.gameObject);
            }

            Debug.DrawLine(rot_point.transform.position, rot_point.transform.position + (rot_point.transform.forward * 100), Color.white, 1000);
        }
    }

    void updateRotation()
    {
        float horz_mouse = Input.GetAxis("Mouse X");

        if (horz_mouse != 0)
        {
            if(horz_mouse > 0.1)
            {
                Vector3 look_direction = rot_point.transform.up * Time.deltaTime * (movement_multiplier * 20);
                rot_point.transform.Rotate(look_direction);
            }

            if (horz_mouse < -0.1)
            {
                Vector3 look_direction = -rot_point.transform.up * Time.deltaTime * (movement_multiplier * 20);
                rot_point.transform.Rotate(look_direction);
            }
        }
    }

    void updateMovement()
    {
        float horz_movement_input = Input.GetAxis("Horizontal");
        float vert_movement_input = Input.GetAxis("Vertical");

        //update sideways movement "left/right"
        if (horz_movement_input != 0)
        {
            if (horz_movement_input > 0.1)
            {
                Vector3 look_direction = rot_point.transform.right * Time.deltaTime * movement_multiplier;
                transform.Translate(look_direction);
            }

            if (horz_movement_input < -0.1)
            {
                Vector3 look_direction = -rot_point.transform.right * Time.deltaTime * movement_multiplier;
                transform.Translate(look_direction);
            }
        }

        //update verticle movement "forward/back"
        if (vert_movement_input != 0)
        {
            //manage forward input
            if (vert_movement_input > 0.1)
            {
                Vector3 look_direction = rot_point.transform.forward * Time.deltaTime * movement_multiplier;
                transform.Translate(look_direction);
            }

            //manage backwards input
            if (vert_movement_input < -0.1)
            {
                Vector3 look_direction = -rot_point.transform.forward * Time.deltaTime * movement_multiplier;
                transform.Translate(look_direction);
            }

            if(bob_up)
            {
                current_move_bob += move_bob_speed * Time.deltaTime;

                if(current_move_bob > move_bob_max)
                {
                    bob_up = false;
                }
            }
            else
            {
                current_move_bob -= move_bob_speed * Time.deltaTime;

                if (current_move_bob < move_bob_min)
                {
                    bob_up = true;
                }
            }

            Vector3 player_cam_pos = player_camera.transform.localPosition;

            player_cam_pos.y = camera_default_pos.y + current_move_bob;

            player_camera.transform.localPosition = player_cam_pos;
        }

    }

    public void damagePlayer(int damage, bool ignore_armour)
    {
        if(ignore_armour)
        {
            if(_health - damage > 0)
            {
                _health -= damage;
                if (PlayerVig.intensity.value < 0.4)
                {
                    PlayerVig.intensity.value = PlayerVig.intensity.value + 0.1f;
                    DamageVigCooldown = 5.0f;
                }
            }
            else
            {
                _health = 0;
            }
        }
        else
        {
            if(_armour - damage > 0)
            {
                _armour -= damage;
            }
            else
            {
                damage = damage - _armour;
                _armour = 0;
                _health -= damage;

                if (PlayerVig.intensity.value < 0.4)
                {
                    PlayerVig.intensity.value = PlayerVig.intensity.value + 0.1f;
                    DamageVigCooldown = 5.0f;
                }

            }
            
        }

        if (_health < 0)
            _health = 0;
        if (_armour < 0)
            _armour = 0;

        //dead
        if(_health == 0)
        {
            StartFadeToBlack();
            death_text.gameObject.SetActive(true);
        }

    }

    public void healPlayer(int health, int armour)
    {
        _health += health; _armour += armour;
    }
}
