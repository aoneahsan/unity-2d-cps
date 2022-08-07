using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TightlyCoupledEnemy : MonoBehaviour
{
    LowCohesionCharacter character;

    private void Start() {
        character = FindObjectOfType<LowCohesionCharacter>();     
    }

    public void Attack()
    {
        character.KnockBack();
        character.Shout();
        character.TakeDamage();
    }
}
