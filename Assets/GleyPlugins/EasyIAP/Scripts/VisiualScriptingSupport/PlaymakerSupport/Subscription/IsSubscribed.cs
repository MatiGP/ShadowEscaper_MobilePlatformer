﻿#if USE_PLAYMAKER_SUPPORT

using UnityEngine.Purchasing;

namespace HutongGames.PlayMaker.Actions
{
    [HelpUrl("http://gleygames.com/documentation/Gley-EasyIAP-Documentation.pdf")]
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Get Subscription Subscribed State")]
    public class IsSubscribed : FsmStateAction
    {
        [Tooltip("Where to send the event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Subscription product")]
        public ShopProductNames subscriptionProductToCheck;

        [Tooltip("Variable where the Subscribed State will be stored")]
        public FsmString result;


        public override void Reset()
        {
            base.Reset();
            eventTarget = FsmEventTarget.Self;
        }

        public override void OnEnter()
        {
            if (IAPManager.Instance.IsInitialized())
            {
                SubscriptionInfo info = IAPManager.Instance.GetSubscriptionInfo(subscriptionProductToCheck);
                if (info != null)
                {
                    result.Value = info.isSubscribed().ToString();
                }
                else
                {
                    result.Value = "-";
                }

            }
            else
            {
                result.Value = "-";
            }
            Finish();
        }
    }
}
#endif
