using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBall : MonoBehaviour
{
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
    }
}