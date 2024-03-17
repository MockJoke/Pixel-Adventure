using UnityEditor.Animations;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer charSprite;
    [SerializeField] private Animator animator;
    
    [Header("Assets")]
    [SerializeField] private Sprite[] charSprites;
    [SerializeField] private AnimatorController[] animationControllers;

    void Awake()
    {
        if (charSprite == null)
            charSprite = GetComponent<SpriteRenderer>();

        if (animator == null)
            animator = GetComponent<Animator>();
        
        int charIndex = PlayerPrefs.GetInt("Character", 0);

        charSprite.sprite = charSprites[charIndex];
        animator.runtimeAnimatorController = animationControllers[charIndex];
    }
}
