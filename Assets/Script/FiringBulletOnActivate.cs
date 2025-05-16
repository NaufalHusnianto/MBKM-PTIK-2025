using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FiringBulletOnActivate : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireBullet(ActivateEventArgs args)
    {
        GameObject SpawnedBullet = Instantiate(bullet);
        SpawnedBullet.transform.position = spawnPoint.position;
        SpawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
        Destroy(SpawnedBullet, 5);
    }
}
