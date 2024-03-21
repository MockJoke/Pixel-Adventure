using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterDataSO", order = 1)]
public class CharacterDataSO : ScriptableObject
{
    [System.Serializable]
    public struct CharacterData
    {
        public Characters charName;
        public float moveSpeed;
        public float jumpForce;
        public float doubleJumpForce;
        public int maxAirJumpCnt;
        public Sprite charSprite;
        public AnimatorController animatorController;
    }

    public List<CharacterData> characterData;
}

public enum Characters
{
    None = 0,
    VirtualGuy = 1,
    PinkMan = 2,
    NinjaFrog = 3,
    MaskDude = 4,
}
