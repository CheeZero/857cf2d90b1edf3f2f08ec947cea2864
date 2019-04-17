using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;
public class EventGameHandler : MonoBehaviour
{
    [SerializeField] GameObject a;
    [SerializeField] Button MyButton;
    // Start is called before the first frame update
    void Start()
    {
        //MyButton.onClick = OnMouseDown ;
    }

    private void OnMouseDown()
    {
        LoginScreenBehavior.socket.Emit("_Health : 40, _Crystals : 1, _GoldCoins : 35, _Emeralds : 10");
        Debug.Log("something emitted");
    }
    private void OnMouseUp()
    {
        LoginScreenBehavior.socket.Emit("_Health : 40, _Crystals : 1, _GoldCoins : 35, _Emeralds : 10");
        Debug.Log("something emitted");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
