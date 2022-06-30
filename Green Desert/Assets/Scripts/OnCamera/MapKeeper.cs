using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapKeeper : MonoBehaviour
{
    // Скрипт должен висеть на главной камере!
    // Отвечает за хранение карты и её рендеринг
    public const int CellCount = 8; // Размер игрового поля в клетках
    const float CellSize = 1; // Размер одной клетки поля
    public static int [,] MapIDs = new int [CellCount, CellCount]; // Массив ID клеток игрового поля
    public static int [,] MapIrrig = new int [CellCount, CellCount]; // Орашение клеток
    public static GameObject [,] MapCells = new GameObject [CellCount, CellCount]; // Массив клеток игрового поля
    TouchMe PrefabsKeeper;
    
    void Awake()
    {
        PrefabsKeeper = gameObject.GetComponent<TouchMe>();
        for(int i=0; i<CellCount; i++)
            for(int j=0; j<CellCount; j++)
                MapIrrig[i,j]=0;

        GameObject [] Cells = GameObject.FindGameObjectsWithTag("MapCell"); // Заносим в массив все клетки поля
        float MinX = Cells[0].transform.position.x; // Левая граница игрового поля  в координатах
        float MaxX = Cells[0].transform.position.x; // Правая граница игрового поля в координатах
        float MinY = Cells[0].transform.position.y; // Нижняя граница игрового поля в координатах
        float MaxY = Cells[0].transform.position.y; // Верхняя граница игрового поля в координатах

        for(int i=0; i<Cells.Length; i++) // Ищем координаты угловых клеток поля
        {
            // Подравнивает координаты клеток чтобы они распологались ровно там где нужно
            float PosX = Mathf.RoundToInt(Cells[i].transform.position.x + CellSize/2) - CellSize/2;
            float PosY = Mathf.RoundToInt(Cells[i].transform.position.y + CellSize/2) - CellSize/2;
            Cells[i].transform.position = new Vector3(PosX, PosY, 0);
            
            float x = Cells[i].transform.position.x;
            float y = Cells[i].transform.position.y;

            if(x < MinX)
                MinX = x;
            else if(x > MaxX)
                MaxX = x;

            if(y < MinY)
                MinY = y;
            else if(y > MaxY)
                MaxY = y;
        }
        
        for(int i=0; i<Cells.Length; i++) // Заносим клетки поля в массив Map
        {
            MouseCliker CurCell = Cells[i].GetComponent<MouseCliker>();
            int x = Mathf.RoundToInt(Mathf.Abs( (Cells[i].transform.position.x - MinX) / CellSize)); // Ищем место объекта на карте по горизонтали
            int y = Mathf.RoundToInt(Mathf.Abs( (Cells[i].transform.position.y - MinY) / CellSize)); // Ищем место объекта на карте по вертикали
            MapIDs[x, y] = CurCell.ID; // Записываем ID объекта в массив
            MapCells[x, y] = Cells[i]; // Записываем клетку поля в массив
            CurCell.PosX = x; // Передаём объкту его место на карте по горизонтали
            CurCell.PosY = y; // Передаём объкту его место на карте по вертикали
            CurCell.GetComponent<SpriteRenderer>().sortingOrder = CellCount - y;
        }

        PrefabsKeeper.LeftBord = MinX-CellSize/2; // Передаём границы поля в TouchMe
        PrefabsKeeper.RightBord = MaxX+CellSize/2;
        PrefabsKeeper.BotBord = MinY-CellSize/2;
        PrefabsKeeper.TopBord = MaxY+CellSize/2;
    }

    public static void CellRendering(int x, int y, GameObject NewCell) // Обновляет клетку карты
    {
        Vector3 CellPosition = MapCells[x, y].GetComponent<Transform>().position; // Координаты изменяемой клетки
        if(MapCells[x, y].GetComponent<Destroying>()!=null)
            Destroying.PreDestroy(MapIDs[x, y], x, y);
        GameObject.Destroy(MapCells[x, y], 0); // Удаление старой клетки

        MapCells[x, y] = GameObject.Instantiate(NewCell, CellPosition, Quaternion.identity); // Спавн новой клетки
        MapCells[x, y].GetComponent<MouseCliker>().PosX = x; // Передача новой клетке её позиции на поле
        MapCells[x, y].GetComponent<MouseCliker>().PosY = y;
        MapIDs[x, y] = MapCells[x, y].GetComponent<MouseCliker>().ID;
        MapCells[x, y].GetComponent<SpriteRenderer>().sortingOrder = CellCount - y;
    }
}
