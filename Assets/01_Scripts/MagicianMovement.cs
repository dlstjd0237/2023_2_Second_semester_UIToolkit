using UnityEngine;



public class 마법사 : MonoBehaviour
{
    private const string _name = "마법사 ";
    private float _speed = 5f;
    private MovementType _movementType = MovementType.FLY;

    public void Introduce()
    {
        Debug.Log("I'm 마법사!! ");
    }
}