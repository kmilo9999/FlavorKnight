using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileToggle : MonoBehaviour
{
    public List<TileBase> active;
    public TileBase inactive;
    public TileBase tmp;
    public float animationSpeed;
    private float currentTime;
    private int animationIndex;
    private Tilemap tm;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        animationIndex = 0;
        tm = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            
            tm.SwapTile(inactive, tmp);
            foreach (TileBase t in active) {
                tm.SwapTile(t, inactive);
            }
            tm.SwapTile(tmp, active[0]);
        }
    }
    void FixedUpdate() {
        if (currentTime > animationSpeed) {
            TileBase current = active[animationIndex];
            currentTime = 0;
            animationIndex++;
            animationIndex = animationIndex % active.Count;
            tm.SwapTile(current, active[animationIndex]);
        }

        currentTime = currentTime + Time.deltaTime;
    }
}
