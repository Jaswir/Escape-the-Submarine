using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private AudioSource _audioSource;
    private bool _underWater;
    [SerializeField]
    private float _underWaterTimer;
    private PlayerController playerController;
    public float timeTillRoomFilled = 10;
    private bool forcedStop;
    private float _inHereTimer;


    public bool isExit;
    public bool playerInHere;
    public List<GameObject> BeeperGameObjects;


    void Awake()
    {
        InitializeAudioSource();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }


    void Update()
    {
        if (playerInHere && isExit)
        {
            ForLongerThanXSecondHere(2);
        }

        #region Drowning
        if (!GameManager.Instance.capturedFlag)
        {
            if (playerInHere && _underWater)
            {
                ForLongerThanXSecondsUnderWater(timeTillRoomFilled);
            }
            else
            {
                _underWaterTimer = 0.0f;
            }
        }

        #endregion

        if (GameManager.Instance.IsGameOver() && !forcedStop)
        {
            DisableNuisanceSounds();
            forcedStop = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        playerInHere = true;

        if (!GameManager.Instance.IsGameOver())
        {
            SetPlayerVoetstapSound(col);
            SetDoorBleeps();
        }


    }
    void OnCollisionExit(Collision col)
    {
        playerInHere = false;
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
    private void DisableAllSounds()
    {
        DisableNuisanceSounds();
        DisableAtmosphereSounds();
    }
    private void DisableAtmosphereSounds()
    {
        Camera.main.GetComponent<AudioSource>().Stop();
    }
    private void DisableNuisanceSounds()
    {
        DisableBeeps();
        stopWaterFlow();
    }
    private void SetPlayerVoetstapSound(Collision col)
    {
        AudioSource playerAudioSource = col.gameObject.GetComponent<PlayerController>().walking;

        //Check whether we have water or not
        if (_underWater)
        {
            //If yes set players sound to WaterVoetstappen
            AudioManager.Instance.Play(playerAudioSource, "WaterVoetstappenSnel", true);
        }
        else
        {
            //Otherwise set it to MetaalVoetstappen
            AudioManager.Instance.Play(playerAudioSource, "MetaalVoetstappenSnel", true);
        }
    }
    private void DisableBeeps()
    {
        int beeps = BeeperGameObjects.Count;
        foreach (GameObject g_Object in BeeperGameObjects)
        {
            AudioSource adios = g_Object.GetComponent<AudioSource>();
            adios.Stop();

        }
    }
    private void ForLongerThanXSecondHere(float x)
    {
        _inHereTimer += Time.deltaTime;
        if (_inHereTimer > x && !GameManager.Instance.capturedFlag)
        {
            GameManager.Instance.capturedFlag = true;
            AudioManager.Instance.Play(playerController.walking, "KlimNaarBuiten", false);
            GameManager.Instance.Win(playerController.walking.clip.length);


        }
    }
    private void ForLongerThanXSecondsUnderWater(float x)
    {
        _underWaterTimer += Time.deltaTime;
        if (_underWaterTimer > x && !GameManager.Instance.drown)
        {
            GameManager.Instance.drown = true;
            AudioManager.Instance.Play(playerController.walking, "IkVerdrink");
            GameManager.Instance.Reset(playerController.walking.clip.length);

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
        if (!_audioSource.bypassEffects && !GameManager.Instance.drown && !GameManager.Instance.capturedFlag)
        {
            AudioManager.Instance.Play(_audioSource, "WaterStroomtUitGat", true);
            _underWater = true;
        }
    }
    public void stopWaterFlow()
    {
        _audioSource.Stop();
    }
}
