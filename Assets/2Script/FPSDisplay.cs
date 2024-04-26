using UnityEngine;
using UnityEngine.UI; // UI 컴포넌트 사용을 위해 추가

public class FPSDisplay : MonoBehaviour
{
    public Text fpsText; // FPS를 표시할 UI 텍스트
    float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        float msec = deltaTime * 1000.0f;
        fpsText.text = $"{msec:0.0} ms ({fps:0.} fps)"; // 밀리초와 FPS 값을 텍스트로 설정
    }
}