using UnityEngine;



public class 전사 : MonoBehaviour
{
    private const string _name = "전사 ";
    private float _speed = 10f;
    private MovementType _movementType = MovementType.WALK;

    public void Introduce()
    {
        Debug.Log("I'm 전사!! ");
    }
}