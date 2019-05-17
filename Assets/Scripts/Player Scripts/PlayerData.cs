using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float speed;
    public int damage;
    public int health;

    public float GetSpeed() {
        return speed;
    }

    public int GetDamage() {
        return damage;
    }

    public void DamagePlayer(int dmg) {
        health -= dmg;
        CheckStatus();
    }

    void CheckStatus() {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
