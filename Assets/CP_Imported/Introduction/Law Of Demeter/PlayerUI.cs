using UnityEngine;

public class PlayerUI : MonoBehaviour {
    [SerializeField] GunUI gunUI;

    public void RedrawUI()
    {
        Player player = GameObject.FindGameObjectWithTag("Player")
                                  .GetComponent<Player>();
        print(player.health);
        gunUI.RedrawUI(player.gun);
    }
}