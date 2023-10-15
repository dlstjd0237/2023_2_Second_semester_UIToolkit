using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WebcamDisplay : MonoBehaviour
{
    private WebCamTexture webcamTexture;
    public RawImage rawImage;

    void Start()
    {
        // 웹캠 이름을 가져와서 웹캠 텍스처를 생성합니다.
        string webcamName = WebCamTexture.devices[1].name;
        webcamTexture = new WebCamTexture(webcamName);

        // 웹캠 텍스처를 RawImage에 할당하여 화면에 표시합니다.
        rawImage.texture = webcamTexture;

        // 웹캠을 시작합니다.
    }

    public void CamStart()
    {
        webcamTexture.Play();

    }
    public void CamStop()
    {
        webcamTexture.Stop();

    }
}
