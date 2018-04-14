using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

//[assembly: InternalsVisibleTo("")]

namespace GeneralDefine
{
  /// <summary>
  /// path group necessary in reference to an external file
  /// </summary>
  internal struct ExternalFilePath
  {
    // internal const string KEY_CONFIG = "keyconf.dat";
    internal const string SOUND_SETTING = "soundset.dat";

    // internal const string EVENT = "eventdat.xml";
    // internal const string EVENT_SCHEMA = "eventdat.xsd";
  }

  /// <summary>
  /// path to the resource in the Resoures
  /// </summary>
  internal struct ResourcePath
  {
    //internal const string sth
  }

  /// <summary>
  /// input base class
  /// </summary>
  public class InputValue
  {
    public readonly string String;

    protected InputValue(string name)
    {
      String = name;
    }
  }

  /// <summary>
  /// key base class
  /// </summary>
  public sealed class Key : InputValue
  {
    public readonly List<KeyCode> DefaultKeyCode;
    public readonly static List<Key> AllKeyData = new List<Key>();

    private Key(string keyName, List<KeyCode> defaultKeyCode)
      : base(keyName)
    {
      DefaultKeyCode = defaultKeyCode;
      AllKeyData.Add(this);
    }

    public override string ToString()
    {
      return String;
    }

    // public static readonly Key Action = new Key("Action", new List<KeyCode> { KeyCode.Z });
    // public static readonly Key Jump = new Key("Jump", new List<KeyCode> { KeyCode.Space });
    // public static readonly Key Balloon = new Key("Balloon", new List<KeyCode> { KeyCode.X });
    // public static readonly Key Squat = new Key("Squat", new List<KeyCode> { KeyCode.C });
    // public static readonly Key Menu = new Key("Menu", new List<KeyCode> { KeyCode.Escape });
  }

  /// <summary>
  /// axis class
  /// </summary>
  public sealed class Axis : InputValue
  {
    public readonly static List<Axis> AllAxisData = new List<Axis>();

    private Axis(string AxisName)
      : base(AxisName)
    {
      AllAxisData.Add(this);
    }

    public override string ToString()
    {
      return String;
    }

    public static Axis Horizontal = new Axis("Horizontal");
    public static Axis Vertical = new Axis("Vertical");
  }

  /// <summary>
  /// class represent of animation param type
  /// </summary>
  public enum AnimationParamType
  {
    Float,
    Int,
    Bool,
    Trigger
  }

  /// <summary>
  /// animation param
  /// </summary>
  internal class AnimationParam
  {
    public readonly string String;
    public readonly AnimationParamType ParamType;

    protected AnimationParam(string stateName, AnimationParamType type)
    {
      String = stateName;
      ParamType = type;
    }

    public override string ToString()
    {
      return String;
    }
  }

  /// <summary>
  /// example use of animation param
  /// </summary>
  // internal sealed class PlayerAnimationParam : AnimationParam
  // {
  //   private PlayerAnimationParam(string stateName, AnimationParamType type)
  //     : base(stateName, type)
  //   {
  //   }

  //   public static PlayerAnimationParam Walk = new PlayerAnimationParam("Walk", AnimationParamType.Bool);
  //   public static PlayerAnimationParam JumpStart = new PlayerAnimationParam("JumpStart", AnimationParamType.Trigger);
  //   public static PlayerAnimationParam JumpEnd = new PlayerAnimationParam("JumpEnd", AnimationParamType.Trigger);
  //   public static PlayerAnimationParam TakeObject = new PlayerAnimationParam("TakeObject", AnimationParamType.Bool);
  //   public static PlayerAnimationParam TakeBalloon = new PlayerAnimationParam("TakeBalloon", AnimationParamType.Bool);
  //   public static PlayerAnimationParam Squat = new PlayerAnimationParam("Squat", AnimationParamType.Bool);
  //   public static PlayerAnimationParam Put = new PlayerAnimationParam("Put", AnimationParamType.Trigger);
  // }

  /// <summary>
  /// input event group
  /// </summary>
  internal enum InputEventGroupName
  {
    //TODO put input into group event
  }

  internal enum AudioMixerParamName
  {
    BGM,
    SFX,
  }

  /// <summary>
  /// held the enum extension method
  /// </summary>
  internal static class ExtensionEnum
  {
    public static string String(this InputEventGroupName group)
    {
      return group.ToString();
    }

    public static string String(this AudioMixerParamName param)
    {
      return param.ToString();
    }
  }
}