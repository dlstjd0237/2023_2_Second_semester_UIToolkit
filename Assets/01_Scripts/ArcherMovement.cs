using UnityEngine;



public class 궁수 : MonoBehaviour
{
    private const string _name = "궁수 ";
    private float _speed = 20f;
    private MovementType _movementType = MovementType.WALK;

    public void Introduce()
    {
        Debug.Log("I'm 궁수!! ");
    }
}