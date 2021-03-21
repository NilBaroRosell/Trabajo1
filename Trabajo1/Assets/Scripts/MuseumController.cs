using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumController : MonoBehaviour
{
    [SerializeField] private PlayerMovementMuseum player;

    // Update is called once per frame
    void Update()
    {
        if(player.GetOnPortal())
        {
            Debug.Log("ON PORTAL");
        }
    }
}
