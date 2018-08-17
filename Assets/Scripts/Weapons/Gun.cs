using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public KeyCode fireButton;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(fireButton))
        {
            GameObject clone = Instantiate(bullet, transform.position, transform.rotation);
            Bullet newBullet = clone.GetComponent<Bullet>();
            newBullet.Fire(transform.forward);
        }
    }
}