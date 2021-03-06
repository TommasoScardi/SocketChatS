using LibChatClient.Protocol;
using System;

namespace LibChatClient
{
    public enum ErrorsType : int
    {
        Null = -1,
        Generic = 1,
        CommandNotRecognized = 2,
        ArgumentNull = 10,
        UserNotAuth = 50,

        AuthGeneric = 100,
        AuthUserInexistent = 101,
        AuthBadDatas = 101, //User dont exist or bad login datas
        AuthUserAlreadyAuth = 102,
        AuthIPChange = 105,

        NewUserRegGeneric = 200,
        NewUserRegBadDatas = 201,

        ReqContactsGeneric = 300,
        ReqContactsInexistent = 301,

        LoadContactGeneric = 400,
        LoadContactInexistent = 401,
        LoadContactMessageEmpty = 402,

        NewContactGeneric = 500,
        NewContactInexistent = 501,
        NewContactUnableToAdd = 502,

        ReqNewMsgGeneric = 600,
        ReqNewMsgNoNewMsg = 601,

        SndMsgGeneric = 700,
        SndMsgContactInexistent = 701,
        SndMsgUnableToSend = 702,

        DeAuthGeneric = 800,
        DeAuthUserNotFound = 801,

    }

    public static class ErrorType
    {
        public static bool TryParse(string s, out ErrorsType error, bool removeTerm = false)
        {
            if (s.Length > (Error.ProtocolString.Length + EOF.ProtocolString.Length))
            {
                if (removeTerm)
                    s = Protocols.RemoveTerminator(s);
                error = (ErrorsType)Enum.Parse(typeof(ErrorsType), Protocols.RemoveErrorMark(s));
                return true;
            }
            else
            {
                error = ErrorsType.Null;
                return false;
            }
        }

        public static string ToString(ErrorsType error)
        {
            return $"{Error.ProtocolString}{error}{EOF.ProtocolString}";
        }
    }
}
