using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal
{
    // Класс пользователя портала
    [Serializable]
    public class User
    {
        public int ID;
        public string FIO;
        public string Email;
        public string Login;
        public string Pass;
        public Guid guid;
        public bool isAuthOk(string Alogin, string Apass)
        {
            return (Login == Alogin) && (Pass == Apass);
        }
    }
}
