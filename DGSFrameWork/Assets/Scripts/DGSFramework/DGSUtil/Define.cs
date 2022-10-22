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
    }
}
