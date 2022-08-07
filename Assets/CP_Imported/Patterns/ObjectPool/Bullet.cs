using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] Vector3 speed;

    private void Update() {
        transform.position += speed * Time.deltaTime;
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}