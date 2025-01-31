﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    private bool gameNotInFocus = false;
    private bool flashlightWasOn = false;
    private bool workbenchWasOn = false;

    public GameObject pauseMenu;
    public GameObject workBenchMenu;
    private GameObject player;
    public TextMeshProUGUI inventory;

    EnableOrDisableScripts EODS;
    ToggleFlashlight TF;
    SearchObject SO;
    Inventory inv;

    void Awake()
    {
        TF = GameObject.FindGameObjectWithTag("Player").GetComponent<ToggleFlashlight>();
        EODS = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnableOrDisableScripts>();
        SO = GameObject.FindGameObjectWithTag("Interact").GetComponent<SearchObject>();
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause") || gameNotInFocus)
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                UnPauseGame();
            }           
        }

        inventory.text = "Pipebombs: " + inv.pipebombCount.ToString() + "\n" +
                         "Gauze: " + inv.gauzeCount.ToString() + "\n" +
                         "Ammo: " + inv.ammo.ToString() + "\n" +
                         "Keys: " + inv.keysCount.ToString() + "\n" +
                         "\n" +
                         "Machete Parts: " + inv.macheteParts.ToString() + "\n" +
                         "Pistol Parts: " + inv.pistolParts.ToString() + "\n" +
                         "Key Fragments: " + inv.keyFragments.ToString() + "\n" +
                         "\n" +
                         "Gunpowder: " + inv.gunpowder.ToString() + "\n" +
                         "Bullet Casings: " + inv.bulletCasings.ToString() + "\n" +
                         "Fuses: " + inv.fuses.ToString() + "\n" +
                         "Cloth: " + inv.cloth.ToString() + "\n";
    }

    void OnApplicationFocus(bool hasFocus)
    {
        gameNotInFocus = !hasFocus;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);

        if (player != null)
        {
            EODS.GetScripts();
            EODS.EnableDisableScripts(false);
            flashlightWasOn = TF.flashlightIsOn;
            TF.ToggleFlashLight(false);
            TF.enabled = false;
        }

        Time.timeScale = 0;
        isPaused = true;

        if (workBenchMenu.activeSelf)
        {
            workbenchWasOn = true;
            workBenchMenu.SetActive(false);
        }
    }

    public void UnPauseGame()
    {
        pauseMenu.SetActive(false);
        
        if (player != null)
        {
            TF.enabled = true;
            if (flashlightWasOn)
            {
                TF.ToggleFlashLight(true);
            }
            if (SO.searching)
            {
                EODS.EnableDisableScripts(false);
            }
            else
            {
                EODS.EnableDisableScripts(true);
            }
        }

        if (workbenchWasOn)
        {
            workBenchMenu.SetActive(true);
            workbenchWasOn = false;
        }

        Time.timeScale = 1;
        isPaused = false;
    }

    public void ResumeGame()
    {
        UnPauseGame();
    }
    public void GoToMainMenu()
    {
        UnPauseGame();
        FindObjectOfType<AudioManager>().Stop("Walking");
        FindObjectOfType<AudioManager>().Stop("Sneaking");
        FindObjectOfType<AudioManager>().Stop("BombSizzle");
        FindObjectOfType<AudioManager>().Stop("Heartbeat");
        FindObjectOfType<AudioManager>().Stop("HeavyBreathing");
        SceneManager.LoadScene(0);
    }
    public void Retry()
    {
        UnPauseGame();
        FindObjectOfType<AudioManager>().Stop("Walking");
        FindObjectOfType<AudioManager>().Stop("Sneaking");
        FindObjectOfType<AudioManager>().Stop("BombSizzle");
        FindObjectOfType<AudioManager>().Stop("Heartbeat");
        FindObjectOfType<AudioManager>().Stop("HeavyBreathing");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
