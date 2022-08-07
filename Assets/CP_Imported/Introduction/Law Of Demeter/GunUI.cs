using System;
using UnityEngine;

public class GunUI : MonoBehaviour {
    public void RedrawUI(Gun gun)
    {
        print(gun.GetRoundsLeft());
    }
}