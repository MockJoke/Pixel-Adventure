using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerMovement playerMovement;

    [Header("Assets")] 
    [SerializeField] private CharacterDataSO charData;

    void Awake()
    {
        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();
    }

    void OnEnable()
    {
        int charIndex = PlayerPrefs.GetInt("Character", 0);
        
        playerMovement.SetData(charData.characterData[charIndex]);
    }
}
