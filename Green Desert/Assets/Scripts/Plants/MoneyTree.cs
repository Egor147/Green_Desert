using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyTree : MonoBehaviour
{
    public int Income;

    void Start()
    {
        Koshelek.Income += Income;
        Koshelek.Vivod();
    }
}
