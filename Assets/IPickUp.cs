using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickUp
{
    // Возврат true, якщо цей предмет можна підняти 
    bool PickUp(Transform hand);
    void Drop();
}
public interface IUsable
{
    // Возврат true, якщо хочете щоб предмет залишився в руках після використання
    bool Use();
}