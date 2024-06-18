using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingWnd : MonoBehaviour
{
    [SerializeField] Image _loadingImg;
    [SerializeField] Text _loadingTxt;
    float _rollingTime, dotTime = 0.3f;
    int _dotCount = 6;
    private void Update()
    {
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
}
