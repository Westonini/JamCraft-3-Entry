﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSelection : MonoBehaviour
{
    public bool hasMachete = false;
    public bool hasPistol = false;
    public bool hasPipebomb = false;
    public bool hasGauze = false;
    public bool hasKey = false;

    public GameObject nothingEquipped;
    public GameObject machete;
    public GameObject pistol;
    public GameObject pipebomb;
    public GameObject gauze;
    public GameObject key;

    [HideInInspector]
    public string currentlySelectedItem = "NoWeapon";

    private PlayerShooting PS;
    private UseConsumableItem UCI;
    private CarryOverInventory CCO;

    [HideInInspector]
    public bool cancelReload;

    void Awake()
    {
        PS = pistol.GetComponent<PlayerShooting>();
        UCI = GameObject.FindGameObjectWithTag("Player").GetComponent<UseConsumableItem>();
        
        try
        {
            CCO = GameObject.FindGameObjectWithTag("CarryOverInventory").GetComponent<CarryOverInventory>();
        }
        catch
        {
            CCO = null;
        }
    }

    private void Start()
    {
        if (CCO != null)
        {
            hasMachete = CCO.carryOverHasMachete;
            hasPistol = CCO.carryOverHasPistol;
            hasPipebomb = CCO.carryOverHasPipebomb;
            hasGauze = CCO.carryOverHasGauze;
        }
    }

    void Update()
    {
        //Machete is slot 1
        if (Input.GetButtonDown("Slot1") && hasMachete == true && currentlySelectedItem != "Machete")
        {
            ChangeSlots(machete);
        }
        else if (Input.GetButtonDown("Slot1") && hasMachete == true && currentlySelectedItem == "Machete")
        {
            ChangeSlots(nothingEquipped);
        }

        //Pistol is slot 2
        if (Input.GetButtonDown("Slot2") && hasPistol == true && currentlySelectedItem != "Pistol")
        {
            ChangeSlots(pistol);
        }
        else if (Input.GetButtonDown("Slot2") && hasPistol == true && currentlySelectedItem == "Pistol")
        {
            ChangeSlots(nothingEquipped);
        }

        //Pipebomb is slot 3
        if (Input.GetButtonDown("Slot3") && hasPipebomb == true && currentlySelectedItem != "Pipebomb")
        {
            ChangeSlots(pipebomb);
        }
        else if (Input.GetButtonDown("Slot3") && hasPipebomb == true && currentlySelectedItem == "Pipebomb")
        {
            ChangeSlots(nothingEquipped);
        }

        //Gauze is slot 4
        if (Input.GetButtonDown("Slot4") && hasGauze == true && currentlySelectedItem != "Gauze")
        {
            ChangeSlots(gauze);
        }
        else if (Input.GetButtonDown("Slot4") && hasGauze == true && currentlySelectedItem == "Gauze")
        {
            ChangeSlots(nothingEquipped);
        }

        //Key is slot 5
        if (Input.GetButtonDown("Slot5") && hasKey == true && currentlySelectedItem != "Key")
        {
            ChangeSlots(key);
        }
        else if (Input.GetButtonDown("Slot5") && hasKey == true && currentlySelectedItem == "Key")
        {
            ChangeSlots(nothingEquipped);
        }
    }

    //Changes currently equipped item to whatever is passed in.
    public void ChangeSlots(GameObject selectedSlot)
    {
        if (pistol.activeSelf == true && currentlySelectedItem == "Pistol")
        {
            PS.CancelReload();
        }
        if (gauze.activeSelf == true && currentlySelectedItem == "Gauze" && UseConsumableItem.playerIsHealing)
        {
            UCI.StopHeal();
        }
        if (selectedSlot == pipebomb)
        {
            FindObjectOfType<AudioManager>().Play("BombSizzle");
        }
        else if (currentlySelectedItem == "Pipebomb")
        {
            FindObjectOfType<AudioManager>().Stop("BombSizzle");
        }

        nothingEquipped.SetActive(false);
        machete.SetActive(false);
        pistol.SetActive(false);
        pipebomb.SetActive(false);
        gauze.SetActive(false);
        key.SetActive(false);

        selectedSlot.SetActive(true);
        currentlySelectedItem = selectedSlot.name.ToString();
    }
}
