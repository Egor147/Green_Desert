using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Koshelek : MonoBehaviour
{
    public static int Balance, Income;
    public int StartMoney;
    public float IncomePeriud;
    public GameObject RashodObject;
    static Text BalOut, IncOut;

    void Start()
    {
        Text[] t = GetComponentsInChildren<Text>();
        foreach(Text i in t)
            if(i.name == "Balance")
                BalOut = i;
            else
                IncOut = i;
        Balance = StartMoney;
        Vivod();
        StartCoroutine(Dohod());
    }

    public static void Vivod()
    {  
        BalOut.text = string.Format("${0}",Balance);
        if(Income > 0)
            IncOut.text = string.Format("+{0}",Income);
        else
            IncOut.text = "";
    }

    public static void Rashod(int Price)
    {
        Koshelek Koshel = GameObject.FindGameObjectWithTag("Canvas").GetComponentInChildren<Koshelek>();
        GameObject spawn = Koshel.RashodObject;
        Vector2 popravka = Koshel.gameObject.transform.position;
        popravka = new Vector2(popravka.x-100, popravka.y+100);
        Balance -= Price;
        Vivod();
        GameObject Ubil = Instantiate(spawn, Koshel.gameObject.transform.position, Quaternion.identity);
        Ubil.GetComponent<Text>().text = string.Format("-{0}",Price);
        Ubil.transform.SetParent(Koshel.transform);
        Ubil.GetComponent<Animation>().Play();
        Destroy(Ubil, 1);
    }

    IEnumerator Dohod()
    {
        yield return new WaitForSeconds(IncomePeriud);
        Balance += Income;
        Vivod();
        StartCoroutine(Dohod());
    }
}
