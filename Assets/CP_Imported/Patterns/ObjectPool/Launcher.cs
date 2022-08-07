using UnityEngine;

public class Launcher : MonoBehaviour {
    [SerializeField] Bullet bulletPrefab;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab);
        }
    }
}