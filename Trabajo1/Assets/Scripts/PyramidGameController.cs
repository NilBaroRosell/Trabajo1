using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PyramidGameController : MonoBehaviour
{
    [SerializeField] private PyramidMovement movement;
    [SerializeField] private PyramidPickUp pickUp;
    [SerializeField] private GameObject cameraPositions;
    [SerializeField] private Camera camera1;
    [SerializeField] private Camera camera2;
    [SerializeField] private GameObject[] players;
    [SerializeField] private GameObject Canvas;
    public int stage = 0;
    private bool finished = false;
    // Start is called before the first frame update
    private void Awake()
    {
        camera1.transform.position = cameraPositions.transform.GetChild(0).transform.position;
        camera1.transform.rotation = cameraPositions.transform.GetChild(0).transform.rotation;
        Canvas.transform.GetChild(0).gameObject.SetActive(false);
        Canvas.transform.GetChild(1).gameObject.SetActive(false);
        Canvas.transform.GetChild(2).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch(stage)
        {
            case 0:
            {
                camera1.transform.position = Vector3.Lerp(camera1.transform.position, cameraPositions.transform.GetChild(1).position, Time.deltaTime * 0.75f);
                camera1.transform.eulerAngles = Vector3.Lerp(camera1.transform.eulerAngles, cameraPositions.transform.GetChild(1).eulerAngles, Time.deltaTime * 0.75f);
                if (Mathf.Abs((camera1.transform.position - cameraPositions.transform.GetChild(1).position).magnitude) < 3 && Mathf.Abs((camera1.transform.eulerAngles - cameraPositions.transform.GetChild(1).eulerAngles).magnitude) < 3)
                {
                    stage++;
                    movement.SetInitialPosition();
                    players[0].transform.position += new Vector3(-44, 0, 0);
                    players[1].transform.position += new Vector3(42, 0, 0);
                    players[2].transform.position += new Vector3(0, 0, 42);
                }
                break;
            }
            case 1:
            {
                camera1.transform.position = Vector3.Lerp(camera1.transform.position, cameraPositions.transform.GetChild(2).position, Time.deltaTime * 0.75f);
                camera1.transform.eulerAngles = Vector3.Lerp(camera1.transform.eulerAngles, cameraPositions.transform.GetChild(2).eulerAngles, Time.deltaTime * 0.75f);
                if (Mathf.Abs((camera1.transform.position - cameraPositions.transform.GetChild(2).position).magnitude) < 3 && Mathf.Abs((camera1.transform.eulerAngles - cameraPositions.transform.GetChild(2).eulerAngles).magnitude) < 3)
                {
                    Canvas.transform.GetChild(0).gameObject.SetActive(true);
                    Canvas.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    Canvas.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                    Canvas.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                    Canvas.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
                    Destroy(camera1);
                    stage++;
                    StartCoroutine(Wait3(1));
                }
                break;
            }
            case 2:
            {
                break;
            }
            case 3:
            {                    
                if (pickUp.GetEnd())
                {
                    stage++;
                }
                break;
            }
            case 4:
            {
                Canvas.transform.GetChild(1).gameObject.SetActive(true);
                stage++;
                SetPoints();
                StartCoroutine(WaitClassification(3));
                break;
            }
            case 5:
            {
                break;
            }
            case 6:
            {
                finished = true;
                break;
            }
            default: break;
        }        
    }

    private IEnumerator Wait3(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Canvas.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        Canvas.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        StartCoroutine(Wait2(1));
    }

    private IEnumerator Wait2(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Canvas.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        Canvas.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        StartCoroutine(Wait1(1));
    }

    private IEnumerator Wait1(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Canvas.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        Canvas.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        StartCoroutine(WaitGo(1));
    }

    private IEnumerator WaitGo(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Canvas.transform.GetChild(0).gameObject.SetActive(false);
        movement.SetStart(true);
        stage++;
    }

    private IEnumerator WaitClassification(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Canvas.transform.GetChild(1).gameObject.SetActive(false);
        Canvas.transform.GetChild(2).gameObject.SetActive(true);
        StartCoroutine(WaitExpedition(3));
    }

    private IEnumerator WaitExpedition(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Canvas.transform.GetChild(2).gameObject.SetActive(false);
        stage++;
    }

    private void SetPoints()
    {
        int score = pickUp.GetScore();
        if(score < 15)
        {
            Canvas.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Yellow player: 50 points";
            Canvas.transform.GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Blue player: 35 points";
            Canvas.transform.GetChild(1).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Green player: 22 points";
            Canvas.transform.GetChild(1).GetChild(5).GetComponent<TextMeshProUGUI>().text = "Red player: " + score + " points";
        }
        else if (score < 30)
        {
            Canvas.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Blue player: 55 points";
            Canvas.transform.GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Yellow player: 47 points";
            Canvas.transform.GetChild(1).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Red player: " + score + " points";
            Canvas.transform.GetChild(1).GetChild(5).GetComponent<TextMeshProUGUI>().text = "Green player: 13 points";
        }
        else if (score < 45)
        {
            Canvas.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Green player: 50 points";
            Canvas.transform.GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Red player: " + score + " points";
            Canvas.transform.GetChild(1).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Yellow player: 26 points";
            Canvas.transform.GetChild(1).GetChild(5).GetComponent<TextMeshProUGUI>().text = "Blue player: 15 points";
        }
        else
        {
            Canvas.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Red player: " + score + " points";
            Canvas.transform.GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Green player: 40 points";
            Canvas.transform.GetChild(1).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Blue player: 38 points";
            Canvas.transform.GetChild(1).GetChild(5).GetComponent<TextMeshProUGUI>().text = "Yellow player: 23 points";
        }
    }

    public bool GetFinished()
    {
        return finished;
    }
}