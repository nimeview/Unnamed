using UnityEngine;

public class GameQuitter : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.LogWarning("Игра завершена.");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    
}
