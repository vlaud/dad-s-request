using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : AIMovement
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1000.0f, 1 << LayerMask.NameToLayer("Ground")))
            {
                MoveToPositionByNav(hit.point);
            }
        }
    }
}
