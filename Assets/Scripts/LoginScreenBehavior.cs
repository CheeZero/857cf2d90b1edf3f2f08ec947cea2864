using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;
using System.Net.Mail;
using Newtonsoft.Json;

class LoginForm
{
    public string login;
    public string password;
}
class RegistrationForm
{

    public string login;
    public string email;
    public string password;
}
public class LoginScreenBehavior : MonoBehaviour
{

    [SerializeField] Button Login;
    [SerializeField] Button Registration;
    [SerializeField] Button Close;
    [SerializeField] Text RegLogin;
    [SerializeField] Text LoginToLogin;
    [SerializeField] Text PasswordToLogin;
    [SerializeField] Text RegPassword;
    [SerializeField] Text RegEmail;
    [SerializeField] Text RegConPassword;
    [SerializeField] GameObject EmailToReg;
    public static SocketIOComponent socket;
    LoginForm LogForm;
    RegistrationForm RegForm;
    bool Connected = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject G = GameObject.Find("SocketIO");
        socket = G.GetComponent<SocketIOComponent>();
        socket.On("open", OpenConnection);
        socket.On("close", CloseConnection);
        socket.On("ReceiveLogin", RecieveLogin);
        socket.On("ReceiveRegistration", RecieveRegistration);
        socket.On("ReceiveLoginData", RecieveLoginData);
        Login.onClick.AddListener(ClickLoginButton);
        Registration.onClick.AddListener(ClickRegistrationButton);
        Close.onClick.AddListener(ClickCloseButton);
        RegForm = new RegistrationForm();
        LogForm = new LoginForm();
    }

    // Update is called once per frame
    void Update()
    {
        if (!socket.IsConnected && Connected == false)
            socket.Connect();
    }
    private void OpenConnection(SocketIOEvent e)
    {
        Connected = true;
        Debug.Log("XDOpen");
    }
    private void CloseConnection(SocketIOEvent e)
    {
        socket.Close();
        Connected = false;
    }
    private void RecieveLogin(SocketIOEvent e)
    {
        Debug.Log(e);
        //JsonUtility.FromJsonOverwrite(e.data.ToString(), test);
        Dictionary<string, int> values = JsonConvert.DeserializeObject<Dictionary<string, int>>(e.data.ToString());
        Player._Health = values["_Health"];
        Player._Crystals = values["_Crystals"];
        Player._Emeralds = values["_Emeralds"];
        Player._GoldCoins = values["_GoldCoins"];
        Player._Experience = values["_Experience"];
        Player._LevelPassed = values["_LevelPassed"];
        Debug.Log(Player._Health);
        Debug.Log(Player._Crystals);
        Debug.Log(Player._Emeralds);
        Debug.Log(Player._GoldCoins);
        Debug.Log(Player._Experience);
        Debug.Log(Player._LevelPassed);
        if (e.data != null)
            Destroy(GameObject.Find("Log&Reg"));

    }
    private void RecieveRegistration(SocketIOEvent e)
    {
        Debug.Log(e);
    }
    private void RecieveLoginData(SocketIOEvent e)
    {
        Debug.Log(e);
    }
    private void SuccessLogin(SocketIOEvent e)
    {

    }
    private void SuccessRegistration(SocketIOEvent e)
    {

    }
    private bool CheckLogin(Text myText)
    {
        if (myText.GetComponentInParent<InputField>().text == "")
            return false;
        return true;
    }
    private bool CheckPass()
    {
        if (RegConPassword.GetComponentInParent<InputField>().text == "" || RegPassword.GetComponentInParent<InputField>().text == "")
            return false;
        if (RegPassword.GetComponentInParent<InputField>().text == RegConPassword.GetComponentInParent<InputField>().text)
        {
            Debug.Log("pass correct");
            //RegForm.Password = RegPassword.text;
            return true;
        }
        Debug.Log("pass inccorect");
        return false;
    }
    private bool CheckForm()
    {
        if (CheckEmail())
            return true;
        return false;
    }
    private bool CheckEmail()
    {
        try
        {
            MailAddress MyMail = new MailAddress(RegEmail.GetComponentInParent<InputField>().text);
            //RegForm.Email = RegEmail.text;
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e.GetType());
            return false;
        }
    }
    public void ClickRegistrationButton()
    {
        //GameObject test = ;
        // Debug.Log(RegEmail.GetComponentInParent<InputField>().text);
        if (!CheckEmail())
            return;
        Debug.Log("email correct");
        RegForm.email = RegEmail.GetComponentInParent<InputField>().text;
        Debug.Log(RegForm.email);
        if (!CheckPass())
            return;
        RegForm.password = RegPassword.GetComponentInParent<InputField>().text;
        Debug.Log("correct pass");
        //!CheckLogin() ? return; : Debug.Log("passed all test");
        if (!CheckLogin(RegLogin))
            return;
        RegForm.login = RegLogin.GetComponentInParent<InputField>().text;
        Debug.Log("all test passed");
        socket.Emit("register", new JSONObject(JsonUtility.ToJson(RegForm)));
    }
    public void ClickLoginButton()
    {
        if (CheckLogin(LoginToLogin))
            LogForm.login = LoginToLogin.GetComponentInParent<InputField>().text;
        if (PasswordToLogin.GetComponentInParent<InputField>().text != "")
            LogForm.password = PasswordToLogin.GetComponentInParent<InputField>().text;
        Debug.Log("LogClicked");
        socket.Emit("login", new JSONObject(JsonUtility.ToJson(LogForm)));
    }
    public void ClickCloseButton()
    {
        Destroy(GameObject.Find("Log&Reg"));
        Debug.Log("CloseClicked");
    }
}
