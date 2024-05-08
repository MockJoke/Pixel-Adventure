using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterDataSO", order = 1)]
public class CharacterDataSO : ScriptableObject
{
    [System.Serializable]
    public struct CharacterData
    {
        public CharacterName charName;
        public Sprite charSprite;
        public RuntimeAnimatorController animatorController;
        public LevelName unlocksAtLvl;
        public float moveSpeed;
        public float jumpForce;
        public float extraJumpForce;
        public int maxAirJumpCnt;
        [Range(0f, 30f), Tooltip("Set as 0, if player can't dash")] public float dashSpeed;
        [Range(0f, 1f), Tooltip("Amount of time, player will remain in dashing")] public float dashDuration ;
        [Range(0f, 5f), Tooltip("Duration bw two dashes")] public float dashCooldownDuration;
        public bool canAttack;
        public bool canWallGrab;
        public float grabCheckRadius;
        public Vector2 grabRightOffset;
        public Vector2 grabLeftOffset;
        public float wallSlideSpeed;
        public Vector2 wallJumpForce;
    }

    public List<CharacterData> characterData;
    
    [System.Serializable]
    public struct PlayerLifeData
    {
        public int InitLifeCount;
        public int MaxLivesBound;
        public int FoodTradeForLivesFactor;
    }

    public PlayerLifeData playerLifeData;
}

public enum CharacterName
{
    None = 0,
    VirtualGuy = 1,
    PinkMan = 2,
    NinjaFrog = 3,
    MaskDude = 4,
}