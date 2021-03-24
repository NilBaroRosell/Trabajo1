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
    [SerializeField] private GameObject canvas;
    public int stage = 0;
    private bool finished = false;
    private int[] points;
    // Start is called before the first frame update
    private void Awake()
    {
        camera1.transform.position = cameraPositions.transform.GetChild(0).transform.position;
        camera1.transform.rotation = cameraPositions.transform.GetChild(0).transform.rotation;
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        canvas.transform.GetChild(1).gameObject.SetActive(false);
        canvas.transform.GetChild(2).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (stage)
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
                        canvas.transform.GetChild(0).gameObject.SetActive(true);
                        canvas.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                        canvas.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                        canvas.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                        canvas.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
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
                    canvas.transform.GetChild(1).gameObject.SetActive(true);
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

        canvas.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        canvas.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        StartCoroutine(Wait2(1));
    }

    private IEnumerator Wait2(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        canvas.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        canvas.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        StartCoroutine(Wait1(1));
    }

    private IEnumerator Wait1(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        canvas.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        canvas.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        StartCoroutine(WaitGo(1));
    }

    private IEnumerator WaitGo(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        canvas.transform.GetChild(0).gameObject.SetActive(false);
        movement.SetStart(true);
        stage++;
    }

    private IEnumerator WaitClassification(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        canvas.transform.GetChild(1).gameObject.SetActive(false);
        canvas.transform.GetChild(2).gameObject.SetActive(true);
        SetFinalPoints();
        StartCoroutine(WaitExpedition(3));
    }

    private IEnumerator WaitExpedition(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        canvas.transform.GetChild(2).gameObject.SetActive(false);
        stage++;
    }

    private void SetPoints()
    {
        int score = pickUp.GetScore();
        if (score < 15)
        {
            canvas.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Pink player: 50 points";
            canvas.transform.GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Blue player: 35 points";
            canvas.transform.GetChild(1).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Green player: 22 points";
            canvas.transform.GetChild(1).GetChild(5).GetComponent<TextMeshProUGUI>().text = "Red player: " + score + " points";
            points[0] += 2;
            points[1] += 4;
            points[2] += 6;
            points[3] += 8;
        }
        else if (score < 30)
        {
            canvas.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Blue player: 55 points";
            canvas.transform.GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Pink player: 47 points";
            canvas.transform.GetChild(1).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Red player: " + score + " points";
            canvas.transform.GetChild(1).GetChild(5).GetComponent<TextMeshProUGUI>().text = "Green player: 13 points";
            points[0] += 4;
            points[1] += 2;
            points[2] += 8;
            points[3] += 6;
        }
        else if (score < 45)
        {
            canvas.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Green player: 50 points";
            canvas.transform.GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Red player: " + score + " points";
            canvas.transform.GetChild(1).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Pink player: 26 points";
            canvas.transform.GetChild(1).GetChild(5).GetComponent<TextMeshProUGUI>().text = "Blue player: 15 points";
            points[0] += 6;
            points[1] += 8;
            points[2] += 2;
            points[3] += 4;
        }
        else
        {
            canvas.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Red player: " + score + " points";
            canvas.transform.GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Green player: 40 points";
            canvas.transform.GetChild(1).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Blue player: 38 points";
            canvas.transform.GetChild(1).GetChild(5).GetComponent<TextMeshProUGUI>().text = "Pink player: 23 points";
            points[0] += 8;
            points[1] += 6;
            points[2] += 4;
            points[3] += 2;
        }
    }

    private void SetFinalPoints()
    {
        int[] aux = new int[4];
        int[] aux2 = new int[4];

        for(int i = 0; i < 4; i ++)
        {
            if(points[i] > aux2[0])
            {
                aux2[3] = aux2[2];
                aux2[2] = aux2[1];
                aux2[1] = aux2[0];
                aux2[0] = points[i];
                aux[3] = aux[2];
                aux[2] = aux[1];
                aux[1] = aux[0];
                aux[0] = i;
            }
            else if(points[i] > aux2[1])
            {
                aux2[3] = aux2[2];
                aux2[2] = aux2[1];
                aux2[1] = points[i];
                aux[3] = aux[2];
                aux[2] = aux[1];
                aux[1] = i;
            }
            else if(points[i] > aux2[2])
            {
                aux2[3] = aux2[2];
                aux2[2] = points[i];
                aux[3] = aux[2];
                aux[2] = i;
            }
            else if(points[i] > aux2[3])
            {
                aux2[3] = points[i];
                aux[3] = i;
            }
        }

        string[] names = new string[4];

        for(int i = 0; i < 4; i++)
        {
            switch(aux[i])
            {
                case 0:
                {
                    names[i] = "Red";
                    break;
                }
                case 1:
                {
                    names[i] = "Green";
                    break;
                }
                case 2:
                {
                    names[i] = "Blue";
                    break;
                }
                case 3:
                {
                    names[i] = "Pink";
                    break;
                }
            }
        }
        
        canvas.transform.GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>().text = names[0] + " player: " + points[aux[0]] + " points";
        canvas.transform.GetChild(2).GetChild(3).GetComponent<TextMeshProUGUI>().text = names[1] + " player: " + points[aux[1]] + " points";
        canvas.transform.GetChild(2).GetChild(4).GetComponent<TextMeshProUGUI>().text = names[2] + " player: " + points[aux[2]] + " points";
        canvas.transform.GetChild(2).GetChild(5).GetComponent<TextMeshProUGUI>().text = names[3] + " player: " + points[aux[3]] + " points";
    }

    public bool GetFinished()
    {
        return finished;
    }

    public void SetPoints (int[] _points)
    {
        points = _points;
    }   
}