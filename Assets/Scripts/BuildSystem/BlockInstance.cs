using UnityEngine;

// Attach at runtime to every block you place.
// Remembers which BlockTypeData index this instance came from.
public class BlockInstance : MonoBehaviour
{
    [HideInInspector] public int typeIndex;
}
