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

    public void InstantiateMeteor()
    {

       if(_meteorPositions.Count >= 10)
        {
            #region FIND MAX POSITION INSTANTIATED
            int[] positions = new int[4];

            positions[0] = CountElements(MeteorPos.LeftLeft);
            positions[1] = CountElements(MeteorPos.LeftRight);
            positions[2] = CountElements(MeteorPos.RightLeft);
            positions[3] = CountElements(MeteorPos.RightRight);

            MeteorPos max = MeteorPos.LeftLeft;

            int i = 0;

            int maxElement = 0;
            while (i < positions.Length - 1)
            {
                if (positions[i] > maxElement)
                {
                    maxElement = positions[i];

                    switch (i)
                    {
                        case 0:
                            max = MeteorPos.LeftLeft;
                            ; break;
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

            #endregion

            ApplyRules(max, positions);

        } else
        {
            int r1 = UnityEngine.Random.Range(0, 2);
            int r2 = UnityEngine.Random.Range(2, 4);

            GameObject o1, o2;

            o1 = GetFreeObject();
            o1.transform.position = Spawns[r1].position;
            o1.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
            o1.SetActive(true);

            o2 = GetFreeObject();
            o2.transform.position = Spawns[r2].position;
            o2.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
            o2.SetActive(true);

            if (r1 == 0)
                AddToPlayerPos(MeteorPos.LeftLeft);
            else
                AddToPlayerPos(MeteorPos.LeftRight);

            if (r2 == 2)
                AddToPlayerPos(MeteorPos.RightLeft);
            else
                AddToPlayerPos(MeteorPos.RightRight);


        }

    }

    void ApplyRules(MeteorPos max, int[] positions)
    {

        GameObject o1, o2;

        switch (max)
        {
            #region MAX POSITIONS LEFT LEFT
            case MeteorPos.LeftLeft:
                o1 = GetFreeObject();
                o1.transform.position = Spawns[1].position;
                o1.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                AddToPlayerPos(MeteorPos.LeftRight);
                o1.SetActive(true);


                o2 = GetFreeObject();
                o2.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                if (positions[2] > positions[3] && positions[2] - positions[3] >= 2)
                {
                    o2.transform.position = Spawns[3].position;
                    AddToPlayerPos(MeteorPos.RightRight);
                }
                else if (positions[3] > positions[2] && positions[3] - positions[2] >= 2)
                {
                    o2.transform.position = Spawns[2].position;
                    AddToPlayerPos(MeteorPos.RightLeft);
                }
                else
                {
                    int r = UnityEngine.Random.Range(2, 4);
                    o2.transform.position = Spawns[r].position;

                    if (r == 2)
                        AddToPlayerPos(MeteorPos.RightLeft);
                    else
                        AddToPlayerPos(MeteorPos.RightRight);


                }

                o2.SetActive(true);

                break;

            #endregion

            #region MAX POSITIONS LEFT RIGHT

            case MeteorPos.LeftRight:

                o1 = GetFreeObject();
                o1.transform.position = Spawns[0].position;
                o1.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                AddToPlayerPos(MeteorPos.LeftLeft);
                o1.SetActive(true);

                o2 = GetFreeObject();
                o2.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];

                if (positions[2] > positions[3] && positions[2] - positions[3] >= 2)
                {
                    o2.transform.position = Spawns[3].position;
                    AddToPlayerPos(MeteorPos.RightRight);
                }
                else if (positions[3] > positions[2] && positions[3] - positions[2] >= 2)
                {
                    o2.transform.position = Spawns[2].position;
                    AddToPlayerPos(MeteorPos.RightLeft);
                }
                else
                {
                    int r = UnityEngine.Random.Range(2, 4);
                    o2.transform.position = Spawns[r].position;

                    if (r == 2)
                        AddToPlayerPos(MeteorPos.RightLeft);
                    else
                        AddToPlayerPos(MeteorPos.RightRight);


                }

                o2.SetActive(true);

                break;

            #endregion

            #region MAX POSITIONS RIGHT LEFT

            case MeteorPos.RightLeft:

                o1 = GetFreeObject();
                o1.transform.position = Spawns[3].position;
                o1.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                AddToPlayerPos(MeteorPos.RightRight);
                o1.SetActive(true);

                o2 = GetFreeObject();
                o2.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];


                if (positions[0] > positions[1] && positions[0] - positions[1] >= 2)
                {
                    o2.transform.position = Spawns[3].position;
                    AddToPlayerPos(MeteorPos.LeftRight);
                }
                else if (positions[1] > positions[0] && positions[1] - positions[0] >= 2)
                {
                    o2.transform.position = Spawns[2].position;
                    AddToPlayerPos(MeteorPos.LeftLeft);
                }
                else
                {
                    int r = UnityEngine.Random.Range(0, 2);
                    o2.transform.position = Spawns[r].position;

                    if (r == 0)
                        AddToPlayerPos(MeteorPos.LeftLeft);
                    else
                        AddToPlayerPos(MeteorPos.LeftRight);


                }

                o2.SetActive(true);

                break;

            #endregion

            #region MAX POSITIONS RIGHT RIGHT

            case MeteorPos.RightRight:

                o1 = GetFreeObject();
                o1.transform.position = Spawns[2].position;
                o1.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                AddToPlayerPos(MeteorPos.RightLeft);
                o1.SetActive(true);

                o2 = GetFreeObject();
                o2.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];

                if (positions[0] > positions[1] && positions[0] - positions[1] >= 2)
                {
                    o2.transform.position = Spawns[3].position;
                    AddToPlayerPos(MeteorPos.LeftRight);
                }
                else if (positions[1] > positions[0] && positions[1] - positions[0] >= 2)
                {
                    o2.transform.position = Spawns[2].position;
                    AddToPlayerPos(MeteorPos.LeftLeft);
                }
                else
                {
                    int r = UnityEngine.Random.Range(0, 2);
                    o2.transform.position = Spawns[r].position;

                    if (r == 0)
                        AddToPlayerPos(MeteorPos.LeftLeft);
                    else
                        AddToPlayerPos(MeteorPos.LeftRight);

                }

                o2.SetActive(true);

                break;

                #endregion
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
