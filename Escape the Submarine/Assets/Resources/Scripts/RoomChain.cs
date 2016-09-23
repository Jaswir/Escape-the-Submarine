using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rooms
{
    public List<Room> rooms = new List<Room>();
}

public class RoomChain : MonoBehaviour
{

    public List<Rooms> RoomsList = new List<Rooms>();
    public float chainSwitchTime;

    [SerializeField]
    private float chainNextRoundTimer;
    [SerializeField]
    private int roomLayerCounter = 1;

    void Start()
    {
        if (RoomsList.Count == 0)
        {
            Debug.LogError("ROOMCHAIN NOT SET !, FIX ME oooo jaaa geee");
        }

        //Turns on sound in first rooms
        foreach (Room room in RoomsList[0].rooms)
        {
            room.startWaterFlow();
            room.SetDoorBleeps();
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (roomLayerCounter < RoomsList.Count)
        {
            chainNextRoundTimer += Time.deltaTime;
            if (chainNextRoundTimer > chainSwitchTime)
            {
                //Turns water off in current RoomLayer
                foreach (Room room in RoomsList[roomLayerCounter - 1].rooms)
                {
                    room.stopWaterFlow();
                }


                //Turns water on in nextRoom Layer
                foreach (Room room in RoomsList[roomLayerCounter].rooms)
                {
                    room.startWaterFlow();
                }


                //Prepares for next switch
                roomLayerCounter++;
                chainNextRoundTimer = 0f;
            }

        }
    }
}
