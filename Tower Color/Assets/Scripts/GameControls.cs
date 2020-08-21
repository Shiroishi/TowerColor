using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MyColor {Red, Blue, Green, Yellow};

public class GameControls : MonoBehaviour
{
    public GameObject camPivot;
    public GameObject ball;
    public GameObject bomb;
    public GameObject uiAd;
    public bool isPaused = true;

    private GameObject myBall;
    private Ray ray;
    private RaycastHit hit;
    private Camera cam;
    private float delta = 0;
    private bool swipping = false;

    // Start is called before the first frame update
    void Start()
    {
        // spawn first ball
        cam = Camera.main;
        SpawnBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            if (Input.touchCount > 0)
            {
                // Swipe
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    delta = Input.GetTouch(0).deltaPosition.x;
                    camPivot.transform.Rotate(Vector3.up, delta);

                    swipping = true;
                }
                else
                {
                    // Shoot ball
                    if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        if (swipping) 
                            swipping = false;
                        else
                        {
                            if (ball != null)
                            {
                                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                                if (Physics.Raycast(ray, out hit))
                                {
                                    if (hit.collider != null)
                                    {
                                        GameObject go = hit.collider.gameObject;
                                        
                                        if (go.tag == "Can" || go.tag == "Bird")
                                        {
                                            cam.gameObject.GetComponent<Animation>().Play();

                                            myBall.GetComponent<Rigidbody>().AddForce((go.transform.position - myBall.transform.position) / 3f, ForceMode.Impulse);
                                            myBall = null;

                                            Invoke("SpawnBall", 0.2f);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                // Lerp camera down
                camPivot.transform.Rotate(Vector3.up, delta);
                delta = Mathf.Lerp(delta, 0, Time.deltaTime * 4f);
            }

            // TESTING
#if UNITY_EDITOR 
            if (Input.GetMouseButtonDown(0))
            {
                if (ball != null)
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider != null)
                        {
                            GameObject go = hit.collider.gameObject;
                            
                            if (go.tag == "Can" || go.tag == "Bird")
                            {
                                cam.gameObject.GetComponent<Animation>().Play();

                                myBall.GetComponent<Rigidbody>().AddForce((go.transform.position - myBall.transform.position) / 3f, ForceMode.Impulse);
                                myBall = null;

                                Invoke("SpawnBall", 0.4f);
                            }
                        }
                    }
                }
            }
#endif
        }
    }

    private void SpawnBall()
    {
        myBall = Instantiate(ball, Vector3.zero, Quaternion.identity);
        myBall.transform.parent = cam.transform.Find("Slot").transform;
        myBall.transform.localPosition = new Vector3(0, -1.5f, 3);
    }

    public void ShowAd()
    {
        if (!isPaused)
        {
            isPaused = true;
            uiAd.SetActive(true);
        }
    }

    public void CloseAd()
    {
        uiAd.SetActive(false);

        // Get bomb reward
        Destroy(myBall.gameObject);
        myBall = Instantiate(bomb, Vector3.zero, Quaternion.identity);
        myBall.transform.parent = cam.transform.Find("Slot").transform;
        myBall.transform.localPosition = new Vector3(0, -1.5f, 3);

        Invoke("Unpause", 0.2f);
    }

    private void Unpause()
    {
        isPaused = false;
    }
}
