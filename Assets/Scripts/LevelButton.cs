using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private int levelIndex;
    [SerializeField] private int rootDepth;
    [SerializeField] private LevelButton[] levelsToActivate;
    [SerializeField] private bool completed = false;

    [Header("visuals")]
    [SerializeField] private float activationDelay = 0.5f;
    [SerializeField] private float activatedScale = 1f, unActivatedScale = 0.8f;
    [SerializeField] private Color completedColour = Color.green;
    [SerializeField] private GameObject rootPrefab;
    
    
    private float rootWidth;

    private LevelManager manager;
    private Color rootStartColour, rootEndColour;

    private Vector3 originalScale;

    private void Awake()
    {
        rootWidth = 1.0f / rootDepth;

        manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        Color gradientStart = manager.rootColour.colorKeys[0].color;
        Color gradientEnd = manager.rootColour.colorKeys[1].color;

        originalScale = transform.localScale;
     
        if (manager.completedLevels.Contains(levelIndex))
        {
            completed = true;
            transform.localScale *= activatedScale;
        }

        float darkeningFraction = Mathf.Max(manager.completedLevels.Count, 1);
        if (levelIndex == 1)
        {
            if (completed)
            {
                rootStartColour = gradientStart;
                float t = 1.0f / (darkeningFraction);
                rootEndColour = Color.Lerp(rootStartColour, gradientEnd, t);
                GetComponent<Image>().color = completedColour;
            }

            transform.localScale *= activatedScale;
        }
        else
        {
            float startT;
            startT = 1.0f / (darkeningFraction + 1.0f);
            startT *= Mathf.Max(rootDepth, 1.0f);
            float endT;
            endT = 1.0f / (darkeningFraction) * Mathf.Max(rootDepth, 1.0f);

            rootStartColour = Color.Lerp(gradientStart, gradientEnd, startT);
            rootEndColour = Color.Lerp(gradientStart, gradientEnd, endT);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        if (levelIndex == 1)
        {
            StartActivation();
        } 
        else
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
        StartCoroutine(ActivationEffect());
        
        yield return new WaitForSeconds(activationDelay*0.5f);
        Button button = gameObject.GetComponent<Button>();
        button.interactable = true;

        yield return new WaitForSeconds(activationDelay * 0.5f);

        if (completed)
        {
            GetComponent<Image>().color = completedColour;

            foreach (LevelButton g in levelsToActivate)
            {
                g.StartActivation();
            }
        }
    }

    private IEnumerator ActivationEffect()
    {
        float scaleFactor = unActivatedScale;

        while (scaleFactor < activatedScale)
        {
            scaleFactor += Time.deltaTime/activationDelay;
            transform.localScale = originalScale * scaleFactor;
            yield return null;
        }

        transform.localScale = originalScale * activatedScale;

        if (completed)
        {
            foreach (LevelButton g in levelsToActivate)
            {
                GameObject newRoot = Instantiate(rootPrefab);
                RootSegment root = newRoot.GetComponent<RootSegment>();
                root.SetColour(rootStartColour, rootEndColour);

                root.Grow(transform.position, g.transform.position, activationDelay, rootWidth);
            }
        }
    }

    private void OnValidate()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = levelIndex.ToString();
    }
}
