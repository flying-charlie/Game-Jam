using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoreController : TileController
{
    protected override void OnMouseDrag()
    {

    }

    protected override void OnStartDrag()
    {

    }

    protected override void OnEndDrag()
    {

    }

    void OnDestroy()
    {
        GameObject.FindGameObjectWithTag("scoreManager").GetComponent<ScoreManager>().GameOver();
    }
}
