using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoginPanel : MonoBehaviour
{
    public InputField m_Username;
    public InputField m_Password;
    public Button m_Login;
    public Button m_Delete;
    public Text m_Status;

    public void Login(string username, string password)
    {
        if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            m_Status.text = "Please make sure that you fill in your username and password";
            return;
        }
        Debug.Log(string.Format("{0} {1}", username, password));
        m_Status.text = string.Format("Welcome {0}!", username.Split("@"[0]));
        Invoke("Reset", 1f);
    }

    public void Reset()
    {
        m_Status.text = m_Username.text = m_Password.text = "";
    }

    private void OnEnable()
    {
        m_Login.onClick.AddListener(() => { Login(m_Username.text, m_Password.text); });
        m_Delete.onClick.AddListener(() => { Reset(); });
    }

    private void OnDisable()
    {
        m_Login.onClick.RemoveListener(() => { Login(m_Username.text, m_Password.text); });
        m_Delete.onClick.RemoveListener(() => { Reset(); });
    }

}
