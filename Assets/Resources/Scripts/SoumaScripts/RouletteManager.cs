using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteManager : MonoBehaviour {

    public Transform roulette;
    public GameObject plate;

    private int[] sizeList;

    private void Awake()
    {
        Init();
    }

    public void Reset()
    {
        Init();
    }

    public void Show()
    {
        StartCoroutine(ShowAnim());
    }

    //円グラフが表示される演出
    private IEnumerator ShowAnim()
    {
        bool flag = true;
        roulette.GetComponent<Image>().fillAmount = 0;
        float speed = 0.05f;
        while (flag)
        {
            roulette.GetComponent<Image>().fillAmount += speed;
            if (roulette.GetComponent<Image>().fillAmount >= 1) flag = false;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void Init()
    {
        //既に作成したグラフがあれば削除する
        foreach (Transform tran in roulette)
        {
            if (tran.name != "Plate") Destroy(tran.gameObject);
        }

        int kindCount = Random.Range(3,8);//最大の色の数 default:3～8
        sizeList = new int[kindCount];//割合のリスト
        int max = 100;//グラフの比率　100が最大 ここからどんどん引いていく
        for (int i = 0; i < kindCount; i++)
        {
            if (max <= 0) break;
            //Plateをコピーしてサイズなどを調整
            GameObject plateCopy = Instantiate(plate) as GameObject;
            plateCopy.transform.SetParent(roulette);
            plateCopy.transform.localPosition = Vector3.zero;
            plateCopy.transform.localScale = Vector3.one;

            //最後の一つの場合は残りのすべてをあてはめる
            if (i == kindCount - 1)
                sizeList[i] = max;
            else
            {
                int no = max;
                if (max > 40) no = 40;

                if (max < 10)
                    no = max;
                else
                {
                    sizeList[i] = Random.Range(1, no + 1);
                    if (max - sizeList[i] < 10) sizeList[i] = max;
                }
            }
            //zの角度を設定
            plateCopy.transform.localEulerAngles = new Vector3(0, 0, (100f - (float)max) / 100f * -360f);

            //円のサイズをfillAmountに設定
            plateCopy.GetComponent<Image>().fillAmount = (float)sizeList[i] / 100f;

            //色をランダムに設定　明るめにしている
            plateCopy.GetComponent<Image>().color = new Vector4(Random.Range(0.6f, 1f),
                                                                 Random.Range(0.6f, 1f),
                                                                 Random.Range(0.6f, 1f),
                                                                 1);
            plateCopy.SetActive(true);
            max -= sizeList[i];
        }
        roulette.GetComponent<Image>().fillAmount = 1;
    }
   

}
