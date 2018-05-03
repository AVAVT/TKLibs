using GeneralDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Audio;
using TKLibs;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Manage sound
/// </summary>
public class SoundManager : MonoBehaviour
{
	[SerializeField]
	private enum SoundType
	{
		BGM,
		SFX
	}
	private const float MAX_DECIBEL = 0f;
	private const float MIN_DECIBEL = -80f;
	public static SoundManager Instance { get; private set; }
	[SerializeField]
	private AudioMixer mixer = null;
	[SerializeField]
	public int poolAmount;
	[SerializeField]
	public bool canGrow;
	[SerializeField]
	public GameObject pooledPrefab;

	[SerializeField]
	private List<string> audioNames = new List<string>();

	[SerializeField]
	private List<AudioSource> audioDatas = new List<AudioSource>();
	[SerializeField]
	private SoundType soundType;
	private List<AudioSource> pooledAudioSource;
	private List<GameObject> pooledObject;
	private SoundSetting soundSetting;

	/// <summary>
	/// volume of SE
	/// </summary>
	public float SFX
	{
		get
		{ return soundSetting.SFX; }
		set
		{
			mixer.SetFloat(AudioMixerParamName.SFX.String(), GetDecibelConversion(value));
			soundSetting.SFX = value;
		}
	}

	/// <summary>
	/// volume of BGM
	/// </summary>
	public float BGM
	{
		get { return soundSetting.BGMVolume; }
		set
		{
			mixer.SetFloat(AudioMixerParamName.BGM.String(), GetDecibelConversion(value));
			soundSetting.BGMVolume = value;
		}
	}

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			try
			{
				LoadSettingFile();
			}
			catch (IOException e)
			{
				Debug.LogWarning(e.Message);
				soundSetting.SFX = 1f;
				soundSetting.BGMVolume = 1f;
			}
		}
		else
		{
			Destroy(this);
		}
		InitializeAudioPool();
	}

	private void Start()
	{
		SFX = soundSetting.SFX;
		BGM = soundSetting.BGMVolume;
	}

	/// <summary>
	/// play a sound
	/// </summary>
	/// <param name="key">Key of the sound</param>
	public void PlayBGM(AudioKey key, bool fade = false, float fadeDuration = 0)
	{
		audioDatas[(int)key].Play();
	}

	/// <summary>
	/// play a sound
	/// </summary>
	/// <param name="name">Name of the sound</param>
	public void PlayBGM(string name, bool fade = false, float fadeDuration = 0)
	{
		PlayBGM(ConvertAudioKey(name), fade, fadeDuration);
	}

	/// <summary>
	/// play a sound
	/// Can play multiple time
	/// </summary>
	/// <param name="key">key of the sound</param>
	/// <param name ="gameObject">gameObject to attach audioSource</param>
	public void PlayOneShot(AudioKey key, Vector3 position)
	{
		int? nullableIndex = GetPoooledObjectIndex();
		if (nullableIndex == null)
		{
			return;
		}
		int index = (int)nullableIndex;
		pooledObject[index].SetActive(true);
		pooledAudioSource[index].clip = audioDatas[(int)key].clip;
		pooledAudioSource[index].volume = audioDatas[(int)key].volume;
		pooledAudioSource[index].loop = audioDatas[(int)key].loop;
		pooledAudioSource[index].spatialBlend = audioDatas[(int)key].spatialBlend;
		pooledAudioSource[index].dopplerLevel = audioDatas[(int)key].dopplerLevel;
		pooledAudioSource[index].minDistance = audioDatas[(int)key].minDistance;
		pooledAudioSource[index].maxDistance = audioDatas[(int)key].maxDistance;
		pooledAudioSource[index].outputAudioMixerGroup = audioDatas[(int)key].outputAudioMixerGroup;
		pooledAudioSource[index].Play();
	}

	public void Play2DSFX(AudioKey key)
	{
		PlayOneShot(key, gameObject.transform.position);
	}

	public void Play2DSFX(string name)
	{
		PlayOneShot(ConvertAudioKey(name), gameObject.transform.position);
	}

	public void Play3DSFX(AudioKey key, Vector3 position)
	{
		PlayOneShot(key, position);
	}

	/// <param name="name">name of the sound</param>
	public void Play3DSFX(string name, Vector3 position)
	{
		PlayOneShot(ConvertAudioKey(name), position);
	}

	/// <summary>
	/// Stop a sound
	/// </summary>
	/// <param name="key">key of the sound</param>
	public void Stop(AudioKey key)
	{
		audioDatas[(int)key].Stop();
	}

	/// <summary>
	/// Stop a sound
	/// </summary>
	/// <param name="name">name of the sound</param>
	public void Stop(string name)
	{
		Stop(ConvertAudioKey(name));
	}

	/// <summary>
	/// Load setting from file
	/// </summary>
	public void LoadSettingFile()
	{
		soundSetting = new SoundSetting(Application.persistentDataPath + "/" + ExternalFilePath.SOUND_SETTING);
		soundSetting.LoadSettingFile();
	}

	/// <summary>
	/// Save setting to file
	/// </summary>
	public void SaveSettingFile()
	{
		//TODO
		soundSetting.SaveSettingFile();
	}

	/// <summary>
	/// to convert a string to AudioKey
	/// </summary>
	/// <param name="name">string to be converted to audio key</param>
	private AudioKey ConvertAudioKey(string name)
	{
		var keyIndex = audioNames.IndexOf(name);
		if (keyIndex < 0)
			throw new Exception(name + "Failed to convert to AudioKey");
		return (AudioKey)keyIndex;
	}

	public void SetMusicVolume(float volume)
	{
		BGM = volume;
		SaveSettingFile();
	}

	public void SetSFXVolume(float volume)
	{
		SFX = volume;
		SaveSettingFile();
	}

	public float GetMusicVolume()
	{
		return BGM;
	}

	public float GetSFXVolume()
	{
		return SFX;
	}

	/// <summary>
	/// decibel conversion
	/// </summary>
	private float DecibelConversion(float volume)
	{
		return 20f * Mathf.Log10(volume);
	}

	/// <summary>
	/// obtain the value of the post-decibel conversion
	/// converted values are Clamp to take appropriate value
	/// </summary>
	private float GetDecibelConversion(float value)
	{
		return Mathf.Clamp(DecibelConversion(value), MIN_DECIBEL, MAX_DECIBEL);
	}

	///<summary>
	///Fade In BGM
	///<summary>
	public void FadeIn(AudioKey key, float fadeDuration)
	{
		audioDatas[(int)key].Play();
		StartCoroutine(Fade(audioDatas[(int)key], 0, audioDatas[(int)key].volume, fadeDuration));
	}

	///<summary>
	///Fade Out BGM
	///<summary>
	public void FadeOut(AudioKey key, float fadeDuration)
	{
		StartCoroutine(Fade(audioDatas[(int)key], audioDatas[(int)key].volume, 0, fadeDuration, () =>
		{
			audioDatas[(int)key].Stop();
		}));
	}

	public void CrossFade(AudioKey currentKey, AudioKey targetKey, float fadeDuration)
	{
    
		FadeOut(currentKey, fadeDuration);
		FadeIn(targetKey, fadeDuration);
	}

	IEnumerator Fade(AudioSource source, float startVolume, float targetVolume, float fadeDuration, Action callback = null)
	{
		source.volume = startVolume;
		float t = 0;
		while (t < fadeDuration)
		{
			source.volume = Mathf.Lerp(startVolume, targetVolume, t / fadeDuration);
			t += Time.deltaTime;
			yield return null;
		}
		source.volume = targetVolume;

		callback?.Invoke();
	}

	#region Pooling

	private void InitializeAudioPool()
	{
		pooledObject = new List<GameObject>();
		pooledAudioSource = new List<AudioSource>();
		for (int i = 0; i < poolAmount; i++)
		{
			GameObject obj = Instantiate(pooledPrefab);
			pooledAudioSource.Add(obj.GetComponent<AudioSource>());
			obj.transform.parent = this.transform;
			obj.SetActive(false);
			pooledObject.Add(obj);
		}
	}

	public int? GetPoooledObjectIndex()
	{
		for (int i = 0; i < pooledObject.Count(); i++)
		{
			if (pooledObject[i] == null)
			{
				GameObject obj = Instantiate(pooledPrefab);
				pooledAudioSource.Add(obj.GetComponent<AudioSource>());
				obj.transform.parent = this.transform;
				obj.SetActive(false);
				pooledObject[i] = obj;
				return i;
			}
			if (!pooledObject[i].activeInHierarchy)
			{
				return i;
			}

			if (canGrow)
			{
				GameObject obj = Instantiate(pooledPrefab);
				pooledAudioSource.Add(obj.GetComponent<AudioSource>());
				obj.transform.parent = this.transform;
				obj.SetActive(false);
				pooledObject.Add(obj);
				return (pooledObject.Count() - 1);
			}
		}
		return null;
	}
	#endregion

	#region CustomInspector

#if UNITY_EDITOR

	[CustomEditor(typeof(SoundManager))]
	private class SoundManagetInspector : Editor
	{
		private SoundManager soundManager;
		private int selectAudioSourceIndex;
		private string newAudioDataName = string.Empty;
		private List<bool> foldSoundDatas = new List<bool>();
		private string outputScriptPath = string.Empty;
		private const string SCRIPT_FILE_NAME = "AudioSourceKeyMap.cs";

		private const string SCRIPT_FILE_CONTENT =
				@"
				/*Do not change*/

				public enum AudioKey
				{
				[CONTENT]
				}";


		public override void OnInspectorGUI()
		{
			soundManager = target as SoundManager;
			soundManager.mixer = EditorGUILayout.ObjectField("AudioMixer", soundManager.mixer, typeof(AudioMixer), false) as AudioMixer;
			soundManager.pooledPrefab = EditorGUILayout.ObjectField("PooledObject", soundManager.pooledPrefab, typeof(GameObject), false) as GameObject;
			soundManager.poolAmount = EditorGUILayout.IntField("PoolAmount", soundManager.poolAmount);
			soundManager.canGrow = EditorGUILayout.Toggle("CanGrow", soundManager.canGrow);
			EditorGUILayout.Separator();

			#region AudioData

			for (int i = 0; i < soundManager.audioDatas.Count; ++i)
			{
				if (foldSoundDatas.Count < i + 1)
					foldSoundDatas.Add(false);

				if (foldSoundDatas[i] = EditorGUILayout.Foldout(foldSoundDatas[i], soundManager.audioNames[i]))
				{
					EditorGUILayout.BeginVertical("box");
					EditorGUILayout.BeginHorizontal();
					//Play Button Display
					if (soundManager.audioDatas[i] != null && soundManager.audioDatas[i].isPlaying)
					{
						if (GUILayout.Button("■"))
						{
							soundManager.audioDatas[i].Stop();
						}
					}
					else if (GUILayout.Button("▶"))
					{
						soundManager.audioDatas[i].Play();
					}
					EditorGUILayout.EndHorizontal();
					soundManager.audioDatas[i].clip = EditorGUILayout.ObjectField("AudioClip", soundManager.audioDatas[i].clip, typeof(AudioClip), false) as AudioClip;
					soundManager.audioDatas[i].loop = EditorGUILayout.Toggle("Loop", soundManager.audioDatas[i].loop);
					soundManager.audioDatas[i].volume = EditorGUILayout.Slider("Volume", soundManager.audioDatas[i].volume, 0, 1);
					soundManager.soundType = (SoundManager.SoundType)EditorGUILayout.EnumPopup("SoundType", soundManager.soundType);
					// soundManager.audioDatas[i].pitch = EditorGUILayout.Slider("Pitch", soundManager.audioDatas[i].pitch, -3, 3);
					// soundManager.audioDatas[i].bypassEffects = EditorGUILayout.Toggle("BypassEffects", soundManager.audioDatas[i].bypassEffects);
					// soundManager.audioDatas[i].bypassListenerEffects = EditorGUILayout.Toggle("BypassListenerEffects", soundManager.audioDatas[i].bypassListenerEffects);
					// soundManager.audioDatas[i].bypassReverbZones = EditorGUILayout.Toggle("BypassReverbZone", soundManager.audioDatas[i].bypassReverbZones);
					// soundManager.audioDatas[i].reverbZoneMix = EditorGUILayout.Slider("ReverbZoneMix", soundManager.audioDatas[i].reverbZoneMix, 0, 1.1f);
					soundManager.audioDatas[i].spatialBlend = EditorGUILayout.Slider("SpatialBlend", soundManager.audioDatas[i].spatialBlend, 0, 1);
					soundManager.audioDatas[i].dopplerLevel = EditorGUILayout.Slider("DopplerLevel", soundManager.audioDatas[i].dopplerLevel, 0, 1);
					soundManager.audioDatas[i].minDistance = EditorGUILayout.FloatField("MinDistance", soundManager.audioDatas[i].minDistance);
					soundManager.audioDatas[i].maxDistance = EditorGUILayout.FloatField("MaxDistance", soundManager.audioDatas[i].maxDistance);
					soundManager.audioDatas[i].outputAudioMixerGroup = EditorGUILayout.ObjectField("OutPut", soundManager.audioDatas[i].outputAudioMixerGroup, typeof(AudioMixerGroup), false) as AudioMixerGroup;
					if (soundManager.audioDatas[i].outputAudioMixerGroup == null)
					{
						EditorGUILayout.HelpBox("Audio Output is not set so can not be applied to the collective setting of the volume", MessageType.Error);
					}
					EditorGUILayout.EndVertical();
				}
			}

			#endregion AudioData

			#region Add/Remove Button

			EditorGUILayout.BeginHorizontal();
			newAudioDataName = EditorGUILayout.TextField(newAudioDataName);
			if (GUILayout.Button("Add"))
			{
				if (soundManager.audioNames.Contains(newAudioDataName) || string.IsNullOrEmpty(newAudioDataName))
				{
					GUIContent content = new GUIContent("Already Key is present or empty");
					EditorWindow.focusedWindow.ShowNotification(content);
				}
				else
				{
					soundManager.audioNames.Add(newAudioDataName);
					var newAudioSource = soundManager.gameObject.AddComponent<AudioSource>();
					newAudioSource.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
					soundManager.audioDatas.Add(newAudioSource);
				}
				newAudioDataName = string.Empty;
			}
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			selectAudioSourceIndex = EditorGUILayout.Popup(selectAudioSourceIndex, soundManager.audioNames.ToArray());
			if (GUILayout.Button("Delete"))
			{
				if (soundManager.audioNames.Count > selectAudioSourceIndex)
				{
					DestroyImmediate(soundManager.audioDatas[selectAudioSourceIndex]);
					soundManager.audioNames.RemoveAt(selectAudioSourceIndex);
					soundManager.audioDatas.RemoveAt(selectAudioSourceIndex);
					foldSoundDatas.RemoveAt(selectAudioSourceIndex);
				}
			}
			EditorGUILayout.EndHorizontal();

			DebugEditorScript();

			#endregion Add/Remove Button

			EditorGUILayout.Space();

			#region CreateScriptFile

			if (GUILayout.Button("Apply"))
			{
				var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets/SoundManager" + SCRIPT_FILE_NAME);

				if (!fullPath.Contains(Path.Combine(Directory.GetCurrentDirectory(), "Assets")))
					Debug.LogError("Missing Assets/SoundManager");

				else
					using (var writer = new StreamWriter(fullPath, false))
					{
						StringBuilder sb = new StringBuilder();
						foreach (var key in soundManager.audioNames)
							sb.Append("\t" + key + ",\r\n");
						var innerContent = sb.ToString();
						var content = SCRIPT_FILE_CONTENT.Replace("[CONTENT]", innerContent);
						writer.Write(content);
						AssetDatabase.Refresh();
						Debug.Log(string.Format("AudioKey have been saved : {0}", fullPath));
					}
			}

			#endregion CreateScriptFile

			EditorUtility.SetDirty(soundManager);
		}

		/// <summary>
		/// editor script debugging function
		/// </summary>
		[System.Diagnostics.Conditional("DEBUG")]
		private void DebugEditorScript()
		{
			EditorGUILayout.Space();
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("Display AudioSource"))
			{
				var allAudioSource = soundManager.GetComponents<AudioSource>();
				foreach (var data in allAudioSource)
					data.hideFlags = HideFlags.None | HideFlags.NotEditable;
			}
			if (GUILayout.Button("Hide AudioSource"))
			{
				var allAudioSource = soundManager.GetComponents<AudioSource>();
				foreach (var data in allAudioSource)
					data.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.NotEditable;
			}
			if (GUILayout.Button("Clear AudioSource"))
			{
				var allAudioSource = soundManager.GetComponents<AudioSource>();
				//can not be deleted during the non-display
				if (allAudioSource.Any(s => (s.hideFlags & HideFlags.HideInInspector) != HideFlags.HideInInspector))
				{
					GUIContent content = new GUIContent("Please display the audioSource");
					EditorWindow.focusedWindow.ShowNotification(content);
				}
				else
				{
					for (int i = 0; i < allAudioSource.Count(); ++i)
						DestroyImmediate(allAudioSource[i]);
					soundManager.audioDatas.Clear();
					soundManager.audioNames.Clear();
					foldSoundDatas.Clear();
				}
			}
			EditorGUILayout.EndHorizontal();
		}
	}

#endif

	#endregion CustomInspector
}