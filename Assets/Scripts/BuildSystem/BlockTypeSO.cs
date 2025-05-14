using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/Block Type")]
public class BlockTypeSO : ScriptableObject
{
    public string blockName;
    public Sprite icon;
    public GameObject prefab;
    public int maxCount = 1;
    [TextArea] public string description;
}
