using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private enum SceneName { MUSEUM, LABARINTH, PYRAMID, WAITING };
    private SceneName scene;
    private MuseumController museumController;
    private PyramidGameController pyramidController;
    private GameObject trophy;
    private int[] points;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Manager").Length > 1)
        {
            Destroy(this);
        }
        else
        {
            scene = SceneName.MUSEUM;
            if (GameObject.Find("MuseumController") != null)
            {
                museumController = GameObject.Find("MuseumController").GetComponent<MuseumController>();
            }
            trophy = GameObject.Find("Trophy");
            trophy.SetActive(false);
            Cursor.visible = false;
            points = new int[4];
            for(int i = 0; i < 4; i++)
            {
                points[i] = 0;
            }            
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Update()
    {
        switch(scene)
        {
            case SceneName.MUSEUM:
            {
                if(museumController.GetOnPortal())
                {
                    scene = SceneName.WAITING;
                    SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
                    StartCoroutine(WaitForLabarinthLoaded());
                }
                break;
            }
            case SceneName.LABARINTH:
            {
                if (1 == 2)
                {
                    //add points
                    scene = SceneName.WAITING;
                    SceneManager.LoadScene("PyramidScene", LoadSceneMode.Single);
                    StartCoroutine(WaitForPyramidLoaded());
                }
                break;
            }
            case SceneName.PYRAMID:
            {                
                if (pyramidController.GetFinished())
                {
                    scene = SceneName.WAITING;
                    SceneManager.LoadScene("Museum", LoadSceneMode.Single);
                    StartCoroutine(WaitForMuseumLoaded());
                }
                break;
            }
            case SceneName.WAITING:
            {
                break;
            }
            default: break;
        }
    }

    public IEnumerator WaitForLabarinthLoaded()
    {
        while (!SceneManager.GetSceneByName("SampleScene").isLoaded)
        {
            yield return null;
        }

        museumController = null;

        /*if (GameObject.Find("PyramidGameController") != null)
        {
            pyramidController = GameObject.Find("PyramidGameController").GetComponent<PyramidGameController>();
        }*/

        scene = SceneName.LABARINTH;
    }

    public IEnumerator WaitForPyramidLoaded()
    {
        while (!SceneManager.GetSceneByName("PyramidScene").isLoaded)
        {
            yield return null;
        }

        //museumController = null;

        if (GameObject.Find("PyramidGameController") != null)
        {
            pyramidController = GameObject.Find("PyramidGameController").GetComponent<PyramidGameController>();
            pyramidController.SetPoints(points);
        }

        scene = SceneName.PYRAMID;
    }

    public IEnumerator WaitForMuseumLoaded()
    {
        while (!SceneManager.GetSceneByName("Museum").isLoaded)
        {
            yield return null;
        }

        pyramidController = null;

        if (GameObject.Find("MuseumController") != null)
        {
            museumController = GameObject.Find("MuseumController").GetComponent<MuseumController>();
        }

        trophy = GameObject.Find("Trophy");
        trophy.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            points[i] = 0;
        }

        scene = SceneName.MUSEUM;
    }
}