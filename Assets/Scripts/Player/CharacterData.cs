using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterDataSO", order = 1)]
public class CharacterDataSO : ScriptableObject
{
    [System.Serializable]
    public struct CharacterData
    {
        public CharacterName charName;
        public float moveSpeed;
        public float jumpForce;
        public float extraJumpForce;
        public int maxAirJumpCnt;
        public Sprite charSprite;
        public AnimatorController animatorController;
        public LevelName unlocksAtLvl;
        public bool canAttack;
    }

    public List<CharacterData> characterData;
}

public enum CharacterName
{
    None = 0,
    VirtualGuy = 1,
    PinkMan = 2,
    NinjaFrog = 3,
    MaskDude = 4,
}
