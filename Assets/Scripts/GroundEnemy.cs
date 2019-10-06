using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    public GameObject BulletSpawn;

    private ObjectPooler objectPooler;
    private SoundManager sm;
    private Animator anim;
    private float fireCooldown;
    private float fireCooldownReset;
    private bool canFire = false;

    void Start()
    {
        objectPooler = GameObject.Find("EnemyBullets").GetComponent<ObjectPooler>();
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        fireCooldownReset = 1.5f;
        fireCooldown = fireCooldownReset;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        fireCooldown -= Time.deltaTime;
        if (fireCooldown < 0)
        {
            canFire = true;
        }
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, this.transform.localScale.x * Vector2.right, Mathf.Infinity, Physics2D.DefaultRaycastLayers, 0, 0);

        if (hit.collider != null && hit.collider.CompareTag("Player") && canFire)
        {
            anim.SetBool("isFiring", canFire);
            FireTurret();
            canFire = false;
            anim.SetBool("isFiring", canFire);
            fireCooldown = fireCooldownReset;
        }
    }

    private void FireTurret()
    {
        sm.PlaySound(sm.sounds[1]);
        GameObject obj = objectPooler.GetPooledObject();
        obj.transform.position = BulletSpawn.transform.position;
        obj.transform.rotation = BulletSpawn.transform.rotation;
        obj.transform.localScale = new Vector2(this.transform.localScale.x, 1);
        obj.gameObject.SetActive(true);
    }
}
