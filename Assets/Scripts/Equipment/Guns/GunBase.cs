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

    private float currentRecoil = 0f;
    private float minRecoil = 0f;
    private float maxRecoil = 10f;

    public float minAccuracy = 1f;
    public float maxAccuracy = 3f;
    public float msBetweenShot = 100f;
    public float muzzleVelocity = 35f;
    public float reloadTime = 1.5f;
    public int currentClipSize = 30;
    public int remainingMags = 2;

    public bool isEmpty;
    public bool isMuzzleFlashActivated;
    bool isReloading = false;
    float nextShotTime;

    private int defaultMagSize;

    private void Start()
    {
        defaultMagSize = currentClipSize;
        gunSound = GetComponent<AudioSource>();
        muzzleFlash = GetComponentInChildren<MuzzleFlashEffect>();
    }

    public virtual void ShootGun()
    {
        if (gunType == GunType.Rifle)
        {
            //Debug.Log("Rifle");
            ShootModeRifle();
        }
    }


    public virtual void ReloadGun()
    {
            if (currentClipSize < defaultMagSize - 1)
            {
                isReloading = true;
                gunSound.PlayOneShot(soundReload);
                currentClipSize = 30;
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
        if (Time.time > nextShotTime && currentClipSize >= 0 && !isReloading)
        {
            nextShotTime = Time.time + msBetweenShot / 1000;

            //accuracy calculation needs to be redone to work with stats
 
            Quaternion accuracy = Quaternion.Euler(Random.Range(-minAccuracy, minAccuracy) * currentRecoil, Random.Range(-minAccuracy, minAccuracy) * currentRecoil, 0);

            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation * accuracy) as Projectile;
            newProjectile.SetSpeed(muzzleVelocity);

            Instantiate(bulletShell, shellEjector.position, shellEjector.rotation);
            PlayShootSound();
            if (isMuzzleFlashActivated)
            {
                muzzleFlash.Activate();
            }
            currentClipSize -= 1;            
        }
        else if (Time.time > nextShotTime && currentClipSize <= 0)
        {
            gunSound.PlayOneShot(soundEmpty);
            nextShotTime = Time.time + msBetweenShot / 150;
            if (!isEmpty)
            {
                isEmpty = true;
            }
        }
    }

    private void Update()
    {
        IncreaseRecoil();
    }

    bool isShooting
    {
        get
        {
            if (Input.GetAxis("Fire1") != 0 && !isEmpty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    void IncreaseRecoil()
    {
        if (isShooting)
        {
            currentRecoil = Mathf.Lerp(currentRecoil, maxRecoil, Time.deltaTime);
        }
        else
        {
            currentRecoil = Mathf.Lerp(currentRecoil, minRecoil, Time.deltaTime * 10f);
        }

        //Debug.Log(currentRecoil);
    }
}
