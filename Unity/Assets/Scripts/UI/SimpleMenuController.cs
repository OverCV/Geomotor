using UnityEngine;
using UnityEngine.UI;

public class SimpleMenuController : MonoBehaviour
{
    void Start()
    {
        // Find buttons by name and assign listeners
        SetupButton("CarLevelButton", StartCarLevel);
        SetupButton("ShipLevelButton", StartShipLevel);
        SetupButton("PlaneLevelButton", StartPlaneLevel);
        SetupButton("QuitButton", QuitGame);
    }
    
    private void SetupButton(string buttonName, System.Action callback)
    {
        GameObject buttonObj = GameObject.Find(buttonName);
        if (buttonObj != null)
        {
            Button button = buttonObj.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => callback());
                Debug.Log($"Button {buttonName} configured successfully");
            }
            else
            {
                Debug.LogWarning($"Button component not found on {buttonName}");
            }
        }
        else
        {
            Debug.LogWarning($"GameObject {buttonName} not found");
        }
    }
    
    public void StartCarLevel()
    {
        Debug.Log("Starting Car Level...");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartCarLevel();
        }
        else
        {
            Debug.LogError("GameManager.Instance is null!");
        }
    }
    
    public void StartShipLevel()
    {
        Debug.Log("Starting Ship Level...");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartShipLevel();
        }
        else
        {
            Debug.LogError("GameManager.Instance is null!");
        }
    }
    
    public void StartPlaneLevel()
    {
        Debug.Log("Starting Plane Level...");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartPlaneLevel();
        }
        else
        {
            Debug.LogError("GameManager.Instance is null!");
        }
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.QuitGame();
        }
        else
        {
            Debug.LogError("GameManager.Instance is null!");
        }
    }
}