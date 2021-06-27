using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{

    public List<Transform> Spawns;

    public float SpeedSQRT;

    public List<MeteorData> MeteorTypes;

    public List<GameObject> pool;

    private List<MeteorPos> _meteorPositions;

    private GameObject _actualObstacle1, _actualObstacle2;

    private float _actualSpeed, _sqrtSpeed;



    // Start is called before the first frame update
    void Start()
    {
        _meteorPositions = new List<MeteorPos>();
        _actualSpeed = SpeedSQRT;
        _sqrtSpeed = SpeedSQRT;
        InstantiateMeteor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToPlayerPos(MeteorPos pos)
    {
        if(_meteorPositions.Count >= 20)
            _meteorPositions.RemoveAt(0);

        _meteorPositions.Add(pos);
    }

    public void InstantiateMeteor()
    {

        SpeedUp();

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
            while (i < positions.Length)
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
                ++i;
            }

            #endregion

            ApplyRules(max, positions);

        } else
        {
            int r1 = UnityEngine.Random.Range(0, 2);
            int r2 = UnityEngine.Random.Range(2, 4);

            _actualObstacle1 = GetFreeObject();
            _actualObstacle1.transform.position = Spawns[r1].position;
            _actualObstacle1.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
            _actualObstacle1.GetComponent<MeteorBehaviour>().SetSpeed(_actualSpeed );
            _actualObstacle1.SetActive(true);

            _actualObstacle2 = GetFreeObject();
            _actualObstacle2.transform.position = Spawns[r2].position;
            _actualObstacle2.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
            _actualObstacle2.GetComponent<MeteorBehaviour>().SetSpeed(_actualSpeed );
            _actualObstacle2.SetActive(true);

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

        switch (max)
        {
            #region MAX POSITIONS LEFT LEFT
            case MeteorPos.LeftLeft:
                _actualObstacle1 = GetFreeObject();
                _actualObstacle1.transform.position = Spawns[1].position;
                _actualObstacle1.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                AddToPlayerPos(MeteorPos.LeftRight);
                _actualObstacle1.SetActive(true);


                _actualObstacle2 = GetFreeObject();
                _actualObstacle2.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                if (positions[2] > positions[3] && positions[2] - positions[3] >= 2)
                {
                    _actualObstacle2.transform.position = Spawns[3].position;
                    AddToPlayerPos(MeteorPos.RightRight);
                }
                else if (positions[3] > positions[2] && positions[3] - positions[2] >= 2)
                {
                    _actualObstacle2.transform.position = Spawns[2].position;
                    AddToPlayerPos(MeteorPos.RightLeft);
                }
                else
                {
                    int r = UnityEngine.Random.Range(2, 4);
                    _actualObstacle2.transform.position = Spawns[r].position;

                    if (r == 2)
                        AddToPlayerPos(MeteorPos.RightLeft);
                    else
                        AddToPlayerPos(MeteorPos.RightRight);


                }

                _actualObstacle2.SetActive(true);

                break;

            #endregion

            #region MAX POSITIONS LEFT RIGHT

            case MeteorPos.LeftRight:

                _actualObstacle1 = GetFreeObject();
                _actualObstacle1.transform.position = Spawns[0].position;
                _actualObstacle1.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                _actualObstacle1.GetComponent<MeteorBehaviour>().SetSpeed(_actualSpeed);
                AddToPlayerPos(MeteorPos.LeftLeft);
                _actualObstacle1.SetActive(true);

                _actualObstacle2 = GetFreeObject();
                _actualObstacle2.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                _actualObstacle2.GetComponent<MeteorBehaviour>().SetSpeed(_actualSpeed);

                if (positions[2] > positions[3] && positions[2] - positions[3] >= 2)
                {
                    _actualObstacle2.transform.position = Spawns[3].position;
                    AddToPlayerPos(MeteorPos.RightRight);
                }
                else if (positions[3] > positions[2] && positions[3] - positions[2] >= 2)
                {
                    _actualObstacle2.transform.position = Spawns[2].position;
                    AddToPlayerPos(MeteorPos.RightLeft);
                }
                else
                {
                    int r = UnityEngine.Random.Range(2, 4);
                    _actualObstacle2.transform.position = Spawns[r].position;

                    if (r == 2)
                        AddToPlayerPos(MeteorPos.RightLeft);
                    else
                        AddToPlayerPos(MeteorPos.RightRight);


                }

                _actualObstacle2.SetActive(true);

                break;

            #endregion

            #region MAX POSITIONS RIGHT LEFT

            case MeteorPos.RightLeft:

                _actualObstacle1 = GetFreeObject();
                _actualObstacle1.transform.position = Spawns[3].position;
                _actualObstacle1.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                _actualObstacle1.GetComponent<MeteorBehaviour>().SetSpeed(_actualSpeed);
                AddToPlayerPos(MeteorPos.RightRight);
                _actualObstacle1.SetActive(true);

                _actualObstacle2 = GetFreeObject();
                _actualObstacle2.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                _actualObstacle2.GetComponent<MeteorBehaviour>().SetSpeed(_actualSpeed);


                if (positions[0] > positions[1] && positions[0] - positions[1] >= 2)
                {
                    _actualObstacle2.transform.position = Spawns[3].position;
                    AddToPlayerPos(MeteorPos.LeftRight);
                }
                else if (positions[1] > positions[0] && positions[1] - positions[0] >= 2)
                {
                    _actualObstacle2.transform.position = Spawns[2].position;
                    AddToPlayerPos(MeteorPos.LeftLeft);
                }
                else
                {
                    int r = UnityEngine.Random.Range(0, 2);
                    _actualObstacle2.transform.position = Spawns[r].position;

                    if (r == 0)
                        AddToPlayerPos(MeteorPos.LeftLeft);
                    else
                        AddToPlayerPos(MeteorPos.LeftRight);


                }

                _actualObstacle2.SetActive(true);

                break;

            #endregion

            #region MAX POSITIONS RIGHT RIGHT

            case MeteorPos.RightRight:

                _actualObstacle1 = GetFreeObject();
                _actualObstacle1.transform.position = Spawns[2].position;
                _actualObstacle1.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                _actualObstacle1.GetComponent<MeteorBehaviour>().SetSpeed(_actualSpeed );
                AddToPlayerPos(MeteorPos.RightLeft);
                _actualObstacle1.SetActive(true);

                _actualObstacle2 = GetFreeObject();
                _actualObstacle2.GetComponent<MeteorBehaviour>().data = MeteorTypes[UnityEngine.Random.Range(0, MeteorTypes.Count)];
                _actualObstacle2.GetComponent<MeteorBehaviour>().SetSpeed(_actualSpeed );

                if (positions[0] > positions[1] && positions[0] - positions[1] >= 2)
                {
                    _actualObstacle2.transform.position = Spawns[3].position;
                    AddToPlayerPos(MeteorPos.LeftRight);
                }
                else if (positions[1] > positions[0] && positions[1] - positions[0] >= 2)
                {
                    _actualObstacle2.transform.position = Spawns[2].position;
                    AddToPlayerPos(MeteorPos.LeftLeft);
                }
                else
                {
                    int r = UnityEngine.Random.Range(0, 2);
                    _actualObstacle2.transform.position = Spawns[r].position;

                    if (r == 0)
                        AddToPlayerPos(MeteorPos.LeftLeft);
                    else
                        AddToPlayerPos(MeteorPos.LeftRight);

                }

                _actualObstacle2.SetActive(true);

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

    void SpeedUp()
    {
        _sqrtSpeed = (Mathf.Sqrt(_sqrtSpeed));
        _actualSpeed += _sqrtSpeed - 1;
        Debug.Log(_actualSpeed);
    }

    public GameObject GetFreeObject() { return pool.Find(item => item.activeInHierarchy == false); }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _actualObstacle1.SetActive(false);
        _actualObstacle2.SetActive(false);
        InstantiateMeteor();
    }
}
