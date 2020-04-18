using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ingredient
{

    public Ingredient(IngredientID id) {
        this.id = id;
    }

    public IngredientID id;
    public bool cooked = false;
    public bool prepared = false;

    public override bool Equals(System.Object obj) {
        if (obj == null || obj.GetType() != this.GetType()) {
            return false;
        }

        Ingredient thatIngred = obj as Ingredient;
        return (this.id == thatIngred.id) && (this.cooked = thatIngred.cooked) && (this.prepared == thatIngred.prepared); 

    }

}
