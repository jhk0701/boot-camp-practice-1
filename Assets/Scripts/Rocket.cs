using System;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private readonly float SPEED = 5f; // 이 로켓의 속력

    private bool isShootable = true;

    // 이벤트 방식이 더 느슨하다
    // 이유 : 지금 캐싱 방식의 클래스의 더 많은 정보를 알아야 하는 것
    // 이벤트 방식의 경우 : 구독된 메서드가 어떤 것인지 알 필요가 없는 것.
    // 단, 오류 발생 시, 어딘지 알기 어려움 - 어떤 방식에서 메서드가 오류난건지 알기 어렵다는 것.
    public event Action OnShoot;
    

    void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        RocketEnergySystem energy = GetComponent<RocketEnergySystem>();
        energy.OnShootEnable -= SetShootable;
        energy.OnShootEnable += SetShootable;
    }


    public void Shoot()
    {
        // TODO : fuel이 넉넉하면 윗 방향으로 SPEED만큼의 힘으로 점프, 모자라면 무시
        if (!isShootable) //(_rocketEnergySystem.Fuel < FUELPERSHOOT) 
            return;

        OnShoot?.Invoke();
        // 다른 클래스의 기능에 의존 : 다른 클래스의 기능을 알아야한다는 것
        // _rocketEnergySystem.UseFuel(FUELPERSHOOT);

        _rb2d.AddForce(Vector2.up * SPEED, ForceMode2D.Impulse);
    }
    
    void SetShootable(bool isOn)
    {
        isShootable = isOn;
    }
}
