using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{

    public void CargarEscena(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void CargarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
    //public void Jugar()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}

    //public void Salir()
    //{
    //    Debug.Log("Salir...");
    //  Application.Quit();
    //}

}
