using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGunIndexes : MonoBehaviour
{
    public SetIndex[] setIndices;
    // Start is called before the first frame update
    void Start()
    {
        setIndices = FindObjectsOfType<SetIndex>();
    }

    // Update is called once per frame
    void Update()
    {
        if (setIndices == null)
        {
            setIndices = FindObjectsOfType<SetIndex>();
        }
    }

    public void CheckAllGuns()
    {
        foreach (var item in setIndices)
        {
            item.checkList = true;
        }
    }
}
