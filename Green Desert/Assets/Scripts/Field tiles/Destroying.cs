using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroying : MonoBehaviour
{
    public static void PreDestroy(int ID, int x, int y)
    {
        TouchMe touchMe = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TouchMe>();

        switch(TouchMe.IDNames[ID])
        {
            case "MoneyTree":
                Koshelek.Income-=touchMe.IDObjects[ID].GetComponent<MoneyTree>().Income;
                Koshelek.Vivod();
            break;

            case "Oasis":
                for(int i=x-1; i<=x+1; i++)
                    for(int j=y-1; j<=y+1; j++)
                        if((i>=0 && i<MapKeeper.CellCount) && (j>=0 && j < MapKeeper.CellCount))
                            MapKeeper.MapCells[i, j].GetComponent<MouseCliker>().Irrigating(-1);
            break;
        }
    }
}
