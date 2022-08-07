using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooselyCoupledEnemy : MonoBehaviour
{
    public void Attack(IAttackable target)
    {
        target.TakeHit();
    }
}

public interface IAttackable
{
    void TakeHit();
}