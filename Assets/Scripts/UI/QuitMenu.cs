using UnityEditor;
using UnityEngine;

public class QuitMenu : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR 
        EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE) 
    Application.Quit();
#elif (UNITY_WEBGL)
    Application.OpenURL("https://mockjoke.itch.io/pixel-adventure");
#endif
    }
}
