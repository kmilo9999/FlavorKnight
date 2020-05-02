using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    up, down, left, right
}

[System.Serializable]
public enum PlayerState {
    standing, moving, attacking, standCarrying, moveCarrying, dead, grabbing, pushing
}
