using Protocol.C2S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol
{
    public static class CommonCheckMsg
    {
        public const int accountMin = 6;
        public const int accountMax = 16;

        public const int passwordMin = 6;
        public const int passwordMax = 16;

        public static ErrorCode CheckRegisterAccount(C2SRegisterAccount msg)
        {
            var res = CheckAccount(msg.comData.account);
            if (res != ErrorCode.Succeed)
            {
                return res;
            }

            res = CheckPassword(msg.comData.password);
            if (res != ErrorCode.Succeed)
            {
                return res;
            }

            res = CheckPhone(msg.comData.password);
            if (res != ErrorCode.Succeed)
            {
                return res;
            }

            res = CheckID(msg.comData.id);
            if (res != ErrorCode.Succeed)
            {
                return res;
            }

            return ErrorCode.Succeed;
        }

        public static ErrorCode CheckID(string id)
        {
            if (id.Length != 18)
            {
                return ErrorCode.PhoneLengthError;
            }

            return ErrorCode.Succeed;
        }

        public static ErrorCode CheckPhone(string phone)
        {
            if (phone.Length != 11)
            {
                return ErrorCode.PhoneLengthError;
            }
            return ErrorCode.Succeed;
        }

        public static ErrorCode CheckPassword(string password)
        {
            if (password.Length < passwordMin || password.Length >= passwordMax)
            {
                return password.Length < passwordMin ? ErrorCode.PasswordShort : ErrorCode.PasswordOverLength;
            }
            return ErrorCode.Succeed;
        }

        public static ErrorCode CheckAccount(string account)
        {
            if (account.Length < accountMin || account.Length >= accountMax)
            {
                return account.Length < accountMin ? ErrorCode.AccountShort : ErrorCode.AccountOverLength;
            }
            return ErrorCode.Succeed;
        }
    }
}
