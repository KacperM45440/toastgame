using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinigameKnifeController : MonoBehaviour
{
    [SerializeField] private MainGameController gameController;
    [SerializeField] private PlayerScore scoreRef;
    [SerializeField] private KnifeScript knifeRef;
    [SerializeField] private GameObject minigameComponents;
    [SerializeField] private GameObject slicedOffParent;
    [SerializeField] private GameObject breadOutline;
    [SerializeField] private GameObject UIHolder;
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private List<GameObject> breads = new List<GameObject>();

    private int score = 0;
    private int breadIndex = 0;

    public void SetupMinigame()
    {
        StartCoroutine(SetupAsync());
    }

    public void StartMinigame()
    {
        UIHolder.SetActive(true);
        SpawnNextBread();
    }

    public void FinishMinigame()
    {
        UIHolder.SetActive(false);
        knifeRef.LoseControl();
        scoreRef.AddScore(score);
        gameController.FinishedMinigame();
    }

    public void UnloadMinigame()
    {
        StartCoroutine(UnloadAsync());
    }

    public void GetPoints(int points)
    {
        score += points;
        scoreText.text = score.ToString();

        if(breadIndex >= breads.Count)
        {
            FinishMinigame();
            return;
        }

        SpawnNextBread();
    }

    public void SpawnNextBread()
    {

        if(breadIndex >= breads.Count)
        {
            return;
        }

        GameObject bread = breads[breadIndex];
        StartCoroutine(MoveBreadIntoSpot(bread));
        breadIndex++;
    }

    private IEnumerator MoveBreadIntoSpot(GameObject bread)
    {
        if(breadIndex > 0)
        {
            yield return new WaitForSeconds(1.5f);
        }

        bread.SetActive(true);
        Rigidbody rbRef = bread.GetComponent<Rigidbody>();

        while (Vector3.Distance(bread.transform.position, Vector3.zero) > 0.1f)
        {
            yield return null;
            rbRef.MovePosition(Vector3.Lerp(bread.transform.position, Vector3.zero, Time.deltaTime * 5f));
        }
        bread.transform.localPosition = Vector3.zero;
        rbRef.useGravity = true;

        Vector3 outlineDefaultPos = breadOutline.transform.localPosition;
        breadOutline.transform.localPosition = new Vector3(Random.Range(-1.5f, 1.5f), outlineDefaultPos.y, outlineDefaultPos.z);
        breadOutline.SetActive(true);

        knifeRef.GainControl(bread);
    }

    private IEnumerator SetupAsync()
    {
        minigameComponents.gameObject.SetActive(true);

        yield return null;
        gameController.MinigameLoaded = true;
    }

    private IEnumerator UnloadAsync()
    {
        yield return new WaitForSeconds(2f);
        minigameComponents.gameObject.SetActive(false);
        slicedOffParent.gameObject.SetActive(false);
    }
}
