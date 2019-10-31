using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] int levelIndex;

    [SerializeField] Material[] materials;
    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void LoadSelectedLevel()
    {
        SceneManager.LoadSceneAsync(levelIndex);
    }

    private void Update()
    {
        if (GetComponent<Button>().interactable)
        {
            image.material = materials[1];
        }
    }
}
