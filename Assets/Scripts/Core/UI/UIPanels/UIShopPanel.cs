using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.UI.Panels
{
    public class UIShopPanel : UIPanel
    {
        public override void BindEvents()
        {
            
        }

        public override void Initialize()
        {
            
        }

        public override void UnBindEvents()
        {

        }

        private void OnDestroy()
        {
            UnBindEvents();
        }
    }
}
