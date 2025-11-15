using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Create Character")]
public class CharacterConfiguration : ScriptableObject
{
    public string characterName;
    public Sprite sprite;
}
