using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaberinthIA : MonoBehaviour
{
    Vector3[] points;
    int idx = 0;

    // Start is called before the first frame update
    void Start()
    {
        points = new Vector3[transform.childCount-1];

        for(int i = 0; transform.childCount-1 > i; i++)
        {
            points[i] = transform.GetChild(i).position;
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.Normalize(points[idx] - transform.position) / 20;
        if (Vector3.Distance(transform.position, points[idx]) < 0.5f && idx < points.Length) idx++;
        GameObject.Find("Red").GetComponent<LaberinthMovement>().hasFinished(transform);
    }
}
