using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private AudioSource _audioSource;
    private bool _underWater;
    private float _inHereTimer;
    private PlayerController playerController;

    public bool isExit;
    public bool playerInHere;
    public List<GameObject> BeeperGameObjects;
    public float timeTillRoomFilled;

    void Awake()
    {
        InitializeAudioSource();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }


    void Update()
    {
        if (playerInHere && isExit && playerController.Playing)
        {

            playerController.Playing = false;
            AudioManager.Instance.Play(playerController.walking, "KlimNaarBuiten", false);
        }


        if (playerInHere && _underWater)
        {
            ForLongerThanXSeconds(timeTillRoomFilled);
        }
        else
        {
            _inHereTimer = 0.0f;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        playerInHere = true;

        SetPlayerVoetstapSound(col);
        SetDoorBleeps();


    }

    void OnCollisionExit(Collision col)
    {
        playerInHere = false;
    }





    private void SetPlayerVoetstapSound(Collision col)
    {
        AudioSource playerAudioSource = col.gameObject.GetComponent<PlayerController>().walking;

        //Check whether we have water or not
        if (_underWater)
        {
            //If yes set players sound to WaterVoetstappen
            AudioManager.Instance.Play(playerAudioSource, "WaterVoetstappen", true);
        }
        else
        {
            //Otherwise set it to MetaalVoetstappen
            AudioManager.Instance.Play(playerAudioSource, "MetaalVoetstappen", true);
        }
    }
    private void InitializeAudioSource()
    {
        //Initialize audioSource
        GameObject parent = transform.parent.gameObject;
        foreach (AudioSource adios in parent.GetComponentsInChildren<AudioSource>())
        {
            if (adios.gameObject.tag == "Hole")
            {
                _audioSource = adios;
            }
        }

    }

    private void ForLongerThanXSeconds(float x)
    {
        _inHereTimer += Time.deltaTime;
        if (_inHereTimer > x)
        {
            playerController.Playing = false;
            AudioManager.Instance.Play(playerController.walking, "IkVerdrink");
            _inHereTimer = 0;
        }
    }

    public void SetDoorBleeps()
    {
        int beeps = BeeperGameObjects.Count;
        foreach (GameObject g_Object in BeeperGameObjects)
        {
            AudioSource adios = g_Object.GetComponent<AudioSource>();

            AudioManager.Instance.Play(adios, "Beep", true, true);
        }
    }
    public void startWaterFlow()
    {
        //Hack: uses bypassEffects to remember rooms that have been silenced
        if (!_audioSource.bypassEffects)
        {
            AudioManager.Instance.Play(_audioSource, "WaterStroomtUitGat", true);
            _underWater = true;

            AudioSource playerAudioSource = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>().walking;
            //If yes set players sound to WaterVoetstappen
            AudioManager.Instance.Play(playerAudioSource, "WaterVoetstappen", true);
        }
    }
    public void stopWaterFlow()
    {
        _audioSource.Stop();
    }
}
