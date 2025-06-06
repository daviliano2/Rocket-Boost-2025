using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
    void Update()
    {
        PlayerQuitApplication();
    }

    void PlayerQuitApplication()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("Quitting the application");
            Application.Quit();
        }
    }
}
