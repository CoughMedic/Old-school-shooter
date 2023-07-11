using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Base_AI_Script : MonoBehaviour
{
    public enum AI_Type
    {
        STILL = 0,
        PATROL = 1
    }

    [Header("Sprite settings")]
    public Sprite[] Movement_Sprites;
    public Sprite[] Attack_Sprites;
    public Sprite[] Death_Sprites;

    public Material damage_material;
    private Material defualt_material;

    private GameObject child_sprite;

    [Header("DetectionDistance")]
    public float spot_distance = 2f;

    public float sprite_Speed =0.5f;
    private float _sprite_speed_cooldown;
    private int _sprite_index = 0;
    public GameObject PlayerRef;

    [Header("AI Stats")]
    public float MovementModifier;
    public float attack_speed;
    public float attack_cooldown;
    public int health;
    public AI_Type behaviour_type;
    public Vector3[] patrol_points;
    public bool loop_patrol = true;
    private int current_point = 0;
    private bool reverse = false;

    [Header("AI State variables")]
    public bool _is_moving = true;
    public bool _is_dead = false;
    public bool _hasSpottedPlayer = false;

    public GameObject droppable_item;

    [HideInInspector] public SpriteRenderer _spriteRenderer;
    [HideInInspector] public Camera _playerCamera;

    // Start is called before the first frame update
    virtual public void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _sprite_speed_cooldown = sprite_Speed;
        _playerCamera = Camera.main;

        attack_cooldown = attack_speed;

        PlayerRef = FindObjectOfType<PlayerController>().gameObject;
        child_sprite = GetComponentInChildren<SpriteRenderer>().gameObject;
    }

    // Update is called once per frame
    virtual public void Update()
    {
        updateMovingSprite();
        DetectionCheck();
        facePlayer();
        updateBehaviour();

        if (_is_dead)
        {
            deathAnimation();
        }
    }

    void updateBehaviour()
    {
        if(behaviour_type == AI_Type.PATROL && !_hasSpottedPlayer)
        {
            Vector3 dir = patrol_points[current_point] - transform.position;
            dir.Normalize();
            transform.position = transform.position + new Vector3(dir.x, 0, dir.z) * Time.deltaTime * MovementModifier;

            Vector3 current_pos = transform.position;
            current_pos.y = 0;

            Vector3 target_pos = patrol_points[current_point];
            target_pos.y = 0;



            if (Vector3.Distance(current_pos, target_pos) < 1)
            {
                if (loop_patrol)
                {
                    if(current_point == patrol_points.Length - 1)
                    {
                        current_point = 0;
                    }
                    else
                    {
                        current_point++;
                    }
                }
                else
                {
                    if (!reverse)
                    {
                        current_point++;
                    }
                    else
                    {
                        current_point--;
                    }

                    if (current_point == -1)
                    {
                        current_point = 0;
                        reverse = false;
                    }
                    else if (current_point == patrol_points.Length)
                    {
                        current_point = patrol_points.Length - 1;
                        reverse = true;
                    }
                }
            }
        }
    }

    public void facePlayer()
    {

        transform.LookAt(_playerCamera.transform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    public void DetectionCheck()
    {
        float dist = Vector3.Distance(_playerCamera.transform.position, transform.position);

        if(dist < spot_distance)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.Normalize(PlayerRef.transform.position - transform.position), out hit, spot_distance))
            {
                Debug.DrawRay(transform.position, Vector3.Normalize(PlayerRef.transform.position - transform.position) * hit.distance, Color.gray, 1f);
                if(hit.collider.GetComponent<PlayerController>())
                    _hasSpottedPlayer = true;
            }
        }
    }

    void updateMovingSprite()
    {
        if(_is_moving && !_is_dead)
        {
            if(_sprite_speed_cooldown <= 0)
            {
                if (_sprite_index == Movement_Sprites.Length - 1)
                {
                    _sprite_index = 0;
                }
                else
                {
                    _sprite_index++;
                }

                _spriteRenderer.sprite = Movement_Sprites[_sprite_index];
                _sprite_speed_cooldown = sprite_Speed;
            }
            else
            {
                _sprite_speed_cooldown -= Time.deltaTime;
            }
        }
    }

    public void deathAnimation()
    {
        if(_sprite_speed_cooldown <= 0)
        {
            if(_sprite_index == Death_Sprites.Length-1)
            {
                if (droppable_item != null)
                {
                    var dropped_item = Instantiate<GameObject>(droppable_item, transform.position, transform.rotation);
                    droppable_item.transform.parent = null;
                }

                Destroy(this.gameObject);
            }

            _spriteRenderer.sprite = Death_Sprites[_sprite_index];
            _sprite_speed_cooldown = sprite_Speed;
            _sprite_index++;
        }
        else
        {
            _sprite_speed_cooldown -= Time.deltaTime;
        }
    }

    public void triggerDeath()
    {
        _is_dead = true;
        _sprite_speed_cooldown = sprite_Speed;
        _sprite_index = 0;
        sprite_Speed = 0.2f;


    }

    public void takeDamage(int value)
    {
        health -= value;
        StartCoroutine(hurtFlash());

        if (health <= 0 && !_is_dead)
        {
            triggerDeath();
        }
    }

    IEnumerator hurtFlash()
    {
        defualt_material = _spriteRenderer.material;
        _spriteRenderer.material = damage_material;

        yield return new WaitForSeconds(0.1f);

        _spriteRenderer.material = defualt_material;

        yield return null;
    }
}
