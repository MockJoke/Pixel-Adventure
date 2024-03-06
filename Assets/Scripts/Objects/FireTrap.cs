using UnityEngine;

public class FireTrap : Trap
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRend;
    
    // [Header("Firetrap Timers")]
    // [SerializeField] private float activationDelay;
    // [SerializeField] private float activeTime;

    // private bool triggered;     //when the trap gets triggered
    // private bool active;        //when the trap is active and can hurt the player

    // private PlayerLife playerLife;
    // private static readonly int activatedAnim = Animator.StringToHash("Activated");

    // void Awake()
    // {
    //     if (animator == null)
    //         animator = GetComponent<Animator>();
    //     
    //     if (spriteRend == null)
    //         spriteRend = GetComponent<SpriteRenderer>();
    // }

    // void Update()
    // {
    //     if (playerLife != null && active)
    //         playerLife.Die();
    // }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Player"))
    //     {
    //         // if (!triggered)
    //         //     StartCoroutine(ActivateFiretrap());
    //         //
    //         // if (active)
    //             collision.GetComponent<PlayerLife>().Die();
    //     }
    // }
    
    // private IEnumerator ActivateFiretrap()
    // {
    //     //turn the sprite red to notify the player and trigger the trap
    //     triggered = true;
    //     spriteRend.color = Color.red;
    //
    //     //Wait for delay, activate trap, turn on animation, return color back to normal
    //     yield return new WaitForSeconds(activationDelay);
    //     
    //     spriteRend.color = Color.white; //turn the sprite back to its initial color
    //     active = true;
    //     animator.SetBool(activatedAnim, true);
    //
    //     //Wait until X seconds, deactivate trap and reset all variables and animator
    //     yield return new WaitForSeconds(activeTime);
    //     active = false;
    //     triggered = false;
    //     animator.SetBool(activatedAnim, false);
    // }
}
