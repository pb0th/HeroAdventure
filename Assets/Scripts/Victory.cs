using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Victory : MonoBehaviour
{
    [SerializeField] private GameObject victoryScreenUI;

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Collided with something");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Victory! Collided with the Player.");
             ShowVictoryScreen();
        }
    }

    private void ShowVictoryScreen()
    {
        if (victoryScreenUI != null)
        {
            victoryScreenUI.SetActive(true); // Enable the victory screen UI
        }
        else
        {
            Debug.LogWarning("Victory Screen UI is not assigned in the inspector!");
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);// Reloads the current scene
    }
}
