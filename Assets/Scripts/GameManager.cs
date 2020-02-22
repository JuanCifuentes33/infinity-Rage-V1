using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public PlayerController pController;
    public FlowerController fController;
    //para guardar la posicion del checkpoint
    public Transform ultimoCheckpoint;
    //para saber si cayo antes de las plataformas que se caen
    public bool firstCollision;

    //BOOL PARA SABER SI EL JUGADOR HA MUERTO
    public bool isPlayerDead;
    //FLOAT DE LA VIDA DEL JUGADOR
    public float playerHealth;

    public float flowerHealth;
    //bool para saber si ya esta en la zona del boss y asi agrandar la cam
    public bool isWithBoss;

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
          //  SceneManager.LoadScene("GameScene");
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //PONE LA SALUD DEL PLAYER A 100
      //  playerHealth = pController.pModel.lifePoints;
        
        //EL JUGADOR NO ESTÁ MUERTO
        isPlayerDead = false;

        isWithBoss = false;

        firstCollision = false;
      //  flowerHealth = fController.fModel.lifePoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (firstCollision)
            print("true");
    }
    //FUNCIÓN QUE RESETEA LOS VALORES INICIALES DEL JUGADOR
    public void RestartGame()
    {
        //EL JUGADOR NO ESTÁ MUERTO
        isPlayerDead = false;
        //PONE LA SALUD DEL PLAYER A 100
        playerHealth = pController.pModel.lifePoints;
        
    }
}

