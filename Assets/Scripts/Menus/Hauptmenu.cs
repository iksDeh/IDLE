using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Hauptmenu : MonoBehaviour
{
    public void OnContinue()
    {
        SceneManager.LoadScene("IDLE", LoadSceneMode.Single);
    }
}
