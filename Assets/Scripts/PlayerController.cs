using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject BulletSpawn;

    private float speed = 3f;
    private float maxHeight = 6f;
    private float fireCooldown;
    private float fireCooldownReset;
    [SerializeField]
    private bool grounded = true;
    [SerializeField]
    private bool isJumping = false;
    [SerializeField]
    private bool pressingDown = false;
    [SerializeField]
    private bool hasSpring = false;
    [SerializeField]
    private bool hasDuck = false;
    [SerializeField]
    private bool hasWeapon = false;
    [SerializeField]
    private bool hasFired = false;
    [SerializeField]
    private bool hasRedKey = false;
    [SerializeField]
    private bool hasBlueKey = false;
    [SerializeField]
    private bool hasYellowKey = false;
    private float moveVelocity;
    private float vertical;
    private Rigidbody2D player;
    private Animator anim;
    private ObjectPooler objectPooler;
    private SoundManager sm;
    private FadeInAndOut fadeInAndOut;
  

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        objectPooler = GameObject.Find("PlayerBullets").GetComponent<ObjectPooler>();
        fadeInAndOut = GameObject.Find("Fader").GetComponent<FadeInAndOut>();
        fireCooldownReset = 3f;
        fireCooldown = fireCooldownReset;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            fadeInAndOut.DoFade(2);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            hasSpring = true;
        }

        if (player.velocity.y != 0)
        {
            grounded = false;
        }

        anim.SetBool("isGrounded", grounded);
        anim.SetBool("isJumping", isJumping);

        if (!pressingDown)
        {
            moveVelocity = speed * Input.GetAxisRaw("Horizontal");
            player.velocity = new Vector2(moveVelocity, player.velocity.y);
        }

        if (hasDuck)
        {
            vertical = Input.GetAxisRaw("Vertical");
            pressingDown = vertical < 0 ? true : false;
            anim.SetBool("pressingDown", pressingDown);
        }
        
        if (hasDuck && pressingDown)
        {
            this.gameObject.layer = 4;
        } 
        else if (hasDuck && !pressingDown)
        {
            this.gameObject.layer = 0;
        }

        if (player.velocity.x > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (player.velocity.x < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        if (grounded && !pressingDown && hasSpring && (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1")))
        {
            sm.PlaySound(sm.sounds[7]);
            player.velocity = new Vector2(player.velocity.x, maxHeight);
            grounded = false;
            isJumping = true;
        }

        if (hasFired)
        {
            fireCooldown -= Time.deltaTime;
        }

        if (fireCooldown < 0)
        {
            hasFired = false;
        }

        if (hasWeapon && !hasFired && !pressingDown && (Input.GetKeyDown(KeyCode.RightControl) || Input.GetButtonDown("Fire2")))
        {
            sm.PlaySound(sm.sounds[1]);
            fireCooldown = fireCooldownReset;
            hasFired = true;
            GameObject obj = objectPooler.GetPooledObject();
            obj.transform.position = BulletSpawn.transform.position;
            obj.transform.rotation = BulletSpawn.transform.rotation;
            obj.transform.localScale = new Vector2(this.transform.localScale.x, 1);
            obj.gameObject.SetActive(true);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform") && player.velocity.y == 0)
        {
            grounded = true;
            isJumping = false;
        }

        if (col.gameObject.CompareTag("Enemy"))
        {
            sm.PlaySound(sm.sounds[5]);
            this.gameObject.SetActive(false);
            col.gameObject.SetActive(false);
            fadeInAndOut.DoFade(4);
        }

        if (col.gameObject.CompareTag("FloatingEnemy") && !pressingDown)
        {
            this.gameObject.SetActive(false);
            col.gameObject.SetActive(false);
            fadeInAndOut.DoFade(4);
        }

        if (col.gameObject.CompareTag("Duck"))
        {
            sm.PlaySound(sm.sounds[9]);
            col.gameObject.SetActive(false);
            hasDuck = true;
        }

        if (col.gameObject.CompareTag("Spring"))
        {
            sm.PlaySound(sm.sounds[9]);
            col.gameObject.SetActive(false);
            hasSpring = true;
        }

        if (col.gameObject.CompareTag("Weapon"))
        {
            sm.PlaySound(sm.sounds[9]);
            col.gameObject.SetActive(false);
            hasWeapon = true;
        }

        if (col.gameObject.CompareTag("RedKey"))
        {
            sm.PlaySound(sm.sounds[9]);
            col.gameObject.SetActive(false);
            hasRedKey = true;
        }

        if (col.gameObject.CompareTag("BlueKey"))
        {
            sm.PlaySound(sm.sounds[9]);
            col.gameObject.SetActive(false);
            hasBlueKey = true;
        }

        if (col.gameObject.CompareTag("YellowKey"))
        {
            sm.PlaySound(sm.sounds[9]);
            col.gameObject.SetActive(false);
            hasYellowKey = true;
        }

        if (col.gameObject.CompareTag("RedDoor") && hasRedKey)
        {
            col.gameObject.SetActive(false);
        }

        if (col.gameObject.CompareTag("BlueDoor") && hasBlueKey)
        {
            col.gameObject.SetActive(false);
        }

        if (col.gameObject.CompareTag("YellowDoor") && hasYellowKey)
        {
            col.gameObject.SetActive(false);
        }

        if (col.gameObject.CompareTag("Portal"))
        {
            sm.PlaySound(sm.sounds[8]);
            this.gameObject.SetActive(false);
            fadeInAndOut.DoFade(3);
        }
    }
}
