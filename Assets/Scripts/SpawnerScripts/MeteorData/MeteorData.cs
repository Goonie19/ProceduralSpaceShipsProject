using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeteorParameters", menuName = "OPG/MeteorData")]
public class MeteorData : ScriptableObject
{

    public Sprite sprite;

    public Color color;

    public float RotationSpeed;

    public float radiusOfImpact;

}
