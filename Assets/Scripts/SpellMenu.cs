using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellMenu : MonoBehaviour
{
    Button btn_spell;

    // Start is called before the first frame update
    private void Start()
    {
        btn_spell = GameObject.Find("btn_spell").GetComponent<Button>();
        btn_spell.onClick.AddListener(() =>
        {
            MsgBox.ShowMsg(() =>
            {

            });

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
