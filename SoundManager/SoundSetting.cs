using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// class to handle the information of the sound settings
/// </summary>
public class SoundSetting
{
  private const string SE = "SE";
  private const string BGM = "BGM";

  private Dictionary<string, double> data = new Dictionary<string, double>();

  private readonly string filePath;

  /// <summary>
  /// access to SE volume
  /// </summary>
  public float SFX
  {
    get { return (float)data[SE]; }
    set { data[SE] = value; }
  }

  /// <summary>
  /// access to BGM volume
  /// </summary>
  public float BGMVolume
  {
    get { return (float)data[BGM]; }
    set { data[BGM] = value; }
  }


  public SoundSetting(string filePath)
  {
    this.filePath = filePath;
    data.Add(SE, 0);
    data.Add(BGM, 0);
  }

  ///<summary>
  /// load setting
  ///<summary>
  public void LoadSettingFile()
  {
    if (File.Exists(filePath))
    {
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Open(filePath, FileMode.Open);

      data = (Dictionary<string, double>)bf.Deserialize(file);
      file.Close();
    }
    else
    {
      SFX = 1;
      BGMVolume = 1;
    }
  }

  /// <summary>
  /// save setting
  /// </summary>
  public void SaveSettingFile()
  {

    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Open(filePath, FileMode.OpenOrCreate);

    bf.Serialize(file, data);
    file.Close();
  }
}