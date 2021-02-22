using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Either place main game scene after MainMenu in heirarchy or replace this part with scene name
       
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
        
    }

    public void HowTo()
    {
        SceneManager.LoadScene("HowTo");
        
    }

    public void MainMenuReturn()
    {

        SceneManager.LoadScene("MainMenu");
    
    }

    public void Quit()
    {
        Application.Quit();
    }
}
