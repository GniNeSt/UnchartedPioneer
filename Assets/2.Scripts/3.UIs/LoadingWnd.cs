using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingWnd : MonoBehaviour
{
    [SerializeField] Image _loadingImg;
    [SerializeField] Text _loadingTxt, _tooTip;
    [SerializeField] Slider _loadingSlider;
    float _rollingTime, dotTime = 0.3f, loadingTime=10.0f;
    int _dotCount = 6;
    private void Update()
    {
        loadingTime+=Time.deltaTime;
        _rollingTime += Time.deltaTime;
        dotTime += Time.deltaTime;
        if (_rollingTime > 0.1f)
        {
            _rollingTime = 0f;
            RollingImg(_loadingImg);
        }
        if (dotTime > 0.3f)
        {
            dotTime = 0f;
            _dotCount = dotText(_loadingTxt, _dotCount);
        }
        if(loadingTime > 10.0f)
        {
            loadingTime = 0f;
            List<string> list = PoolManager._instance.TipDatas;
            int num = Random.Range(0, list.Count);
            _tooTip.text = list[num];
        }
        
    }
    private void LateUpdate()
    {
        _loadingSlider.value = loadingTime/10f;
    }
    public void RollingImg(Image Img)
    {
        Img.rectTransform.Rotate(new Vector3(0,0,30));
    }
    public int dotText(Text text, int count)
    {
        if(count < 6)
        {
            text.text += ".";
            count++;
        }
        else
        {
            count = 0;
            text.text = "Loading";
        }
        return count;
    }

    public void OpenWindow()
    {
        gameObject.SetActive(true);
        Canvas canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }
    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }
    public void SetLoaddingRate(float value)
    {
        _loadingSlider.value = value;
    }
}
