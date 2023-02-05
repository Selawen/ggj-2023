using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Tree : MonoBehaviour
{
    public List<Sprite> treeStages;

    public float growTime;

    LevelManager manager;
    Image currentImage;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType(typeof(LevelManager)) as LevelManager;
        currentImage = GetComponent<Image>();

        StartCoroutine(GrowTree());
    }


    private IEnumerator GrowTree()
    {
        for (int x =0; x < manager.completedLevels.Count; x++)
        {
            yield return new WaitForSeconds(growTime);
            currentImage.sprite = treeStages[x];
        }
    }
}
