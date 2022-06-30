using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oasis : MonoBehaviour
{
    void Start()
    {   
        StartCoroutine(Delayer());
    }

    IEnumerator Delayer()
    {
        yield return new WaitForFixedUpdate();
        MouseCliker MusClik = gameObject.GetComponent<MouseCliker>();
        int x = MusClik.PosX;
        int y = MusClik.PosY;

        for(int i=x-1; i<=x+1; i++)
            for(int j=y-1; j<=y+1; j++)
                if((i>=0 && i<MapKeeper.CellCount) && (j>=0 && j < MapKeeper.CellCount))
                    MapKeeper.MapCells[i, j].GetComponent<MouseCliker>().Irrigating(1);
    }
}
