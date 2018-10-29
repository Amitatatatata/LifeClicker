using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Ami
{

    public class iTweenEventHandler : MonoBehaviour
    {
        public Action<Vector2> OnUpdateMoveDelegate;

        public void OnUpdateMove(Vector2 value)
        {
            if (OnUpdateMoveDelegate != null)
            {
                OnUpdateMoveDelegate.Invoke(value);
            }
        }

        public Action OnCompleteDelegate;

        public void OnComplete()
        {
            if (OnCompleteDelegate != null)
            {
                OnCompleteDelegate.Invoke();
            }
        }
    }

    public static class iTweenExtension
    {
        private static iTweenEventHandler SetUpEventHandler(GameObject obj)
        {
            iTweenEventHandler eventHandler = obj.GetComponent<iTweenEventHandler>();
            if (eventHandler == null)
            {
                eventHandler = obj.AddComponent<iTweenEventHandler>();
            }

            return eventHandler;
        }

        public static void MoveTo(this RectTransform obj, Vector2 pos,
            float time, float delay, iTween.EaseType easeType,
            Action onCompleteDelegate = null)
        {
            iTweenEventHandler eventHandler = SetUpEventHandler(obj.gameObject);
            eventHandler.OnUpdateMoveDelegate = (Vector2 value) =>
            {
                obj.anchoredPosition = value;
            };

            eventHandler.OnCompleteDelegate = onCompleteDelegate;

            iTween.ValueTo(obj.gameObject, iTween.Hash(
                "from", obj.anchoredPosition,
                "to", pos,
                "time", time,
                "delay", delay,
                "easetype", easeType,
                "onupdate", "OnUpdateMove",
                "onupdatetarget", eventHandler.gameObject,
                "oncomplete", "OnComplete",
                "oncompletetarget", eventHandler.gameObject
                ));
        }
    }

}