using UnityEngine;
public class Die : MonoBehaviour {
    protected Vector3 _LocalHitNormalized;
    public Transform _PointEndDice;
    private Rigidbody _Rigibody;
    public int _value = 0;
    public int _point;
    protected float _validMargin = 0.45F;
    public bool _ischeck = true;
    public bool _isPlaying;
    public bool _isEnd;
    public bool _ischange;    
    public bool rolling
    {
        get
        {
            return !(_Rigibody.velocity.sqrMagnitude <= 0F && _Rigibody.angularVelocity.sqrMagnitude <=0F);
        }
    }	
    protected bool localHit
    {
        get
        {			
            Ray ray = new Ray(transform.position + (new Vector3(0, 2, 0) * transform.localScale.magnitude), Vector3.up * -1);
            RaycastHit hit = new RaycastHit();			
            if (GetComponent<Collider>().Raycast(ray, out hit, 3 * transform.localScale.magnitude))
            {				
                _LocalHitNormalized = transform.InverseTransformPoint(hit.point.x, hit.point.y, hit.point.z).normalized;
                return true;
            }			
            return false;
        }
    }	
    protected bool valid(float t, float v)
    {
        if (t > (v - _validMargin) && t < (v + _validMargin))
            return true;
        else
            return false;
    }
    void Start()
    {
        _Rigibody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (!rolling && localHit)
        {
            GetValue();
            if (_isPlaying == true)
            {
                for (int i = 0; i < StartDice._EnemyDice.Length; i++)
                {
                    if (StartDice._EnemyDice[i]._ischeck)
                    {
                        if (_value == StartDice._EnemyDice[i]._value)
                        {
                            gameObject.GetComponent<Renderer>().material = Resources.Load("Dice-greenMaterial") as Material;
                            MovePoint(_point);
                            StartDice._EnemyDice[i]._ischeck = false;
                            _isPlaying = false;
                            StartDice._bonus++;
                            return;
                        }
                        else gameObject.GetComponent<Renderer>().material = Resources.Load("Dice-red") as Material;
                        MovePoint(_point);
                        _isPlaying = false;
                    }
                }
            }
        }        
    }
    void GetValue()
    {		
        _value = 0;
        float _delta = 1;		
        int _side = 1;
        Vector3 testHitVector;		
        do
        {          
            testHitVector = HitVector(_side);
            if (testHitVector != Vector3.zero)
            {
                if (valid(_LocalHitNormalized.x, testHitVector.x) &&
                    valid(_LocalHitNormalized.y, testHitVector.y) &&
                    valid(_LocalHitNormalized.z, testHitVector.z))
                {
                    float nDelta = Mathf.Abs(_LocalHitNormalized.x - testHitVector.x) + Mathf.Abs(_LocalHitNormalized.y - testHitVector.y) + Mathf.Abs(_LocalHitNormalized.z - testHitVector.z);
                    if (nDelta < _delta)
                    {                       
                        _value = _side;
                        _delta = nDelta;                     
                    }                   
                }              
            }           
            _side++;            
        } while (testHitVector != Vector3.zero);             
    }
    public void MovePoint(int a )
    {
        
        gameObject.transform.position = StartDice._Point[a].position;        
        _Rigibody.isKinematic = true;        
    }	
    protected virtual Vector3 HitVector(int side)
    {
        return Vector3.zero;
    }
    public void NextDice()
    {
            if (StartDice._ThirdDice._ischange == false)
            {
                if (StartDice._SecondDice._ischange == false)
                {
                    if (StartDice._FirstDice._ischange == false)
                    {
                        StartDice._FirstDice._ischange = true;
                    }
                    else
                    {
                        StartDice._FirstDice._ischange = false;
                        StartDice._SecondDice._ischange = true;
                    }
                }
                else
                {
                    StartDice._SecondDice._ischange = false;
                    StartDice._ThirdDice._ischange = true;
                }
            }
            else
            {
                StartDice._ThirdDice._ischange = false;
            }   
    }
}
