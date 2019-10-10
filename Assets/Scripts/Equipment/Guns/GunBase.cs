using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{

    public string gunName;

    public Rarity rarity;
    public GunType gunType;
    public AmmoType ammoType;

    public float fireRate;

    //equipped Weapon Attachments
    public GameObject equippedSight;
    public GameObject equippedClip;

    public Transform muzzle;
    public Projectile projectile;

    [SerializeField]
    private Transform bulletShell;
    [SerializeField]
    private Transform shellEjector;

    [SerializeField]
    private MuzzleFlashEffect muzzleFlash;
    private AudioSource gunSound;
    public AudioClip[] soundShootClips;
    public AudioClip soundEmpty;
    public AudioClip soundReload;

    public float minAccuracy = 1f;
    public float maxAccuracy = 3f;
    public float msBetweenShot = 100f;
    public float muzzleVelocity = 35f;
    public float reloadTime = 1.5f;
    public int clipSize = 30;

    private int shotgunSize = 50;

    public bool isEmpty;
    bool isReloading = false;
    float nextShotTime;

    private int defaultMagSize;

    private void Start()
    {
        defaultMagSize = clipSize;
        gunSound = GetComponent<AudioSource>();
        muzzleFlash = GetComponentInChildren<MuzzleFlashEffect>();
    }

    public virtual void ShootGun()
    {
        if (gunType == GunType.Rifle)
        {
            Debug.Log("Rifle");
            ShootModeRifle();
        }
        if (gunType == GunType.Shotgun)
        {
            Debug.Log("Shotgun");
            ShootModeShotgun();
        }
    }


    public virtual void ReloadGun()
    {
        if (clipSize < defaultMagSize - 1)
        {
            isReloading = true;
            gunSound.PlayOneShot(soundReload);
            clipSize = 30;
            nextShotTime = Time.time + reloadTime;

            isReloading = false;
            isEmpty = false;
        }
    }

    void PlayShootSound()
    {
        int clipNumber = Random.Range(0, soundShootClips.Length);
        gunSound.clip = soundShootClips[clipNumber];
        gunSound.Play();
    }

    void ShootModeRifle()
    {
        if (Time.time > nextShotTime && clipSize >= 0 && !isReloading)
        {
            nextShotTime = Time.time + msBetweenShot / 1000;

            //accuracy calculation needs to be redone to work with stats
            //Quaternion accuracy = Quaternion.Euler(Random.Range(-1.0f, 1.0f), Random.Range(-3.0f, 3.0f), 0);

            Quaternion accuracy = Quaternion.Euler(Random.Range(-minAccuracy, minAccuracy), Random.Range(-maxAccuracy, maxAccuracy), 0);

            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation * accuracy) as Projectile;
            newProjectile.SetSpeed(muzzleVelocity);


            Instantiate(bulletShell, shellEjector.position, shellEjector.rotation);
            PlayShootSound();
            muzzleFlash.Activate();
            clipSize -= 1;

        }
        else if (Time.time > nextShotTime && clipSize <= 0)
        {
            gunSound.PlayOneShot(soundEmpty);
            nextShotTime = Time.time + msBetweenShot / 150;
            if (!isEmpty)
            {
                isEmpty = true;
            }
        }
    }

    void ShootModeShotgun()
    {
        if (Time.time > nextShotTime && clipSize >= 0 && !isReloading)
        {
            for (int i = 0; i < shotgunSize; i++)
            {
                Quaternion accuracy = Quaternion.Euler(Random.Range(-minAccuracy, minAccuracy), Random.Range(-maxAccuracy, maxAccuracy), 0);

                Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation * accuracy) as Projectile;
                newProjectile.SetSpeed(muzzleVelocity);
            }

            Instantiate(bulletShell, shellEjector.position, shellEjector.rotation);
            PlayShootSound();
            muzzleFlash.Activate();
            clipSize -= 1;
        }
    }
}
