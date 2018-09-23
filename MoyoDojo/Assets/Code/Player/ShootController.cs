using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootController : MonoBehaviour {

    private AudioSource source;

    public GameObject bullet;
    public Transform weaponPosition;
    public AudioClip playerShot;

    private float nextFire = 0.0f;

    public float fireRate = 0.5f;
    public float bulletSpeed = 1f;

    public int bulletDmg = 20;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }


    void Update ()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
        {
            source.PlayOneShot(playerShot);
            fire();
        }
    }

    void fire()
    {
        nextFire = Time.time + fireRate;

        GameObject inst_bullet = Instantiate(bullet, weaponPosition.position, weaponPosition.rotation) as GameObject;
        inst_bullet.GetComponent<BulletController>().PlayerDMG = bulletDmg;
        Rigidbody bulletRigidbody = inst_bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse) ;

        //Vector3 mousePos = (Vector3)Input.mousePosition;
        //// Get the height distance from camera to player. The Z axis is read as camera height in ScreenToWorldPoint
        //mousePos.z = Camera.main.transform.position.y - myCharacter.transform.position.y;
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos));
        //Vector3 bulletHeading = (mousePos - myCharacter.trasnform.position).normalized;
        //Vector3 bulletVelocity = bulletHeading * whateverBulletSpeedIs; // meters per second

        //if (Input.GetKey(KeyCode.Mouse0){
        //    GameObject bullet = Instantiate(bulletPrefab, myCharacter.transform.position, myCharacter.transform.rotation);
        //    bullet.GetComponent<MyBulletScript>().velocity = bulletVelocity;
        //}

        //void Update() -----> in bulleScript
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, transform.position + velocity, Time.deltaTime);
        //}

    }
}
