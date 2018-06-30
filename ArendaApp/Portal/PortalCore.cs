using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArendaRESTLib;

namespace Portal
{
    // Главный класс портала
    public class PortalCore
    {
        // Список параметров GET
        public Dictionary<String, String> _get;
        // Список полученных COOKIE
        public Dictionary<String, String> _cookie;
        // Код пользователя
        private string userid;

        // Получение переменной GET
        private String getVar(string name)
        {
            if (_get.ContainsKey(name)) return _get[name]; else return "";
        }
        // Получение cookies
        private String getCookie(string name)
        {
            if (_cookie.ContainsKey(name)) return _cookie[name]; else return "";
        }
        // Ссылка на экземплятор HTTP-запроса
        private HttpProcessor p;
        public PortalCore(HttpProcessor Ap)
        {
            p = Ap;
            _get = new Dictionary<String, String>();
            _cookie = new Dictionary<String, String>();
        }
        // Провка, авторизован ли пользователь
        private bool isAuthMode()
        {
            if (getVar("logout") == "1")
            {
                p.setCookie("PortalUser", "");
                return false;
            }
            return (userid != "");
        }
        // Получение обработанного шаблона сайта
        public Template getResult()
        {
            bool conn= ObjModule.OpenChannel();
            bool conn2 = ObjModule.OpenChannel2();
            bool conn3 = ObjModule.OpenChannel3();

            // Получение пользователя из сессии
            userid = getCookie("PortalUser");
            // Получение текущей страницы
            String page = getVar("page");
            if (page == "") page = "start";

            Template html = new Template("main");
            
                        
            Template subpage = null;

            if ((!conn)||(!conn2)||(!conn3)) page="error" ;

            // Обработка стартовой страницы
            if (page == "start")
            {
                subpage = new Template(page);
            }
            else
            if (page == "error")
            {
                subpage = new Template(page);
                subpage.setVar("SERVICE",Config.ArendaURL) ;
                subpage.setVar("ERRMSG", ObjModule.errmsg);
            }
            else
                if (page == "about")
                {
                    subpage = new Template(page);
                }
                else
                    if (page == "reg")
                    {
                        subpage = new Template("reg");
                        // Если пришла регистрация, то добавляем пользователя
                        if (getVar("login") != "")
                        {
                            String msg = "";
                            DataModule.addUser(getVar("login"), getVar("pass"), getVar("email"), ref msg);
                            subpage.setVar("MSG", msg);
                        }
                        else
                            subpage.setVar("MSG", "");
                    }
            else
                        if (page == "bill")
                        {
                            subpage = new Template("bill");
                            // Если оплата
                            if (getVar("sum") != "")
                            {
                                ObjModule.channel2.IncSum(userid, Int32.Parse(getVar("sum")));
                                subpage.setVar("MSG", "Успешное пополнение счета");
                            }
                            else
                                subpage.setVar("MSG", "");
                        }
                        else
                            if (page == "reserve")
                            {
                                subpage = new Template("reserve");
                                // Бронь
                                string selid = getVar("selid");

                                subpage.setVar("SELID", selid);
                                ArendaItem item = ObjModule.channel.GetItemByID(selid);

                                subpage.setVar("ID", item.id);
                                subpage.setVar("TYPE", item.roomtype);
                                subpage.setVar("PRICE", item.price);
                                subpage.setVar("S", item.s);
                                subpage.setVar("CITY", item.city);
                                subpage.setVar("ADDRESS", item.address);
                                subpage.setVar("ELITE", item.elite ? "Премиум" : "Стандарт");
                                    
                                if (getVar("isok") != "")
                                {
                                    if (!ObjModule.channel2.DecSum(userid,item.price))
                                        subpage.setVar("MSG", "Недостаточно средств на счете");
                                    else
                                    {
                                        ObjModule.channel3.DoReserveRoom(userid,item.id);

                                        subpage.setVar("REDIR", "1");
                                        subpage.setVar("MSG", "Успешное бронирование");
                                    }
                                }
                                else
                                    subpage.setVar("MSG", "");
                            }
                            else
                        if (page == "auth")
                        {
                            subpage = new Template("auth");
                            if (getVar("login") != "")
                            {
                                // И выполняем авторизацию
                                if (DataModule.isAuthOK(getVar("login"), getVar("pass"), ref userid))
                                {
                                    subpage.setVar("MSG", "Авторизация успешна");
                                    subpage.setVar("REDIR", "1");
                                    // с установкой сессии в cookie
                                    p.setCookie("PortalUser", userid);
                                }
                                else
                                    subpage.setVar("MSG", "Неправильный логин или пароль");
                            }
                            subpage.setVar("MSG", "");
                        }
                        else
                            // Обработка страницы поиска
                            if (page == "findrooms")
                            {
                                subpage = new Template(page);
                                string tekcity = getVar("citylist");
                                subpage.setVar("USER_ID", userid);

                                List<String> citylist = ObjModule.channel.GetCityList();
                                string options = "";
                                foreach (var opt in citylist)
                                    options += "<option value='" + opt + "' " + (tekcity.Equals(opt) ? "selected" : "") + ">" + opt + "</option>";
                                subpage.setVar("CITYLIST", options);

                                List<String> ids = ObjModule.channel.GetItems();

                                string roomshtml = "";
                                foreach (var id in ids)
                                {
                                    ArendaItem item = ObjModule.channel.GetItemByID(id);
                                    if ((tekcity == "") || (tekcity.Equals(item.city)))
                                    {
                                        Template roomtpl = new Template("_room");
                                        roomtpl.setVar("ID", id);
                                        roomtpl.setVar("TYPE", item.roomtype);
                                        roomtpl.setVar("PRICE", item.price);
                                        roomtpl.setVar("S", item.s);
                                        roomtpl.setVar("CITY", item.city);
                                        roomtpl.setVar("ADDRESS", item.address);
                                        roomtpl.setVar("ELITE", item.elite ? "Премиум" : "Стандарт");
                                        
                                        bool isres = ObjModule.channel3.IsRoomReserved(userid,id);
                                        roomtpl.setVar("RESERVED", isres ? "none" : "");
                                        roomtpl.setVar("NORESERVED", isres ? "" : "none");
                                        
                                        roomshtml += roomtpl.HTML();
                                    }
                                }
                                subpage.setVar("ROOMS", roomshtml);

                            }
                            else
                            {
                                subpage = new Template("notfound");
                                subpage.setVar("PAGE", page);
                            }

            if (isAuthMode())
            {
                html.setVar("AUTHIN", "none");
                html.setVar("NOAUTHIN", "");
                html.setVar("USERNAME", DataModule.getUserName(userid));
                html.setVar("SUM", ObjModule.channel2.GetAccountID(userid));
            }
            else
            {
                html.setVar("AUTHIN", "");
                html.setVar("NOAUTHIN", "none");
                html.setVar("USERNAME", "Гость");
            }

            html.setVar("PAGEDATA", subpage.HTML());
            // Добавляем случайное значения для защиты от кэширования
            html.setVar("RND", (new Random()).Next(10000).ToString("D"));
            html.setVar("PAGE", page);
             
            return html;

            
        }
         
    }
}
