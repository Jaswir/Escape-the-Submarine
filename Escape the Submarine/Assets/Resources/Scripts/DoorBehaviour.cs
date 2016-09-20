using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{



    void OnTriggerEnter(Collider other)
    {

        AudioManager.Instance.Play("poepje");
    }
}
