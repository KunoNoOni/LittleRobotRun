using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject BulletSpawn;

    private ObjectPooler objectPooler;
    private SoundManager sm;
    private float fireCooldown;
    private float fireCooldownReset;
    private bool canFire = false;


    void Start()
    {
        objectPooler = GameObject.Find("EnemyBullets").GetComponent<ObjectPooler>();
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        fireCooldownReset = 2.5f;
        fireCooldown = fireCooldownReset;
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, this.transform.localScale.x * Vector2.right,Mathf.Infinity,Physics2D.DefaultRaycastLayers,0,0);


        if (hit.collider != null && hit.collider.CompareTag("Player") && canFire)
        {
            FireTurret();
            canFire = false;
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
