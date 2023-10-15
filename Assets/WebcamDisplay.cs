using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WebcamDisplay : MonoBehaviour
{
    private WebCamTexture webcamTexture;
    public RawImage rawImage;

    void Start()
    {
        // ��ķ �̸��� �����ͼ� ��ķ �ؽ�ó�� �����մϴ�.
        string webcamName = WebCamTexture.devices[1].name;
        webcamTexture = new WebCamTexture(webcamName);

        // ��ķ �ؽ�ó�� RawImage�� �Ҵ��Ͽ� ȭ�鿡 ǥ���մϴ�.
        rawImage.texture = webcamTexture;

        // ��ķ�� �����մϴ�.
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
