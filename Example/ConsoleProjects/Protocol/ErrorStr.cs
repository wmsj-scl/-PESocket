namespace Protocol
{
    public static class ErrorStr
    {
        public static string GetErrorStr(ErrorCode errorCode)
        {
            switch (errorCode)
            {
                case ErrorCode.Succeed:
                    return "成功";
                case ErrorCode.AccountExists:
                    return "账号已存在";
                case ErrorCode.AccountOverLength:
                    return "账号长度大于："+CommonCheckMsg.accountMax;
                case ErrorCode.AccountShort:
                    return "账号长度小于："+CommonCheckMsg.accountMin;
                case ErrorCode.FailedFileNotExists:
                    return "文件不存在";
                case ErrorCode.FailedReadFile:
                    return "文件读取失败";
                case ErrorCode.IDLengthError:
                    return "身份证长度错误";
                case ErrorCode.NoExecution:
                    return "未知错误";
                case ErrorCode.PasswordOverLength:
                    return "密码长度大于：" + CommonCheckMsg.passwordMax;
                case ErrorCode.PasswordShort:
                    return "密码长度小于："+ CommonCheckMsg.passwordMin;
                case ErrorCode.PhoneLengthError:
                    return "手机号长度错误";
                case ErrorCode.PasswordError:
                    return "密码错误";
                case ErrorCode.AccountNotExists:
                    return "账号不存在 请前往注册";
                case ErrorCode.InterestsError:
                    return "利率设置错误！";
                case ErrorCode.LimitOfMoneyError:
                    return "贷款额度设置失败";
                case ErrorCode.NumberStagesError:
                    return "分期上限设置错误 小于 3 或 大于 36";
                case ErrorCode.BorrowTitleError:
                    return "分期协议标题不可为空";
                case ErrorCode.BorrowContentError:
                    return "分期协议不可为空";
                case ErrorCode.AccountNoRight:
                    return "账户没有权限";
                case ErrorCode.AppCfgNotSet:
                    return " 应用数据还未设置，请先联系管理员设置应用数据!";
                case ErrorCode.OutLimitOfMoney:
                    return "借款金额超出上限!";
            }
            return errorCode.ToString();
        }
    }
}
