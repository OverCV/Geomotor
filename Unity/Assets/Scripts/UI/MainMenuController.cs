using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("UI References")]
    public Button carLevelButton;
    public Button shipLevelButton;
    public Button planeLevelButton;
    public Button quitButton;
    
    void Start()
    {
        // Setup button listeners
        if (carLevelButton != null)
        {
            carLevelButton.onClick.AddListener(StartCarLevel);
        }
        
        if (shipLevelButton != null)
        {
            shipLevelButton.onClick.AddListener(StartShipLevel);
        }
        
        if (planeLevelButton != null)
        {
            planeLevelButton.onClick.AddListener(StartPlaneLevel);
        }
        
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
    }
    
    public void StartCarLevel()
    {
        Debug.Log("Starting Car Level...");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartCarLevel();
        }
    }
    
    public void StartShipLevel()
    {
        Debug.Log("Starting Ship Level...");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartShipLevel();
        }
    }
    
    public void StartPlaneLevel()
    {
        Debug.Log("Starting Plane Level...");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartPlaneLevel();
        }
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.QuitGame();
        }
    }
}