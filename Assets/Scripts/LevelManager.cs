using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    MusicManager mm;

    void Awake()
    {
        mm = GameObject.Find("MusicManager").GetComponent<MusicManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
       mm.PlaySound(mm.music[1]);
    }
}
