using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionArithmetic : MonoBehaviour
{
  void Start()
  {

  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Q))
    {
      PowFunction();
    }
  }

  void PowFunction()
  {
    Debug.Log(Mathf.Pow(2, 24));
  }
}
