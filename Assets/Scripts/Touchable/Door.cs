using DefaultNamespace.Touchable;
using UnityEngine;

public class Door : MonoBehaviour,ITouchable
{
    public void Interact()
    {
        Destroy(gameObject);
    }
}
