using Protocol.C2S;
using Protocol.CommonData;
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

        public static ErrorCode CheckBorrowMoney(BorrowInformatio borrow)
        {
            var res = CheckAccount(borrow.account);
            if (res != ErrorCode.Succeed)
            {
                return res;
            }

            return res;
        }


        public static ErrorCode CheckAppData(AppData appData)
        {
            ErrorCode errorCode = ErrorCode.Succeed;
            if (appData.interests < 0.01 || appData.interests > 0.5)
            {
                return ErrorCode.InterestsError;
            }
            if (appData.limitOfMoney < 5000 || appData.limitOfMoney>300000)
            {
                return ErrorCode.LimitOfMoneyError;
            }
            if (CheckStages(appData.numberStages)!=ErrorCode.Succeed)
            {
                return ErrorCode.NumberStagesError;
            }
            if (string.IsNullOrEmpty(appData.borrowTitle))
            {
                return ErrorCode.BorrowTitleError;
            }
            if (string.IsNullOrEmpty(appData.borrowContent))
            {
                return ErrorCode.BorrowContentError;
            }

            return errorCode;
        }

        public static ErrorCode CheckStages(int numberStages)
        {
            if (numberStages < 1 || numberStages > 36)
            {
                return ErrorCode.NumberStagesError;
            }
            return ErrorCode.Succeed;
        }

        public static ErrorCode CheckRegisterAccount(C2SRegisterAccount msg)
        {
            return CheckRegisterAccount(msg.comData);
        }

        public static ErrorCode CheckRegisterAccount(CommonAccountData data)
        {
            var res = CheckAccount(data.account);
            if (res != ErrorCode.Succeed)
            {
                return res;
            }

            res = CheckPassword(data.password);
            if (res != ErrorCode.Succeed)
            {
                return res;
            }

            res = CheckPhone(data.phone);
            if (res != ErrorCode.Succeed)
            {
                return res;
            }

            res = CheckID(data.id);
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
                return ErrorCode.IDLengthError;
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
