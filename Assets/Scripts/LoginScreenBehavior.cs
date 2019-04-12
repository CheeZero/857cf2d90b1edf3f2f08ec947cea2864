using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;
using System.Net.Mail;

class LoginForm
{
    public string Login;
    public string Password;
}
class RegistrationForm { 

    public string Login;
    public string Email;
    public string Password;
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
    SocketIOComponent socket;
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
        socket.On("RecieveLogin", RecieveLogin);
        socket.On("RecieveRegistration", RecieveRegistration);
        socket.On("SuccessLogin", SuccessLogin);
        socket.On("SuccessRegistration", SuccessRegistration);
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
    private void OpenConnection(SocketIOEvent e) {
        Connected = true;
        Debug.Log("XDOpen");
    }
    private void CloseConnection(SocketIOEvent e){
        socket.Close();
        Connected = false;
    }
    private void RecieveLogin(SocketIOEvent e){

    }
    private void RecieveRegistration(SocketIOEvent e){

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
        if(CheckEmail())
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
        RegForm.Email = RegEmail.GetComponentInParent<InputField>().text;
        Debug.Log(RegForm.Email);
        if (!CheckPass())
            return;
        RegForm.Password = RegPassword.GetComponentInParent<InputField>().text;
        Debug.Log("correct pass");
        //!CheckLogin() ? return; : Debug.Log("passed all test");
        if (!CheckLogin(RegLogin))
            return;
        RegForm.Login = RegLogin.GetComponentInParent<InputField>().text;
        Debug.Log("all test passed");
        socket.Emit("register", new JSONObject(JsonUtility.ToJson(RegForm)));
    }
    public void ClickLoginButton()
    {
        if (CheckLogin(LoginToLogin))
            LogForm.Login = LoginToLogin.GetComponentInParent<InputField>().text;
        if (PasswordToLogin.GetComponentInParent<InputField>().text != "")
            LogForm.Password = PasswordToLogin.GetComponentInParent<InputField>().text;
        Debug.Log("LogClicked");
        socket.Emit("login", new JSONObject(JsonUtility.ToJson(LogForm)));
    }
    public void ClickCloseButton()
    {
        Destroy(GameObject.Find("Log&Reg"));
        Debug.Log("CloseClicked");
    }
}
