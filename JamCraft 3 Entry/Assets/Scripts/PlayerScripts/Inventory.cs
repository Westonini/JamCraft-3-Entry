﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [HideInInspector]
    public bool canCraftPistol = false, canCraftMachete = false, canCraftAmmo = false, canCraftPipebomb = false, canCraftGauze = false, canCraftKey = false;

    [HideInInspector]
    public int pistolParts, macheteParts, ammo, bulletCasings, gunpowder, fuses, cloth, keyFragments;

    [HideInInspector]
    public int pipebombCount = 0, gauzeCount = 0, keysCount = 0;

    private SlotSelection SS;
    private CarryOverInventory COI;

    public GameObject pipebombImage;
    public TextMeshProUGUI pipebombCountText;
    public GameObject gauzeImage;
    public TextMeshProUGUI gauzeCountText;
    public GameObject keyImage;
    public TextMeshProUGUI keyCountText;

    private bool pipebombCraftedOnce = false;
    private bool gauzeCraftedOnce = false;
    private bool keyCraftedOnce = false;

    void Awake()
    {
        SS = GameObject.FindGameObjectWithTag("Player").GetComponent<SlotSelection>();

        try
        {
            COI = GameObject.FindGameObjectWithTag("CarryOverInventory").GetComponent<CarryOverInventory>();
        }
        catch
        {
            COI = null;
        }
    }

    void Start()
    {
        if (COI != null)
        {
            pistolParts = COI.carryOverPistolParts;
            macheteParts = COI.carryOverMacheteParts;
            ammo = COI.carryOverAmmo;
            bulletCasings = COI.carryOverBulletCasings;
            gunpowder = COI.carryOverGunpowder;
            fuses = COI.carryOverFuses;
            cloth = COI.carryOverCloth;

            pipebombCount = COI.carryOverPipebombCount;
            gauzeCount = COI.carryOverGauzeCount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        canCraftPistol = (pistolParts >= 4) ? true : false;
        canCraftMachete = (macheteParts >= 3) ? true : false;
        canCraftAmmo = (bulletCasings >= 6 && gunpowder >= 3) ? true : false;
        canCraftPipebomb = (fuses >= 2 && gunpowder >= 2) ? true : false;
        canCraftGauze = (cloth >= 4) ? true : false;
        canCraftKey = (keyFragments >= 4) ? true : false;

        SS.hasPipebomb = (pipebombCount >= 1) ? true : false;
        SS.hasGauze = (gauzeCount >= 1) ? true : false;
        SS.hasKey = (keysCount >= 1) ? true : false;

        UpdateConsumableCountText(pipebombCount, ref pipebombCraftedOnce, pipebombImage, pipebombCountText);
        UpdateConsumableCountText(gauzeCount, ref gauzeCraftedOnce, gauzeImage, gauzeCountText);
        UpdateConsumableCountText(keysCount, ref keyCraftedOnce, keyImage, keyCountText);
    }

    void UpdateConsumableCountText(int count, ref bool craftedOnce, GameObject image, TextMeshProUGUI countText)
    {
        if (count > 0 || craftedOnce)
        {
            craftedOnce = true;
            image.SetActive(true);
            countText.text = count.ToString();
        }
    }
}
