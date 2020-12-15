using UnityEngine;

[CreateAssetMenu(fileName = "TypeData", menuName = "Type Data")]
public class TypeData : ScriptableObject
{
    public ECreatureType Type;
    public Color PrimaryColor;
    public Color SecondaryColor;
    public Sprite Sprite;
    public Sprite UncoloredSprite;
}
