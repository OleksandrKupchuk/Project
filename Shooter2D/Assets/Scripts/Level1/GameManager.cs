using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager instanceGameManager;

    [SerializeField] GameObject gemsPrefab;

    [SerializeField] Text gemsText;

    int collectedGems;
    public static GameManager InstanceGameManager
    {
        get
        {
            if(instanceGameManager == null)
            {
                instanceGameManager = FindObjectOfType<GameManager>();
            }
            return instanceGameManager;
        }

        set 
        {
            instanceGameManager = value;
        }
    }

    public GameObject GemsPrefab { get => gemsPrefab;}
    public int CollectedGems 
    {
        get 
        {
            return collectedGems;
        }
        set
        {
            gemsText.text = value.ToString();
            collectedGems = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
