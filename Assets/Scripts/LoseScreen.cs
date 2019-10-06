using UnityEngine;

public class LoseScreen : MonoBehaviour 
{
    private FadeInAndOut fadeInAndOut;

    private MusicManager mm;

    void Awake()
    {
        mm = GameObject.Find("MusicManager").GetComponent<MusicManager>();
    }

    private void Start()
    {
        if (mm.music.Length > 0)
        {
            mm.PlaySound(mm.music[2]);
        }

        fadeInAndOut = GameObject.Find("Fader").GetComponent<FadeInAndOut>();
    }

    public void BackToTitle()
    {
        fadeInAndOut.DoFade(0);
    }
}
