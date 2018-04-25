using UnityEngine;
using ApproximationNET;

public static class VectorExtensionMethods
{
  public static Vector2 ApproxClampMagnitude(this Vector2 vector, float magnitude)
  {
    return vector.sqrMagnitude > magnitude * magnitude
      ? vector.ApproxScaleTo(magnitude)
      : vector;
  }

  public static Vector2 ApproxScaleTo(this Vector2 vector, float magnitude)
  {
    float idist = Approximation.InvSqrt(vector.sqrMagnitude);
    return new Vector2(
      vector.x * idist * magnitude,
      vector.y * idist * magnitude
    );
  }
  public static Vector2 ScaleTo(this Vector2 vector, float magnitude)
  {
    return vector.normalized * magnitude;
  }

  public static Vector3 ScaleTo(this Vector3 vector, float magnitude)
  {
    return vector.normalized * magnitude;
  }

  public static Vector2 xy(this Vector3 v)
  {
    return new Vector2(v.x, v.y);
  }

  public static Vector3 WithX(this Vector3 v, float x)
  {
    return new Vector3(x, v.y, v.z);
  }

  public static Vector3 WithY(this Vector3 v, float y)
  {
    return new Vector3(v.x, y, v.z);
  }

  public static Vector3 WithZ(this Vector3 v, float z)
  {
    return new Vector3(v.x, v.y, z);
  }

  public static Vector2 WithX(this Vector2 v, float x)
  {
    return new Vector2(x, v.y);
  }

  public static Vector2 WithY(this Vector2 v, float y)
  {
    return new Vector2(v.x, y);
  }

  public static Vector3 WithZ(this Vector2 v, float z)
  {
    return new Vector3(v.x, v.y, z);
  }

  public static Vector2Int xy(this Vector3Int v)
  {
    return new Vector2Int(v.x, v.y);
  }

  public static Vector3Int WithX(this Vector3Int v, int x)
  {
    return new Vector3Int(x, v.y, v.z);
  }

  public static Vector3Int WithY(this Vector3Int v, int y)
  {
    return new Vector3Int(v.x, y, v.z);
  }

  public static Vector3Int WithZ(this Vector3Int v, int z)
  {
    return new Vector3Int(v.x, v.y, z);
  }

  public static Vector2Int WithX(this Vector2Int v, int x)
  {
    return new Vector2Int(x, v.y);
  }

  public static Vector2Int WithY(this Vector2Int v, int y)
  {
    return new Vector2Int(v.x, y);
  }

  public static Vector3Int WithZ(this Vector2Int v, int z)
  {
    return new Vector3Int(v.x, v.y, z);
  }

  // axisDirection - unit vector in direction of an axis (eg, defines a line that passes through zero)
  // point - the point to find nearest on line for
  public static Vector3 NearestPointOnAxis(this Vector3 axisDirection, Vector3 point, bool isNormalized = false)
  {
    if (!isNormalized) axisDirection.Normalize();
    var d = Vector3.Dot(point, axisDirection);
    return axisDirection * d;
  }

  // lineDirection - unit vector in direction of line
  // pointOnLine - a point on the line (allowing us to define an actual line in space)
  // point - the point to find nearest on line for
  public static Vector3 NearestPointOnLine(
      this Vector3 lineDirection, Vector3 point, Vector3 pointOnLine, bool isNormalized = false)
  {
    if (!isNormalized) lineDirection.Normalize();
    var d = Vector3.Dot(point - pointOnLine, lineDirection);
    return pointOnLine + (lineDirection * d);
  }

  public static int GridDistanceTo(this Vector2Int vector, Vector2Int target)
  {
    return Mathf.Abs(vector.x - target.x) + Mathf.Abs(vector.y - target.y);
  }

  public static Vector2 RotateBy(this Vector2 vector, float degree)
  {
    return Quaternion.Euler(0, 0, degree) * vector;
  }

  public static Vector2 RandomPointInRadius(this Vector2 point, float radius)
  {
    return point + Vector2.one.RotateBy(Random.Range(0f, 360f)) * radius * Mathf.Sqrt(Random.Range(0f, 1f));
  }
}