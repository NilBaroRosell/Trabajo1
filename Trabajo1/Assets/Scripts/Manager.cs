using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    //public Manager instance;
    private enum SceneName { MUSEUM, LABARINTH, PYRAMID, WAITING };
    private SceneName scene;
    private MuseumController museumController;
    private PyramidGameController pyramidController;
    [SerializeField] private GameObject trophy;
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
            trophy.SetActive(false);
            Cursor.visible = false;
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

        museumController = null;

        if (GameObject.Find("PyramidGameController") != null)
        {
            pyramidController = GameObject.Find("PyramidGameController").GetComponent<PyramidGameController>();
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

        trophy.SetActive(true);

        scene = SceneName.MUSEUM;
    }
}
