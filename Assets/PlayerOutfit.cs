using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerOutfit : MonoBehaviour
{
    public static PlayerOutfit _PlayerOutfitInstance;

    private void Awake()
    {
        if (_PlayerOutfitInstance == null)
        {
            _PlayerOutfitInstance = this;
            DontDestroyOnLoad(this);
        }
        else if (_PlayerOutfitInstance != this)
        {
            Destroy(gameObject);
        }
    }
}
