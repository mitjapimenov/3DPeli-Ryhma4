using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCharacterCollision : MonoBehaviour
{
    public CapsuleCollider characterCollider;
    public CapsuleCollider chararacterBlockerCollider;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(characterCollider, chararacterBlockerCollider, true);
    }

    
}
