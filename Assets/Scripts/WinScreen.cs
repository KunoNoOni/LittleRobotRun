using UnityEngine;

public class WinScreen : MonoBehaviour
{
    private FadeInAndOut fadeInAndOut;

    MusicManager mm;

    void Awake()
    {
        mm = GameObject.Find("MusicManager").GetComponent<MusicManager>();
    }

    private void Start()
    {
        if (mm.music.Length > 0)
        {
            mm.PlaySound(mm.music[3]);
        }

        fadeInAndOut = GameObject.Find("Fader").GetComponent<FadeInAndOut>();
    }

    public void BackToTitle()
    {
        fadeInAndOut.DoFade(0);
    }
}
