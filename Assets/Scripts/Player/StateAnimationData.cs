using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class StateAnimationData : ScriptableObject
{
    // Unity inspector hack
    [System.Serializable]
    public struct StateDirectionAnimation {
        public PlayerState state;
        public Direction direction;
        public string animation;
    };

    public StateDirectionAnimation[] animations;

    public Dictionary<Tuple<PlayerState, Direction>, string> animationDict;


    void Start() {
        Init();
    }

    void Init() {
        if (animationDict == null) {
            animationDict = new Dictionary<Tuple<PlayerState, Direction>, string>();
            foreach (StateDirectionAnimation sda in animations) {
                animationDict.Add(new Tuple<PlayerState, Direction>(sda.state, sda.direction), sda.animation);
            }
        }
    }

    public string GetName(PlayerState state, Direction direction) {
        Init();
        Tuple<PlayerState, Direction> key = new Tuple<PlayerState, Direction>(state, direction); 
        if (animationDict.ContainsKey(key)) {
            return animationDict[key];
        }
        Debug.LogWarningFormat("Did not find animation string for key {}", key);
        return null;
    }

}
