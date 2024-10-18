using System;
using UnityEngine;
using UnityEngine.UI;

// 기능 : 로켓의 에너지 시스템, 연료 관리
// 연료 표기
public class RocketEnergySystem : MonoBehaviour
{
    private readonly float FUELPERSHOOT = 10f; // 이 로켓의 연료 소모량
    private readonly float MAXFUEL = 100f;
    private readonly float REFILL = 0.1f;

    [Header("Fuel")] 
    [SerializeField] private Image fuelBar;
    [SerializeField] private float fuel;
    private float lastUsedFuel = -1f;
    public float Fuel 
    { 
        get { return fuel; } 
        private set 
        {
            fuel = value;
            
            if (fuel < 0f)
                fuel = 0f;
            else if (fuel > MAXFUEL)
                fuel = MAXFUEL;

            if (lastUsedFuel > 0f && fuel > lastUsedFuel)
            {
                OnShootEnable?.Invoke(true);
                lastUsedFuel = -1f;
            }

            fuelBar.fillAmount = fuel * 0.01f;
        }
    }

    public event Action<bool> OnShootEnable;


    void Start()
    {
        Fuel = MAXFUEL;

        // 구독을 하는 주체성 - 이 클래스에서 주도적으로 구독한다는 측면에서 독립성이 높음.
        Rocket rocket = GetComponent<Rocket>();
        rocket.OnShoot -= UseFuel;
        rocket.OnShoot += UseFuel;
    }

    void Update()
    {
        // 매프레임 마다 0.1f 씩 충전
        // 만약 60프레임 밑으로 내려가는 환경이라면?
        Fuel += REFILL * Time.deltaTime;

        // 10 프레임 : Time.deltaTime = 0.1f
        // 60 프레임 : Time.deltaTime 0.0166666f = 0.1f / 6f
        

        // fixed update
        // time.deltaTime
    }

    public void UseFuel()
    {
        Fuel -= FUELPERSHOOT;

        if(Fuel < FUELPERSHOOT)
        {
            OnShootEnable?.Invoke(false);   
            lastUsedFuel = FUELPERSHOOT;
        }
    }
}