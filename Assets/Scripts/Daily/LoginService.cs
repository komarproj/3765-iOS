using System;
using Data;
using Game.UserData;

namespace Spinner
{
    public class LoginService
    {
        private readonly SaveSystem _saveSystem;

        public LoginService(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }

        public bool CanDailySpin()
        {
            var record = GetLoginData().LastDailyRewardData;

            if (string.IsNullOrEmpty(record))
                return true;
            
            var now = DateTime.Now;
            var lastDate = Convert.ToDateTime(record);
            
            return now.Date > lastDate.Date;
        }

        public void RecordDailySpin()
        {
            GetLoginData().LastDailyRewardData = DateTime.Now.ToString();
        }

        private LoginData GetLoginData() => _saveSystem.Data.LoginData;
    }
}