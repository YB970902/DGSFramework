using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DGS.Define
{
    public class DefineServerData
    {
        public const byte GAP_OF_KEY = 100; // 현재는 0부터 199를 쓰기 때문에 100이지만, 0부터 99까지만 쓰고 싶다면 50으로 바꿔서 쓸 수 있도록
        // 값은 0부터 199까지의 값이 들어갈 수 있다
        // 0부터 99 까지는 Request를 위해 사용하고, 100부터 199까지는 Response를 위해 사용한다
        public const byte REQ_REGISTER_MY_INFO = 0; // 내 정보를 서버에 등록
        public const byte REQ_USER_INFO = 1;
        public const byte REQ_NEW_USER_INFO = 2;

        public const byte RES_REGISTER_MY_INFO = REQ_REGISTER_MY_INFO + GAP_OF_KEY; // 내 정보 등록 메시지
        public const byte RES_USER_INFO = REQ_USER_INFO + GAP_OF_KEY;
        public const byte RES_NEW_USER_INFO = REQ_NEW_USER_INFO + GAP_OF_KEY;
    }
}
