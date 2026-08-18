using UnityEngine;
using UnityEngine.SceneManagement;


public class WinDoor : MonoBehaviour
{
    public string winSceneName = "WinScreen";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached the Win Door!");

            SceneManager.LoadScene(winSceneName);
        }
    }
}
