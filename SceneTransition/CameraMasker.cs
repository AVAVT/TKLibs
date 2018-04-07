using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraMasker : MonoBehaviour
{
  public Material TransitionMaterial;
  public Texture2D maskTexture;
  public Texture2D unmaskTexture;
  public float maskTime = 0.2f;

  private void Awake()
  {
    TransitionMaterial.SetFloat("_Cutoff", 1);
  }

  private void Start()
  {
    Unmask();
  }

  public void MaskChangeScene(string sceneName, System.Action callback = null)
  {
    TransitionMaterial.SetTexture("_TransitionTex", maskTexture);
    StartCoroutine(MaskCoroutine(0f, 1f, sceneName, callback));
  }

  public void Unmask()
  {
    TransitionMaterial.SetTexture("_TransitionTex", unmaskTexture);
    StartCoroutine(MaskCoroutine(1f, 0f));
  }

  IEnumerator MaskCoroutine(float start, float end, string targetScene = null, System.Action callback = null)
  {
    yield return null;

    float time = 0;
    while (time < maskTime)
    {
      TransitionMaterial.SetFloat("_Cutoff", Mathf.Lerp(start, end, time / maskTime));
      time += Time.deltaTime;
      yield return null;
    }

    TransitionMaterial.SetFloat("_Cutoff", end);
    if (callback != null) callback();

    if (!string.IsNullOrEmpty(targetScene))
    {
      SceneManager.LoadScene(targetScene);
    }
  }

  void OnRenderImage(RenderTexture src, RenderTexture dst)
  {
    if (TransitionMaterial != null)
      Graphics.Blit(src, dst, TransitionMaterial);
  }
}
