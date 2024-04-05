using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    public static T Instance { get; private set; }

    [SerializeField] private bool persistent = false;

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log($"{Instance.name} {gameObject.name} An instance of this singleton already exists");
            Destroy(this);
            // throw new System.Exception($"{Instance.name} {gameObject.name} An instance of this singleton already exists.");
        }
        else
        {
            Instance = this as T;
            
            if (persistent)
                DontDestroyOnLoad(this);
        }
    }
}