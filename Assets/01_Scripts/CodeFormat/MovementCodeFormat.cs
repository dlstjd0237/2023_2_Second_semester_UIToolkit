using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
    WALK,
    FLY
}
public class MovementCodeFormat : MonoBehaviour
{
    public static string MovementFormat =
 @"using UnityEngine;



public class {0} : MonoBehaviour
{{
    private const string _name = ""{0} "";
    private float _speed = {2}f;
    private MovementType _movementType = MovementType.{3};

    public void Introduce()
    {{
        Debug.Log(""I'm {0}!! "");
    }}
}}";

}
