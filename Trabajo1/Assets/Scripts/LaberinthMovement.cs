using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaberinthMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Light light;
    List<KeyValuePair<int, string>> ranking;
    public int[] points;
    bool finished = false;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        ranking = new List<KeyValuePair<int, string>>();
        points = new int[4];
        StartCoroutine(shine());
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized / speed;
            hasFinished(transform);
        }
        else
        {
            if (ranking.Count == 4) showClasification();
        }
    }

    IEnumerator shine()
    {
        transform.GetChild(0).transform.localPosition += Vector3.up * 10;
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

        transform.GetChild(0).transform.localPosition += Vector3.down * 10;

        yield return new WaitForSeconds(10);

        StartCoroutine(shine());
        yield break;
    }

    public void hasFinished(Transform _player)
    {
       if(_player.position.x > 92)
        {
            ranking.Add(new KeyValuePair<int, string>(10 - ranking.Count * 3, _player.name));
            Debug.Log(ranking[ranking.Count - 1]);
            if (_player != transform)
            {
                _player.GetComponent<LaberinthIA>().enabled = false;
            }
            else
            {
                finished = true;
            }
        }
    }

    void showClasification()
    {
        points[0] = ranking.Find(x => x.Value == "Red").Key;
        points[1] = ranking.Find(x => x.Value == "Green").Key;
        points[2] = ranking.Find(x => x.Value == "Blue").Key;
        points[3] = ranking.Find(x => x.Value == "Pink").Key;

        Invoke("EndOfTheGame", 10.0f);
    }

    void EndOfTheGame()
    {
        gameOver = true;
    }
}
