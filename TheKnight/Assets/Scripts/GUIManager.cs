using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GUIManager : MonoBehaviour
{
    [Header("Attributes")]
    
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject[] HUD;
    [SerializeField] private Slider healthBar;

    //public bool Win { get; set; } = false;

    public void Pause()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }
    
    public void MainMenu()
    {
        //SceneTransition.SwitchScene(0);
        Time.timeScale = 1;
    }
    
    public void Restart()
    {
        //SceneTransition.SwitchScene(1);
        Time.timeScale = 1;
    }
    
    internal void SetMaxHealth(int health)
    {
        healthBar.maxValue = health;
    }
    
    internal void SetHealth(int health)
    {
        healthBar.value = health;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !deathScreen.activeInHierarchy)
        { 
            if(!pauseScreen.activeInHierarchy) Pause();
            else if(pauseScreen.activeInHierarchy) Resume();
        }

        switch (PlayerPrefs.GetInt("hudScale"))
        {
            case 0:
                foreach (var element in HUD)
                    element.transform.localScale = new Vector3(0.75f,0.75f,1);
                break;
            case 1:
                foreach (var element in HUD)
                    element.transform.localScale = new Vector3(1,1,1);
                break;
            case 2:
                foreach (var element in HUD)
                    element.transform.localScale = new Vector3(1.25f,1.25f,1);
                break;
        }
    }
}
