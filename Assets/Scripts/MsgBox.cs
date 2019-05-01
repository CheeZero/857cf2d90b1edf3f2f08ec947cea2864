using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MsgBox : MonoBehaviour
{
    public static MsgBox instance;
    public GameObject SpellsI;
    public GameObject SpellsA;
    /*public GameObject SpellsF;
    public GameObject SpellsG; */
    void Awake()
    {
        instance = this;
    }

    public static void ShowMsg(Action action)
    {
        GameObject IceSpells = Instantiate(instance.SpellsI);
        GameObject AirSpells = Instantiate(instance.SpellsA);
        /*GameObject FireSpells = Instantiate(instance.SpellsF);
        GameObject GroundSpells = Instantiate(instance.SpellsG);
        */
        Transform panel = IceSpells.transform.Find("Panel");
        Transform panel1 = AirSpells.transform.Find("Panel1");
        /*Transform panel = FireSpells.transform.Find("Panel");
        Transform panel = GroundSpells.transform.Find("Panel");
        */
        Button close = panel.Find("Close").GetComponent<Button>();
        Button air = panel.Find("Air_btn").GetComponent<Button>();
        /*Button ice = panel.Find("Ice_btn").GetComponent<Button>();
        Button fire = panel2.Find("Fire_btn").GetComponent<Button>();
        Button ground = panel3.Find("Ground_btn").GetComponent<Button>();
        */
        close.onClick.AddListener(() =>
        {
            Destroy(IceSpells);
            Destroy(AirSpells);
            /*Destroy(FireSpells);
            Destroy(GroundSpells);*/
        });

        air.onClick.AddListener(() =>
        {
            MsgBox.ShowMsg(() =>
            {

            });
            Destroy(IceSpells);
        });

        /*  fire.onClick.AddListener(() =>
        {
            Destroy(FireSpells);
        });


        ice.onClick.AddListener(() =>
        {
            Destroy(IceSpells);
        });

        ground.onClick.AddListener(() =>
        {
            Destroy(IceSpells);
        });*/
    }
}
