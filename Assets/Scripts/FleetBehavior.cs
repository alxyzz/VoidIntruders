using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetBehavior : MonoBehaviour
{
    GameObject Player;
    public GameObject IntroScreen;
    public GameObject DefeatScreen;
    private AudioSource[] allAudioSources;
    public GameObject Winner;
    AudioSource PlayerSource;
    public GameObject EnemyPrefab;
    public AudioClip ArabicNokiaRingtone;
    Vector3 InitialPos;

    public float MaximumDepthBeforeLosing;

    public float waveWidth;
    public float waveHeight;
    public float BoundaryX;
    public float BoundaryY;
    int wave = 1;
    bool running = false;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(Intro());



        Player = GameObject.Find("Player");
        PlayerSource = Player.GetComponent<AudioSource>();
        InitialPos = transform.position;
        
    }

    IEnumerator Intro()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        IntroScreen.SetActive(false);
        running = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            if (transform.childCount == 0)
            {
                Victory();
                running = false;
                Debug.Log("WINNER");
            }
            if (transform.position.y < MaximumDepthBeforeLosing)
            {
                Defeat();
                running = false;
                Debug.Log("LOSER");
            }
            FleetMove();
        }
    }
    public float yAccel;
    public float xAccel;

    bool dir = true; //true is right, false is left
    void FleetMove()
    {

        if (transform.position.x > BoundaryX || transform.position.x < (BoundaryX * -1))
        {
            dir = !dir;
            transform.position = new Vector3(transform.position.x, transform.position.y + yAccel, transform.position.z);
        }


        if (!dir)
        {
            xAccel = (xAccel * -1);
        }
        Vector3 move = new Vector3(transform.position.x + xAccel, transform.position.y, transform.position.z);
        transform.position = move;
    }

    void Victory()
    {
        StopAllSounds();
        Winner.SetActive(true);
        PlayerSource.PlayOneShot(ArabicNokiaRingtone);
    }


    void Defeat()
    {
        StopAllSounds();
        DefeatScreen.SetActive(true);
        PlayerSource.PlayOneShot(ArabicNokiaRingtone);

    }


    void StopAllSounds()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }


}
