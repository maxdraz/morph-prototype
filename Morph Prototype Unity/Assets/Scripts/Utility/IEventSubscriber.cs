using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventSubscriber
{
    IEnumerator SubscribeToEventsCoroutine();
    void UnsubscribeFromEvents();
}
