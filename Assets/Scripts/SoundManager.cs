using UnityEngine;

public class SoundManager : MonoBehaviour 
{
    //This class will allow you to have 8 sounds all playing at the same time
    //You will need a empty game object to place the script on.
    //8 AudioSources will automatically be added to the gameobject.
    //
    //On the script that will play the sound you want just copy the following
    //and paste at the top of the script where your variables are at.
    //
    //SoundManager sm;
    //
    //void Awake() 
    //{
    //    sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    //}
    //
    //To play the sound you want just use the following line:
    //
    //sm.PlaySound(sm.sounds[0]); 
    //
    //Just replace the 0 with the correct number from the array for the sound
    //you want.


    //variable to hold all of the sounds in the game
    public AudioClip[] sounds;

    //variable for the 8 AudioSources
    public AudioSource[] channels;

    //variable for the instance of SoundManager
    public static SoundManager Instance;

    //Here is where we grab a reference to the AudioSource
	void Awake()
	{
        this.InstantiateManager();

		for (int i = 0; i < 8; i++)
        {
            this.gameObject.AddComponent<AudioSource>().playOnAwake = false;
        }
	
        channels = GetComponents<AudioSource>();
        SetSoundVolume(.5f);
	}

    //This insures there is only one instance of the SoundManager in the scene
    //and it persist changing scenes.
    void InstantiateManager()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        } 
        else if(this != Instance)
        {
            Destroy(this.gameObject);
        }
    }

    //Function that allows us to play any one of the sounds in the sounds
    //array
    public void PlaySound(AudioClip aClip)
    {
        foreach(AudioSource channel in channels)
        {
            if(!channel.isPlaying)
            {
                channel.clip = aClip;
                channel.Play();
                return;
            }
        }
    }

    //Function that allows us to set the volume of all of the sound channels
    public void SetSoundVolume(float volume)
    {
        foreach(AudioSource channel in channels)
        {
            channel.volume = volume;
        }
    }

    //Function that allows us to mute/unmute all of the sounds channels
    public void MuteSounds(bool mute)
    {
        foreach(AudioSource channel in channels)
        {
            channel.mute = mute;  
        }
    }
}
