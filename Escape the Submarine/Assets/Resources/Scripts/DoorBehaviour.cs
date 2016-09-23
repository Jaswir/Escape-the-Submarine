using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public AudioSource audioSource;
    private bool doorEntered;
    private bool doorActive = true;
    private float doorTimer;


    /// <summary>
    /// Activated when door is closed
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter(Collision col)
    {
        AudioManager.Instance.Play(audioSource, "DeurIsDicht");
    }


    /// <summary>
    /// Activated when door is open
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter(Collider other)
    {
        //Speel normaal deur open geluid
        AudioManager.Instance.Play(audioSource, "Deur");

        //Disables Trigger
        doorEntered = true;

    }

    void CloseDoor()
    {
        //Closes door after audio clip time
        if (doorEntered && doorActive)
        {
            doorTimer += Time.deltaTime;
            if (doorTimer > audioSource.clip.length)
            {
                GetComponent<BoxCollider>().isTrigger = false;
                doorActive = false;
                doorTimer = 0;

                //Destroy Hole in Wall
                GameObject parent = transform.parent.gameObject;
                foreach (AudioSource adios in parent.GetComponentsInChildren<AudioSource>())
                {
                    if (adios.gameObject.tag == "Hole")
                    {
                        adios.bypassEffects = true;
                        adios.Stop();
                    }
                }

            }
        }
    }

    void Update()
    {
        CloseDoor();
    }
}
