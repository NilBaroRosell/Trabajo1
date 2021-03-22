using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumController : MonoBehaviour
{
    [SerializeField] private PlayerMovementMuseum player;
    private bool onPortal = false;

    public bool GetOnPortal()
    {
        return player.GetOnPortal();
    }
}
