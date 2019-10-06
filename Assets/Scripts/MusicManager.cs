using UnityEngine;

public class MusicManager : MonoBehaviour 
{
    //This class will manage all of the music in the game. Just need to add
    //an empty gameobject to hold this script. 1 AudioSource will be added 
    //automatically to the gameobject.
	//
    //On the script that will play the music you want just copy the following
    //and paste at the top of the script where your variables are at.
    //
    //MusicManager mm;
    //
    //void Awake() 
    //{
    //    mm = GameObject.Find("MusicManager").GetComponent<MusicManager>();
    //}
    //
    //To play the music you want just use the following line:
    //
    //mm.PlaySound(mm.music[0]);
    //
    //Just replace the 0 with the correct number from the array for the music
    //you want.


    //variable that holds all of the music in the game
    public AudioClip[] music;

    //variable that we will use to play the music in the game from
    public AudioSource channel;

    //variable for the instance of SoundManager
    public static MusicManager Instance;

    //Here is where we grab a reference to the AudioSource
    void Awake()
    {
        this.InstantiateManager();

        this.gameObject.AddComponent<AudioSource>();
	
        channel = GetComponent<AudioSource>();
        channel.loop = true;
        channel.volume = .5f;

    }

    //This insures there is only one instance of the MusicManager in the scene
    //and it persist changing scenes.
    void InstantiateManager()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != Instance)
        {
            Destroy(this.gameObject);
        }
    }
    
    //Function that allows us to play the music in the game
    public void PlaySound(AudioClip aClip)
    {
        channel.clip = aClip;
        channel.Play();
    }

    //Function that allows us to set the volume of the music 
    public void SetMusicVolume(float volume)
    {
        channel.volume = volume;
    }

    //Function that allows us to mute/unmute the music in the game
    public void MuteMusic(bool mute)
    {
        channel.mute = mute;
    }

    public bool IsPlaying()
    {
        return channel.isPlaying;
    }
}
