using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IngredientData : ScriptableObject
{
    public IngredientID id;

    public bool preperable;
    public bool cookable;
    public float cookTime;

}
