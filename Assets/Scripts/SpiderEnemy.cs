using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemy : Enemy
{
  // Start is called before the first frame update
  protected override void Start()
  {
    // base.Start();
  }

  // Update is called once per frame
  void Update()
  {

  }

  protected override void Move()
  {
    Debug.Log("The Spider Enemy is a spider");
  }
}
