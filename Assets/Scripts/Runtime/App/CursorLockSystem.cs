using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hmlca.CS.App
{
    public class CursorLockSystem : Singleton<CursorLockSystem>
    {
        public void SetLock(bool lockCursor)
        {
            Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !lockCursor;
        }


        public void SetUnlock(bool unlockCursor) => SetLock(!unlockCursor);
    }
}
