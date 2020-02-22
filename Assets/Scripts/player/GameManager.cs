using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [HideInInspector] public PlayerController pController;



    //BOOL PARA SABER SI EL JUGADOR HA MUERTO
    [HideInInspector]public bool isPlayerDead;
    //FLOAT DE LA VIDA DEL JUGADOR
    [HideInInspector] public float playerHealth;
    //PUNTODE DE MANA DEL JUGADOR
    [HideInInspector] public float mana;
   
    

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);

            //EL MAGEMANAGER ESTÁ SITUADO EN EL PRELOAD SCENE, PERO TE MANDA DIRECTAMENTE AL MENÚ PRINCIPAL
            SceneManager.LoadScene("EscenaDePrueba");
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //EL JUGADOR NO ESTÁ MUERTO
        isPlayerDead = false;
      
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    //FUNCIÓN QUE RESETEA LOS VALORES INICIALES DEL JUGADOR
    public void RestartGame()
    {
        //EL JUGADOR NO ESTÁ MUERTO
        isPlayerDead = false;
        
    }
}

