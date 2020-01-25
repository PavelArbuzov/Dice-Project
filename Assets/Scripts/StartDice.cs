 using UnityEngine;
public class StartDice : MonoBehaviour
{
    public Transform[] _PointEnd;
    public Transform[] _EnemyTransform;
    public Transform[] _PlayerTransform;
    public Die_d6  _EnemyDicePref;
    public Die_d6[] _PlayerDice;
    public static Transform[] _Point;    
    public static Die_d6[] _EnemyDice;
    public static Die_d6 _FirstDice;
    public static Die_d6 _SecondDice;
    public static Die_d6 _ThirdDice;    
    public  static int _bonus=0;    
    int _cout = 0;
    int _coutn = 0;
    void Start()
    {
        _Point = new Transform[3];
        _EnemyDice = new Die_d6[3];
        foreach (Die_d6 element in _EnemyDice)
        {
            _EnemyDice[_cout] = Instantiate(_EnemyDicePref, _EnemyTransform[_cout].position, _EnemyTransform[_cout].rotation = Random.rotation);
            _cout++;
        }        
        foreach (Transform element in _PointEnd)
        {
            _Point[_coutn] = element;
            _coutn++;
        }        
        _FirstDice = Instantiate(_PlayerDice[0], _PlayerTransform[0].position, _PlayerTransform[0].rotation = Random.rotation);
        _SecondDice = Instantiate(_PlayerDice[1], _PlayerTransform[1].position, _PlayerTransform[1].rotation = Random.rotation);
        _ThirdDice = Instantiate(_PlayerDice[2], _PlayerTransform[2].position, _PlayerTransform[2].rotation = Random.rotation);        
        _FirstDice._point = 0;
        _SecondDice._point = 1;
        _ThirdDice._point = 2;
        _FirstDice._ischange = true;
    }
    void Update()
    {
        if (_ThirdDice._isEnd == false) Bonus();
    }
    public void Bonus()
    {       
        if (_ThirdDice.GetComponent<Rigidbody>().isKinematic == true)
        {
            if (_SecondDice.GetComponent<Rigidbody>().isKinematic == true)
            {
                if (_FirstDice.GetComponent<Rigidbody>().isKinematic == true)
                {
                    switch (_bonus)
                    {
                        case 0:
                            Debug.Log("Бонус 0");
                            _ThirdDice.GetComponent<Die>()._isEnd = true;
                            break;
                        case 1:
                            _ThirdDice.GetComponent<Die>()._isEnd = true;
                            Debug.Log("Бонус 1");
                            break;
                        case 2:
                            _ThirdDice.GetComponent<Die>()._isEnd = true;
                            Debug.Log("Бонус 2");
                            break;
                    }
                }
            }
        }
    }
}
