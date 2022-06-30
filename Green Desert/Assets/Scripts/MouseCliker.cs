using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCliker : MonoBehaviour
{
    public int ID, PosX, PosY;
    GameObject GlobalScripts; // Глобальные скрипты висят на главной камере
    GameObject Earth, Sand;

    void Start()
    {
        GlobalScripts = GameObject.FindGameObjectWithTag("MainCamera"); // Поиск главной камеры
        Sand = GlobalScripts.GetComponent<TouchMe>().IDObjects[TouchMe.SandID];
        Earth = GlobalScripts.GetComponent<TouchMe>().IDObjects[TouchMe.EarthID];
        Irrigating(0);
    }

    void OnMouseDown()
    {
        if(gameObject.tag == "MapCell")
        {
            GlobalScripts.GetComponent<TouchMe>().X = PosX;
            GlobalScripts.GetComponent<TouchMe>().Y = PosY;
        }
        else
            GlobalScripts.GetComponent<TouchMe>().ChosenObject = gameObject;
    }    
    
    public void Irrigating(int Vlagnost)
    {
        MapKeeper.MapIrrig[PosX, PosY] += Vlagnost;
        switch (TouchMe.IDNames[ID])
        {
            case "Sand":
                if(MapKeeper.MapIrrig[PosX, PosY]>0)
                    MapKeeper.CellRendering(PosX, PosY, Earth);
            break;

            case "Earth":
                if(MapKeeper.MapIrrig[PosX, PosY]<=0)
                    MapKeeper.CellRendering(PosX, PosY, Sand);
            break;
        }
    }
}
