using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hren : MonoBehaviour
{
    public int Price;
    public string [] CanDig;
    public GameObject Sand;

    public void dig(int x, int y) 
    {
        TouchMe touchMe = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TouchMe>();

        foreach(string name in CanDig)
            if(TouchMe.IDNames[MapKeeper.MapIDs[x, y]] == name)
            {
                if(Price<=Koshelek.Balance)
                {
                    Destroying.PreDestroy(MapKeeper.MapIDs[x, y],x,y);
                    MapKeeper.CellRendering(x, y, Sand);
                    Koshelek.Rashod(Price);
                }
                break;
            }
    }
}
