using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public RoomChain RoomChain;
    public Canvas Canvas;

    public bool drown;
    public bool capturedFlag;


    // Use this for initialization
    void Awake()
    {
        ApplySingleton();
    }


    void WinMessage()
    {
        Text messageText = Canvas.GetComponentInChildren<Text>();
        messageText.text = "You Managed to Escape a Sinking Submarine. The Audience goes wild and would like an encore.Will you take the leap?";
    }


    void LoseMessage()
    {
        Text messageText = Canvas.GetComponentInChildren<Text>();
        messageText.text =
            "You Drown in the Submarine. Shortly after the submarine shrinks to the bottom and implodes. " +
            "All that's left of you is a clump of your carcass, the size of a peanut ";
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
