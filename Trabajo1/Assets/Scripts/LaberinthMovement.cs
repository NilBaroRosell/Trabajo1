using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaberinthMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Light light;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shine());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized / speed;
    }

    IEnumerator shine()
    {
        transform.GetChild(0).transform.localPosition += Vector3.up * 20;
        while (light.intensity < 1)
        {
            light.intensity += 0.01f;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(3);

        while (light.intensity > 0)
        {
            light.intensity -= 0.01f;
            yield return new WaitForFixedUpdate();
        }

        transform.GetChild(0).transform.localPosition += Vector3.down * 20;

        yield return new WaitForSeconds(10);

        StartCoroutine(shine());
        yield break;
    }
}
