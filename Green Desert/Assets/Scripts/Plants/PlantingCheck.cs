using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingCheck : MonoBehaviour
{
    public int Price;
    public string [] CanBePlantedOn;
    
    // Отвечает за проверку условий посадки
    public void Planting(int x, int y, GameObject Plant) 
    {
        foreach(string name in CanBePlantedOn)
            if(TouchMe.IDNames[MapKeeper.MapIDs[x, y]] == name)
            {
                if(Price<=Koshelek.Balance)
                {
                    MapKeeper.CellRendering(x, y, Plant);
                    Koshelek.Rashod(Price);
                }
                break;
            }
    }
}
