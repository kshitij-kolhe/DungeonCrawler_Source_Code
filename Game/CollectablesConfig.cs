using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CollectablesConfig",menuName ="CollectablesConfig")]
public class CollectablesConfig : ScriptableObject
{
    [SerializeField]
    private GameObject[] collectables = null;

    public GameObject GetCollectables()
    {
        return collectables[(int)Random.Range(0f, (float)collectables.Length)];
    }
}
