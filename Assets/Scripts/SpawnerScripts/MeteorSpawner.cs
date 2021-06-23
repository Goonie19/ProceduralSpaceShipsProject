using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{

    public List<Transform> Spawns;

    public List<MeteorData> MeteorTypes;

    public List<GameObject> pool;

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
        int[] positions = new int[4];

        positions[0] = CountElements(MeteorPos.LeftLeft);
        positions[1] = CountElements(MeteorPos.LeftRight);
        positions[2] = CountElements(MeteorPos.RightLeft);
        positions[3] = CountElements(MeteorPos.RightRight);

        MeteorPos max = MeteorPos.LeftLeft;

        int i = 0;

        int maxElement = 0;
        while(i < positions.Length -1)
        {
            if (positions[i] > maxElement)
            {
                maxElement = positions[i];

                switch (i)
                {
                    case 0: 
                        max = MeteorPos.LeftLeft;
                        ;break;
                    case 1:
                        max = MeteorPos.LeftRight;
                        ; break;
                    case 2:
                        max = MeteorPos.RightLeft;
                        ; break;
                    case 3:
                        max = MeteorPos.RightRight;
                        ; break;
                    default:
                        break;
                }

            }
        }

        GameObject o1, o2;

        switch (max)
        {
            case MeteorPos.LeftLeft:
                o1 = GetFreeObject();
                o1.transform.position = Spawns[1].position;
                o1.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                o1.SetActive(true);
                
                o2 = GetFreeObject();
                o2.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                if (positions[2] > positions[3])
                    o2.transform.position = Spawns[3].position;
                else
                    o2.transform.position = Spawns[2].position;

                o2.SetActive(true);

                break;
            case MeteorPos.LeftRight:

                o1 = GetFreeObject();
                o1.transform.position = Spawns[0].position;
                o1.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                o1.SetActive(true);
                
                o2 = GetFreeObject();
                o2.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                if (positions[2] > positions[3])
                    o2.transform.position = Spawns[3].position;
                else
                    o2.transform.position = Spawns[2].position;

                o2.SetActive(true);

                break;
            case MeteorPos.RightLeft:

                o1 = GetFreeObject();
                o1.transform.position = Spawns[3].position;
                o1.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                o1.SetActive(true);

                o2 = GetFreeObject();
                o2.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                if (positions[0] > positions[1])
                    o2.transform.position = Spawns[1].position;
                else
                    o2.transform.position = Spawns[0].position;

                o2.SetActive(true);

                break;
            case MeteorPos.RightRight:

                o1 = GetFreeObject();
                o1.transform.position = Spawns[2].position;
                o1.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                o1.SetActive(true);

                o2 = GetFreeObject();
                o2.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                if (positions[0] > positions[1])
                    o2.transform.position = Spawns[1].position;
                else
                    o2.transform.position = Spawns[0].position;

                o2.SetActive(true);

                break;
        }


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

    public GameObject GetFreeObject() { return pool.Find(item => item.activeInHierarchy == false); }
}
