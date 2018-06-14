using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour {

    public string gunName;

    public Rarity rarity;
    public GunType gunType;
    public AmmoType ammoType;

    public float fireRate;

    //equipped Weapon Attachments
    public GameObject equippedSight;
    public GameObject equippedClip;

    public virtual void ShootGun()
    {

    }

    public virtual void ReloadGun()
    {

    }


}
