using UnityEngine;

public class AutoDestroy : MonoBehaviour {
    public void Init(float delay) {
        Destroy(gameObject, delay);
    }
}
