using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private int levelIndex;
    [SerializeField] private LevelButton[] levelsToActivate;
    [SerializeField] private bool completed = false;
    [SerializeField] private float activationDelay = 0.5f;
    [SerializeField] private float activatedScale = 1f, unActivatedScale = 0.8f;
    [SerializeField] private Color completedColour = Color.green;

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
     
        if (GameObject.Find("LevelManager").GetComponent<LevelManager>().completedLevels.Contains(levelIndex))
        {
            completed = true;
            transform.localScale *= activatedScale;
        }

        if (levelIndex == 1)
        {
            transform.localScale *= activatedScale;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (completed)
        {
            GetComponent<Image>().color = completedColour;

            foreach (LevelButton g in levelsToActivate)
            {
                g.StartActivation();
            }
        } 
        else if (levelIndex != 1)
        {
            GetComponent<Button>().interactable = false;

            transform.localScale *= unActivatedScale;
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void StartActivation()
    {
        StartCoroutine(activateButton());
    }

    public IEnumerator activateButton()
    {
        yield return new WaitForSeconds(activationDelay);
        Button button = gameObject.GetComponent<Button>();
        button.interactable = true;
        StartCoroutine(ActivationEffect());
    }

    private IEnumerator ActivationEffect()
    {
        float scaleFactor = unActivatedScale;

        while (scaleFactor < activatedScale)
        {
            scaleFactor += Time.deltaTime;
            transform.localScale = originalScale * scaleFactor;
            yield return null;
        }

        transform.localScale = originalScale * activatedScale;
    }

    private void OnValidate()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = levelIndex.ToString();
    }
}
