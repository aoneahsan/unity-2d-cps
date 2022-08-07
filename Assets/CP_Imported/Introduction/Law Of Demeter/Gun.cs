using UnityEngine;

public class Gun : MonoBehaviour {
    public Ammo ammo;

    public int GetRoundsLeft()
    {
        return ammo.roundsLeft;
    }
}