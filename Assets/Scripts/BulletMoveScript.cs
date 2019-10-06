using UnityEngine;
using System.Collections;

public class BulletMoveScript : MonoBehaviour
{
    private float speed = 3f;
    private FadeInAndOut fadeInAndOut;
    private SoundManager sm;

    void Start()
    {
        fadeInAndOut = GameObject.Find("Fader").GetComponent<FadeInAndOut>();
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Update()
    {
        transform.Translate(this.transform.localScale.x * (speed * Time.deltaTime), 0 , 0);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("LevelWall"))
        {
            sm.PlaySound(sm.sounds[2]);
            this.gameObject.SetActive(false);
        }

        if (col.gameObject.CompareTag("Enemy") && this.gameObject.tag == "PlayerBullet")
        {
            sm.PlaySound(sm.sounds[5]);
            sm.PlaySound(sm.sounds[0]);
            this.gameObject.SetActive(false);
            col.gameObject.SetActive(false);
        }

        if (col.gameObject.CompareTag("Player") && this.gameObject.tag == "EnemyBullet")
        {
            sm.PlaySound(sm.sounds[6]);
            sm.PlaySound(sm.sounds[0]);
            this.gameObject.SetActive(false);
            col.gameObject.SetActive(false);
            fadeInAndOut.DoFade(4);
        }
    }
}