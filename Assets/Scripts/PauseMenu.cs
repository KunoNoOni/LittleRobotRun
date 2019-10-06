using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour 
{
    private MusicManager mm;
    private SoundManager sm;
    public Slider musicSlider;
    public Slider soundSlider;

    private void Awake()
    {
        mm = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    private void OnEnable()
    {
        musicSlider.value = mm.channel.volume;
        soundSlider.value = sm.channels[0].volume;
    }

    public void SetMusicVolume()
    {
        mm.SetMusicVolume(musicSlider.value);
    }

    public void SetSoundVolume()
    {
        sm.SetSoundVolume(soundSlider.value);
    }
}
