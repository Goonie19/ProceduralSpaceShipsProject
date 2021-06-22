using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{

    public List<Transform> LeftSpawns;
    public List<Transform> RightSpawns;

    public List<MeteorData> MeteorTypes;

    private List<MeteorPos> _meteorPositions;

    // Start is called before the first frame update
    void Start()
    {
        _meteorPositions = new List<MeteorPos>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToPlayerPos(MeteorPos pos)
    {
        if(_meteorPositions.Count >= 10)
            _meteorPositions.RemoveAt(0);

        _meteorPositions.Add(pos);
    }

    void WhereToInstantiate()
    {
        int leftLeft, leftRight, rightLeft, rightRight;

        leftLeft = CountElements(MeteorPos.LeftLeft);
        leftRight = CountElements(MeteorPos.LeftRight);
        rightLeft = CountElements(MeteorPos.RightLeft);
        rightRight = CountElements(MeteorPos.RightRight);
    }

    int CountElements(MeteorPos pos)
    {
        int cont = 0;

        foreach(MeteorPos p in _meteorPositions)
        {
            if (p == pos)
                ++cont;
        }

        return cont;
    }
}
