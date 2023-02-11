using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace TKLibs
{
  public class CameraMasker : MonoBehaviour
  {
    [SerializeField] Material TransitionMaterial2;
    [SerializeField]Texture2D maskTexture;
    [SerializeField] Texture2D unmaskTexture;
    [SerializeField] float maskTime = 0.2f;
    
    Material _transitionMaterial;
    
    static readonly int Cutoff = Shader.PropertyToID("_Cutoff");
    static readonly int TransitionTex = Shader.PropertyToID("_TransitionTex");

    void Awake()
    {
      _transitionMaterial = new Material(TransitionMaterial2);
      _transitionMaterial.SetFloat(Cutoff, 1);
    }

    void Start()
    {
      Unmask();
      SceneManager.sceneLoaded += (scene, _) => SceneManager.SetActiveScene(scene);
    }

    public void AddScene(string sceneName)
    {
      SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void MaskSwitchScene(string sceneName, System.Action callback = null)
    {
      _transitionMaterial.SetTexture(TransitionTex, maskTexture);
      StartCoroutine(
        MaskCoroutine(
          0f,
          1f,
          () =>
          {
            callback?.Invoke();
            StartCoroutine(SwitchScene(sceneName));
          }
        )
      );
    }

    IEnumerator SwitchScene(string sceneName)
    {
      SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
      SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
      yield return null;
      Unmask();
    }

    public void MaskChangeScene(string sceneName, System.Action callback = null)
    {
      _transitionMaterial.SetTexture(TransitionTex, maskTexture);
      StartCoroutine(
        MaskCoroutine(
          0f,
          1f,
          () =>
          {
            callback?.Invoke();
            SceneManager.LoadScene(sceneName);
          }
        )
      );
    }

    public void Unmask()
    {
      _transitionMaterial.SetTexture(TransitionTex, unmaskTexture);
      StartCoroutine(MaskCoroutine(1f, 0f));
    }

    IEnumerator MaskCoroutine(float start, float end, System.Action callback = null)
    {
      yield return null;

      float time = 0;
      while (time < maskTime)
      {
        _transitionMaterial.SetFloat(Cutoff, Mathf.Lerp(start, end, time / maskTime));
        time += Time.deltaTime;
        yield return null;
      }

      _transitionMaterial.SetFloat(Cutoff, end);
      callback?.Invoke();
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
      if (_transitionMaterial != null)
        Graphics.Blit(src, dst, _transitionMaterial);
    }
  }
}