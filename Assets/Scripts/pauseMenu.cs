using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    //bool for menus
    public static bool gameIsPaused = false;
    public static bool inventoryIsOpened = false;

    //menus
    public GameObject pauseMenuUI;
    public GameObject inventoryMenuUI;

    // Update is called once per frame
    void Update()
    {
        //open/closes menu if escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                //closes
                Resume();
            }
            else
            {
                //opens
                Pause();
            }
        }

        //open/close inventory if tab is pressed
        if (Input.GetKeyDown(KeyCode.Tab) && gameIsPaused == false)
        {
            if (inventoryIsOpened)
            {
                //close inventory
                Close();
            }
            else
            {
                //opens enventory
                open();
            }
        }
    }

    //closing pause menu function
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;

    }

    //pausing game function
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    //closing inventory function
    public void Close()
    {
        inventoryMenuUI.SetActive(false);
        inventoryIsOpened = false;
    }

    //opening inventory function
    void open()
    {
        inventoryMenuUI.SetActive(true);
        inventoryIsOpened = true;
    }

}
