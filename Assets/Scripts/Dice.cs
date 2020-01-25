using UnityEngine;
public class Dice : MonoBehaviour 
{    
    private Die _GetComponentDie;
    private Rigidbody _Rigibody;
    public static Vector3 _DiceVelocity;
    void Start()
    {
        _GetComponentDie = gameObject.GetComponent<Die>();
        _Rigibody = GetComponent<Rigidbody>();
    }  
    void FixedUpdate()
    {
        _DiceVelocity = _Rigibody.velocity;
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            RaycastHit _hit;
            if (Physics.Raycast(ray, out _hit, Mathf.Infinity))
            {
                if (_hit.transform.GetComponent<Die>() != null)
                {
                    if (_hit.transform.GetComponent<Die>()._ischange == true)
                    {
                        float dirX = Random.Range(0, 500);
                        float dirY = Random.Range(0, 500);
                        float dirZ = Random.Range(0, 500);
                        _hit.rigidbody.AddForce(transform.forward * 500);
                        _hit.rigidbody.AddTorque(dirX, dirY, dirZ);                       
                        _GetComponentDie.NextDice();
                        _GetComponentDie._isPlaying = true;
                        gameObject.GetComponent<Dice>().enabled = false;
                    }
                }
            }
        }
    }
}