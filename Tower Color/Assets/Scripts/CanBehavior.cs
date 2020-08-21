using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBehavior : MonoBehaviour
{
    public bool isDying = false;
    public ColorSetting colorSetting;
    public GameObject myParticle;
    public AudioClip sndPop;

    public Material matFreeze;
    public Material matSaved;

    private Rigidbody myRigidbody;
    private Renderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody>();
        myRenderer = gameObject.GetComponent<Renderer>();

        matSaved = myRenderer.material;

        // Set particles color
        myParticle.GetComponent<ParticleSystemRenderer>().material = matSaved;

        Invoke("Freeze", 5f);
    }

    void Update()
    {
        if( transform.position.y < -3f)
            Destroy(gameObject);
    }

    public void Explode()
    {
        if (!myRigidbody.isKinematic)
        {
            isDying = true;
            StartCoroutine("ExplodeDelayed");
        }
    }

    IEnumerator ExplodeDelayed()
    {
        yield return new WaitForSeconds(0.05f);

        AudioSource.PlayClipAtPoint(sndPop, transform.position);

        // Chain explode
        Collider[] colls = Physics.OverlapCapsule(transform.position + transform.up/2f, transform.position - transform.up/2f, 0.6f);

        foreach (Collider coll in colls)
        {
            if (coll.gameObject.tag == "Can")
            {
                CanBehavior can = coll.gameObject.GetComponent<CanBehavior>();
                if (!can.isDying)
                {
                    if (colorSetting.myColor == can.GetComponent<ColorSetting>().myColor)
                        can.Explode();
                }
            }
        }

        // Particles
        myParticle.transform.parent = null;
        myParticle.transform.localScale = Vector3.one;
        myParticle.SetActive(true);

        // Translate body first to trigger exit and lower ceiling accordingly!
        transform.Translate(Vector3.up * 10000);
        yield return new WaitForFixedUpdate();

        Vibration.Vibrate(25);
        Destroy(gameObject);
    }

    public void Freeze()
    {
        myRigidbody.isKinematic = true;
        myRenderer.material = matFreeze;
    }

    public void Unfreeze()
    {
        myRigidbody.isKinematic = false;
        myRenderer.material = matSaved;

        CancelInvoke("Freeze");
    }
}
