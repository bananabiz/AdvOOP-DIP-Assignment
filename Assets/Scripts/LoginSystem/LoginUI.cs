using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//below 5 lines allow you to send email to anyone
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class LoginUI : MonoBehaviour
{
    public Text loginUsername, loginPassword;
    public Text inputUsername, inputPassword;
    public Text inputEmail, inputRetrieveEmail;
    public Text inputConfirmPass;
    public Text checkUser, checkEmail, checkPassword, checkConfirmPass;
    public Text registerResult;
    public GameObject loginPanel, createUserPanel, forgetPasswordPanel;

    private string notify = "";
    private WWW www;
    private WWWForm user;
    public string userName, email, passWord, confirmPassword, inputRetrieve;
    public string status;

    void GetAccountInfo()
    {
        userName = inputUsername.text;
        email = inputEmail.text;
        passWord = inputPassword.text;
        confirmPassword = inputConfirmPass.text; 
        Debug.Log(userName);
    }

    void Update()
    {
        
    }
    
    IEnumerator LoginToDataBase(string username, string password)
    {
        string loginURL = "http://localhost/loginsystem/login.php";
        WWWForm loginForm = new WWWForm();
        loginForm.AddField("username_Post", username);
        loginForm.AddField("password_Post", password);
        WWW www = new WWW(loginURL, loginForm);
        yield return www;

        status = www.text;
        if(www.text == "Login Success")
        {
            Debug.Log("Load");
            SceneManager.LoadScene(1);
        }
    }
    
    IEnumerator CreateAccount(string username, string email, string password, string confirmPassword)
    {
        string createUserURL = "http://localhost/loginsystem/insertuser.php";
        user = new WWWForm();

        if (username == "")
        {
            checkUser.text = "Please fill in the blank";
        }
        if (email == "")
        {
            checkEmail.text = "Please fill in the blank";
        }
        if (password == "")
        {
            checkPassword.text = "Please fill in the blank";
        }
        if (confirmPassword == "")
        {
            checkConfirmPass.text = "Please fill in the blank";
        }

        if (username != "" && email != "" && password != "" && password == confirmPassword)
        {
            user.AddField("username_Post", username);
            user.AddField("email_Post", email);
            user.AddField("password_Post", password);
            www = new WWW(createUserURL, user);
            yield return www;
            Debug.Log(www.text);

            if (www.text == "User Already Exists")
            {
                checkUser.text = "Your username has been registered.";
            }
            else
            {
                checkUser.text = "V";
            }
            if (www.text == "Email Already Exists")
            {
                checkEmail.text = "Your email has been registered.";
            }
            else
            {
                checkEmail.text = "V";
            }
            if (www.text == "Password Already Exists")
            {
                checkPassword.text = "Your password has been registered.";
            }
            else
            {
                checkPassword.text = "V";
            }

            if (confirmPassword == passWord)
            {
                checkConfirmPass.text = "V";
            }
            else
            {
                checkConfirmPass.text = "Input Not Correct";
            }
            if (www.text != "User Already ExistsEmail Already Exists" && www.text != "Complete Sections")
            {
                registerResult.text = "Register successful, go back and sign in.";
                inputUsername.text = "";
                inputEmail.text = "";
                inputPassword.text = "";
                inputConfirmPass.text = "";
            }
            else
            {
                registerResult.text = "Username or Password already exist";
            }
        }
        else
        {
            Debug.Log("Complete Sections");
        }
    }

    public void Login()  //button
    {
        StartCoroutine(LoginToDataBase(loginUsername.text.ToString(), loginPassword.text.ToString()));
        
    }
    
    public void Submit()  //button
    {
        Debug.Log("Sending");
        GetAccountInfo();
        StartCoroutine(CreateAccount(userName, email, passWord, confirmPassword));
    }
    
    public void Register()  //button
    {
        loginPanel.SetActive(false);
        createUserPanel.SetActive(true);
        forgetPasswordPanel.SetActive(false);
        
        registerResult.text = "";
        checkUser.text = "";
        checkEmail.text = "";
        checkPassword.text = "";
        checkConfirmPass.text = "";  
    }

    public void ForgetPassword()  //button
    {
        loginPanel.SetActive(false);
        createUserPanel.SetActive(false);
        forgetPasswordPanel.SetActive(true);
    }

    public void Back()  //button
    {
        loginPanel.SetActive(true);
        createUserPanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
    }

    /*
    private void OnGUI()
    {
        float scrH = Screen.height / 9;
        float scrW = Screen.width / 16;

        if (notify != "")
        {
            GUI.Box(new Rect(0, 0, Screen.width, scrH*0.75f), notify);
        }

        if (GUI.Button(new Rect(scrW, scrH, scrW, scrH), "Send"))
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("sqlunityclasssydney@gmail.com");
            mail.To.Add(email);
            mail.Subject = "Reset Account";
            mail.Body = "Hi, reset Here.";
                                        //simple mail transfer protocol
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = 25;
            smtpServer.Credentials = new NetworkCredential("sqlunityclasssydney@gmail.com", "sqlpassword") as ICredentialsByHost;
            smtpServer.EnableSsl = true;

            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
            { return true; };
            
            smtpServer.Send(mail);
            Debug.Log("Succeed to send");
        }
    } */

    public void Send()
    {
        MailMessage mail = new MailMessage();

        inputRetrieve = inputRetrieveEmail.text;
        mail.From = new MailAddress("sqlunityclasssydney@gmail.com");
        mail.To.Add(inputRetrieve);
        mail.Subject = "Reset Account";
        mail.Body = "Hi, reset Here.";
        //simple mail transfer protocol
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 25;
        smtpServer.Credentials = new NetworkCredential("sqlunityclasssydney@gmail.com", "sqlpassword") as ICredentialsByHost;
        smtpServer.EnableSsl = true;

        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
        { return true; };
        
        smtpServer.Send(mail);
        Debug.Log("Succeed to send");
    } 

}
