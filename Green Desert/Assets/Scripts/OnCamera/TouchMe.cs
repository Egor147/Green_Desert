using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMe : MonoBehaviour
{
    // Скрипт должен висеть на главной камере!
    // Обрабатывает нажатия игрока по экрану и игровому полю. Хранит в себе префабы и ID всех объектов в игре
    public const int EmptyHand = -1, FreeMode = 0;
    const int Plant = 1;
    public static int SandID, EarthID;
    public int ID = EmptyHand; // ID объекта что мы выбрали - говорит что это за объект
    public GameObject ChosenObject;
    public int Action = 0; // Возможное действие игрока
    public int X, Y; // Позиция выбранного объекта
    public GameObject [] IDObjects; // Массив с префабами всех игровых объектов
    public static string [] IDNames; // Для проверки тайлов поля другими скриптами создан массив IDNames.
    // Сравненивается имя нужного объекта со строкой записанной в массив по указанному ID
    [HideInInspector]
    public float LeftBord, RightBord, TopBord, BotBord; // Границы игрового поля
    int IDCount; // Размер массива
    MapKeeper MapRender; // Переменная для обращения к компоненту MapKeeper

    void Awake()
    {
        IDCount = IDObjects.Length;
        IDNames = new string [IDCount];
        MapRender = gameObject.GetComponent<MapKeeper>();
        
        for(int i=0; i<IDCount; i++) // Заполнение массива IDNames именами префабов и меняет ID префабов
        {
            IDObjects[i].GetComponent<MouseCliker>().ID = i;
            IDNames[i] = IDObjects[i].name;
            if(IDObjects[i].name == "Sand")
                SandID = i;
            if(IDObjects[i].name == "Earth")
                EarthID = i;
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            ChoseAction();
    }

    // Отвечает за выбор действия
    void ChoseAction()
    {
        switch(Action)
        {
            case Plant:
                if(InField(Input.mousePosition))
                    IDObjects[ID].GetComponent<PlantingCheck>().Planting(X, Y, IDObjects[ID]);
            break;
        }
    }

    // Проверяет было ли нажатие на поле
    bool InField(Vector3 TouchPos)
    {
        Camera cam = gameObject.GetComponent<Camera>();
        TouchPos = cam.ScreenToWorldPoint(TouchPos);
        bool flag = true;
        if(TouchPos.x <= LeftBord || TouchPos.x >= RightBord)
            flag = false;
        if(TouchPos.y <= BotBord || TouchPos.y >= TopBord)
            flag = false;
        return flag;
    }
}
