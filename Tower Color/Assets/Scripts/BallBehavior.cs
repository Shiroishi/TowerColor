using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    private Rigidbody myRigidbody;
    public ColorSetting colorSetting;
    public AudioClip sndBomb;
    public AudioClip sndBird;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void OnCollisionEnter (Collision coll)
    {
        if (coll.gameObject.tag == "Can")
        {
            CanBehavior can = coll.gameObject.GetComponent<CanBehavior>();

            if (gameObject.name.StartsWith("Ball"))
            {
                // Check color
                if (colorSetting.myColor == can.GetComponent<ColorSetting>().myColor)
                {
                    Vibration.Vibrate(100);
                    can.Explode();
                    Die();
                }
                else
                {
                    // Remove ball
                    Vibration.Vibrate(50);
                    myRigidbody.useGravity = true;
                    Invoke("Die", 1f);
                }
            }
            else
            {
                // BOMB
                AudioSource.PlayClipAtPoint(sndBomb, transform.position);
                GenerateExplosion(80f, 0.8f, transform.position);
                Die();
            }
        }

        // BIRD
        if (coll.gameObject.tag == "Bird")
        {
            AudioSource.PlayClipAtPoint(sndBird, transform.position);
            GenerateExplosion(50f, 2f, coll.gameObject.transform.position);
            Destroy(coll.gameObject.transform.parent.gameObject);
            Die();
        }
    }

    void GenerateExplosion(float pow, float rad, Vector3 center)
    {
        Collider[] cans = Physics.OverlapSphere(transform.position, rad);

        foreach(Collider collider in cans)
        {
            if (collider.gameObject.tag == "Can")
                collider.GetComponent<Rigidbody>().AddExplosionForce(pow, transform.position, rad, 0, ForceMode.Impulse);
        }

        Instantiate(Resources.Load("RuntimePrefabs/Explosion"), transform.position, Quaternion.identity);
        Vibration.Vibrate(1000);
    }

    void Die ()
    {
        Destroy(gameObject);
    }
}
