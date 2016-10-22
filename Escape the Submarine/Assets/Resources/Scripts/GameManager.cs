using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Alarm;
    public RoomChain RoomChain;
    public Canvas Canvas;

    public bool drown;
    public bool capturedFlag;

    private bool soundActive = true;


    // Use this for initialization
    void Awake()
    {
        ApplySingleton();
    }


    void WinMessage()
    {
        Text messageText = Canvas.GetComponentInChildren<Text>();
        messageText.text = "";
    }


    void LoseMessage()
    {
        Text messageText = Canvas.GetComponentInChildren<Text>();
        messageText.text =
            "";
        SceneManager.LoadScene("Escape the Submarine");
    }

    /// <summary>
    /// Resets the application in time seconds
    /// </summary>
    /// <param name="time"></param>
    public void Reset(float time)
    {
        StartCoroutine(Reset_Coroutine(time));
    }

    IEnumerator Reset_Coroutine(float time)
    {
        yield return new WaitForSeconds(time);
        LoseMessage();
        //SceneManager.LoadScene(0);

    }

    public void Win(float time)
    {
        StartCoroutine(Win_Coroutine(time));
    }

    IEnumerator Win_Coroutine(float time)
    {
        yield return new WaitForSeconds(time);
        WinMessage();
        //SceneManager.LoadScene(0);

    }

    public bool IsGameOver()
    {
        return Instance.capturedFlag || Instance.drown;
    }
    void Update()
    {
        if (IsGameOver() && soundActive)
        {
            Alarm.GetComponent<AudioSource>().volume = 0;
            soundActive = false;
        }
    }


    /// <summary>
    /// Applies the singleton pattern.
    /// </summary>
    private void ApplySingleton()
    {
        //Singleton 
        if (Instance == null)
            Instance = this;

        if (this != Instance)
            Destroy(gameObject);
    }

}
