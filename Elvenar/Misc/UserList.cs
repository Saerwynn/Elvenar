using System.Collections.Generic;

namespace Elvenar.Misc
{
    class UserList
    {
        public static List<UserTokens> getUsers()
        {
            var user1 = new UserTokens();
            user1.username = "*******";
            user1.password = "*******";

            var user2 = new UserTokens();
            user2.username = "*******";
            user2.password = "*******";

            var user3 = new UserTokens();
            user3.username = "*******";
            user3.password = "*******";

            var user4 = new UserTokens();
            user4.username = "*******";
            user4.password = "*******";

            var user5 = new UserTokens();
            user5.username = "*******";
            user5.password = "*******";

            var user6 = new UserTokens();
            user6.username = "*******";
            user6.password = "*******";

            var user7 = new UserTokens();
            user7.username = "*******";
            user7.password = "*******";

            var user8 = new UserTokens();
            user8.username = "*******";
            user8.password = "*******";

            var user9 = new UserTokens();
            user9.username = "*******";
            user9.password = "*******";

            var user10 = new UserTokens();
            user10.username = "*******";
            user10.password = "*******";

            var newList = new List<UserTokens>();

            newList.Add(user1);
            newList.Add(user2);
            newList.Add(user3);
            newList.Add(user4);
            newList.Add(user5);
            newList.Add(user6);
            newList.Add(user7);
            newList.Add(user8);
            newList.Add(user9);

            return newList;
        }
    }
}
