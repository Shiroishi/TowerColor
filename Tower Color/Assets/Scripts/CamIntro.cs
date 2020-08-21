using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamIntro : MonoBehaviour
{
    public GameObject freeZone;
    public GameControls gameLogic;

    private float posY;
    private bool intro;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        posY = freeZone.transform.position.y - 12f;

        if (intro)
        {
            transform.Rotate(Vector3.up, (transform.position.y - posY) / 6f, 0);
        }

        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, posY, transform.position.z), Time.deltaTime);
    }

    public void StartGame()
    {
        intro = true;

        Invoke("EndIntro", 4.5f);
    }

    private void EndIntro()
    {
        intro = false;

        // Unfreeze top
        freeZone.SetActive(true);
        gameLogic.isPaused = false;
    }
}
