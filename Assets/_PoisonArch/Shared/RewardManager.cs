using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoisonArch;

using TMPro;
using DG.Tweening;
using System;

public enum StarCount_e
{
    OneStar = 1,
    TwoStar,
    ThreeStar
}
public class RewardManager : AbstractSingleton<AudioManager>
{
    [Header("Star")]
    public List<Transform> Stars;
    private StarCount_e StarCountEnum;
    public TMP_Text StarText;

    [Header("FlyStar")]
    public Transform FlyStarStartPos;
    public GameObject ItemStar;
    public Transform StarBarPos;
    public TMP_Text StarBarText;

    [Header("Coin & Gem")]
    public TMP_Text StarCountText;
    public TMP_Text GemCountText;
    int GemCount;

    const string k_StarCount = "StarCount";
    public int S_StarCount
    {
        get => PlayerPrefs.GetInt(k_StarCount);
        set => PlayerPrefs.SetInt(k_StarCount, value);
    }
    private void Awake()
    {
        //StarCountText.text = LoadStarData().ToString();
    }
    void Update()
    {
       GetStar(StarCount_e.TwoStar);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetFlyStar(10);
        }

    }
    public void GetStar(Enum _enum)
    {
        StartCoroutine(StarCoroutine(_enum));

        Sequence DCSeq = DOTween.Sequence().SetAutoKill(false);

        switch (StarCountEnum)
        {
            case StarCount_e.OneStar:
                StarText.text = "TEBRÝKLER!";
                S_StarCount += 1;
                StarCountText.text = S_StarCount.ToString();

                DCSeq.Append(Stars[0].GetComponent<RectTransform>().DOScale(new Vector3(1.1f, 1.1f, 1.1f), .4f));
                DCSeq.Append(Stars[0].GetComponent<RectTransform>().DOScale(Vector3.one, .2f));
                DCSeq.Append(StarText.GetComponent<RectTransform>().DOScale(Vector3.one, .5f));
                break;
            case StarCount_e.TwoStar:
                StarText.text = "BRAVO!";
                S_StarCount += 2;
                StarCountText.text = S_StarCount.ToString();

                DCSeq.Append(Stars[0].GetComponent<RectTransform>().DOScale(new Vector3(1.2f, 1.2f, 1.2f), .4f));
                DCSeq.Join(Stars[0].GetComponent<RectTransform>().DOScale(new Vector3(1f, 1f, 1f), .4f));
                DCSeq.Append(Stars[1].GetComponent<RectTransform>().DOScale(new Vector3(1.25f, 1.25f, 1.25f), .4f));
                DCSeq.Append(Stars[1].GetComponent<RectTransform>().DOScale(new Vector3(1.2f, 1.2f, 1.2f), .4f));
                DCSeq.Join(StarText.GetComponent<RectTransform>().DOScale(Vector3.one, .5f));
                break;
            case StarCount_e.ThreeStar:
                StarText.text = "HARÝKASIN!";
                S_StarCount += 3;
                StarCountText.text = S_StarCount.ToString();

                DCSeq.Append(Stars[0].GetComponent<RectTransform>().DOScale(new Vector3(1.2f, 1.2f, 1.2f), .4f));
                DCSeq.Join(Stars[0].GetComponent<RectTransform>().DOScale(Vector3.one, .4f));
                DCSeq.Append(Stars[1].GetComponent<RectTransform>().DOScale(new Vector3(1.25f, 1.25f, 1.25f), .4f));
                DCSeq.Join(Stars[1].GetComponent<RectTransform>().DOScale(new Vector3(1.2f, 1.2f, 1.2f), .4f));
                DCSeq.Append(Stars[2].GetComponent<RectTransform>().DOScale(new Vector3(1.1f, 1.1f, 1.1f), .4f));
                DCSeq.Append(Stars[2].GetComponent<RectTransform>().DOScale(Vector3.one, .2f));
                DCSeq.Append(StarText.GetComponent<RectTransform>().DOScale(Vector3.one, .5f));
                break;
        }

        SaveStarData(S_StarCount);
    }
    public void GetFlyStar(int goStar)
    {
        Sequence DCSeq = DOTween.Sequence().SetAutoKill(false);

        for (int i = 0; i < goStar; i++)
        {
            S_StarCount += i;
            StarCountText.text = S_StarCount.ToString();

            var flyStarIns = Instantiate(ItemStar, new Vector3(UnityEngine.Random.Range(FlyStarStartPos.position.x - 20f, FlyStarStartPos.position.x + 20f),
                UnityEngine.Random.Range(FlyStarStartPos.position.y - 25f, FlyStarStartPos.position.y + 25f), 0f), Quaternion.identity) as GameObject;
            flyStarIns.transform.SetParent(FlyStarStartPos);
            flyStarIns.transform.localScale = Vector3.one;

            DCSeq.Append(flyStarIns.transform.DOMove(StarBarPos.GetChild(2).position, .6f));
            DCSeq.Append(StarBarPos.GetChild(2).GetComponent<RectTransform>().DOScale(new Vector3(1.2f, 1.2f, 1.2f), .2f));
            DCSeq.Append(StarBarPos.GetChild(2).GetComponent<RectTransform>().DOScale(Vector3.one, .2f)).AppendCallback(() => {
                //print("fly: " + S_StarCount);
                //StarBarText.text = S_StarCount.ToString();
                Destroy(flyStarIns);
            });
        }

        SaveStarData(S_StarCount);
    }
    IEnumerator StarCoroutine(Enum _enum)
    {
        StarCountEnum = (StarCount_e)_enum;
        int _openCount = (int)Enum.ToObject(StarCountEnum.GetType(), StarCountEnum);

        for (int i = 0; i < _openCount; i++)
        {
            Stars[i].transform.GetChild(1).gameObject.SetActive(true);
            yield return new WaitForSeconds(.4f);
        }
    }

    public int LoadStarData()
    {
        return PlayerPrefsUtils.Read<int>(k_StarCount);
    }
    public void SaveStarData(int _starCount)
    {
        PlayerPrefsUtils.Write(k_StarCount, _starCount);
    }
}
