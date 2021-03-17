using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidPickUp : MonoBehaviour
{
    public int score;
    private int points = 15;

    private void Awake()
    {
        score = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Eye")
        {
            AddScore();
            Destroy(other.gameObject);
        }
    }

    private void AddScore()
    {
        score += points;
    }
}
