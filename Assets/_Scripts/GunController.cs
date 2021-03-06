using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float damage = 10f;
    public float range = 8000f;
    public float fireRate = 5f;
    public ParticleSystem spark;

    public Camera fpsCam;
    private float nextTimeToFire = 0f;

    public AudioSource bullet_sound;

    private bool isWebGL = false;

    private void Start()
    {
        bullet_sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWebGL && Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    public void Shoot()
    {
        bullet_sound.Play();
        spark.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                
                target.TakeDamage(damage);
            }

            EnemyTest enemy = hit.transform.GetComponent<EnemyTest>();
            if (enemy != null)
            {
                
                enemy.TakeDamage(damage);
            }
        }
    }
}
