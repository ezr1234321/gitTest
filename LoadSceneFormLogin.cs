using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LoadSceneFormLogin : MonoBehaviour
{   //非同步載入程序
    AsyncOperation async;
    public Slider slider;
    public TMP_Text text;//百分制顯示進度加載情況
    void Start()
    {
        //開啓協程
        slider.value = 0f;

        StartCoroutine("loginLoginpage");
    }
    IEnumerator loginLoginpage()
    {
        int displayProgress = 0;//進度條
        int toProgress = 0;
        AsyncOperation op = SceneManager.LoadSceneAsync("SampleScene");
        op.allowSceneActivation = false;//Allow Scenes to be activated as soon as it is ready => false
        while (op.progress < 0.9f) //此處如果是 <= 0.9f 則會出現死循環所以必須小0.9
        {
            toProgress = (int)op.progress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                SetLoadingPercentage(displayProgress);
                yield return new WaitForEndOfFrame();//ui渲染完成之後
            }
        }
        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            SetLoadingPercentage(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;

    }

    private void SetLoadingPercentage(int displayProgress)
    {
        slider.value = displayProgress;
        text.text = displayProgress.ToString() + "%";
    }

}
