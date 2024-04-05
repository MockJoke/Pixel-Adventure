using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{ 
    public static T Instance { get; private set; }

    [SerializeField] private bool persistent = false;

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Debug.Log($"{Instance.name} {gameObject.name} An instance of this singleton already exists");
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