using UnityEngine;
using UnityEngine.EventSystems;



public class TitleMenu : MonoBehaviour
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
            mm.PlaySound(mm.music[0]);
        }
        
        fadeInAndOut = GameObject.Find("Fader").GetComponent<FadeInAndOut>();
    }

    public void PlayGameButton()
    {
        fadeInAndOut.DoFade(2);
    }

    public void InstructionsButton()
    {
        fadeInAndOut.DoFade(1);
    }

    public void CreditsButton()
    {
        fadeInAndOut.DoFade(5);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
