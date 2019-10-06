using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerWeapon: MonoBehaviour
{
    public GameObject PlayerWeaponModel;
    public GameObject BulletSpawn;

    private float speed = 3f;
    private float maxHeight = 6f;
    private float fireCooldown;
    private float fireCooldownReset;
    private bool grounded = true;
    private bool isJumping = false;
    private bool pressingDown = false;
    private bool hasSpring = true;
    private bool hasDuck = true;
    private bool hasWeapon = true;
    private bool hasRedKey = false;
    private bool hasBlueKey = false;
    private bool hasYellowKey = false;
    private bool hasFired = false;
    private float moveVelocity;
    private float vertical;
    private Rigidbody2D player;
    private Animator anim;
    private ObjectPooler objectPooler;
    private SoundManager sm;
    private FadeInAndOut fadeInAndOut;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        objectPooler = GameObject.Find("PlayerBullets").GetComponent<ObjectPooler>();
        //sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        fadeInAndOut = GameObject.Find("Fader").GetComponent<FadeInAndOut>();
        fireCooldownReset = 3f;
        fireCooldown = fireCooldownReset;
    }

    // Update is called once per frame
    void Update()
    {
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
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (player.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
            

        if (grounded && !pressingDown && hasSpring && (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1")))
        {
            //sm.PlaySound(sm.sounds[2]);
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
            Debug.Log("FIRING!");
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

        if (col.gameObject.CompareTag("RedKey"))
        {
            //play pickup sound here
            col.gameObject.SetActive(false);
            hasRedKey = true;
        }

        if (col.gameObject.CompareTag("BlueKey"))
        {
            //play pickup sound here
            col.gameObject.SetActive(false);
            hasBlueKey = true;
        }

        if (col.gameObject.CompareTag("YellowKey"))
        {
            //play pickup sound here
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
            col.gameObject.SetActive(false);
            fadeInAndOut.DoFade(3);
        }
    }
}
