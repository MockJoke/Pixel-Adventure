using UnityEditor;
using UnityEngine;

public class QuitMenu : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR 
        EditorApplication.isPlaying = false;
#else 
		Application.Quit();
#endif
    }
}
