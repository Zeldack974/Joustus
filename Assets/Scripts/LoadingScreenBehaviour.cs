using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBehaviour : MonoBehaviour
{
    public GameObject loader;
    public GameObject spritesReferences;

    void Awake()
    {
        DontDestroyOnLoad(loader);
        DontDestroyOnLoad(spritesReferences);
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
