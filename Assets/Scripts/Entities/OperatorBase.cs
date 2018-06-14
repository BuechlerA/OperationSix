﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorBase : Entity
{

    //General Stats
    public string operatorName;
    public Nationality nationality;
    public Specialty specialty;
    public Rarity rarity;

    //Ability Stats
    private float aggression;
    private float leadership;
    private float selfcontrol;
    private float stamina;
    private float teamwork;
    private float demolition;
    private float electronics;
    private float firearms;
    private float grenades;
    private float stealth;

    //Bool Checks
    private bool isSneaking;

    //Equipment
    public GunBase equippedPrimary;
    public GunBase equippedSecondary;
    public ArmorBase equippedArmor;

    public virtual void Escort()
    {
        
    }
	

}
