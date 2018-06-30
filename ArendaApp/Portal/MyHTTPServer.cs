using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Portal
{
    // Реализация Http-сервера для чата
    public class MyHttpServer : HttpServer
    {
        public MyHttpServer(int port)
            : base(port)
        {
            // Загрузка данных
            
        }
        // Разбор запроса GET
        private void parseGet(Dictionary<String,String> _get, string query)
        {
            string[] param = query.Split(new char[] { '&' });
            
            foreach (string str in param)
            {
                string[] tmp = str.Split(new char[] { '=' });
                if (tmp.Count() == 2)
                    _get.Add(tmp[0], Uri.UnescapeDataString(tmp[1].Replace("+", " ")));
            }

        }
        // Разбор cookies
        private void parseCookie(Dictionary<String, String> _cookie, string cookie)
        {
            string[] param = cookie.Split(new char[] { ';' });

            foreach (string str in param)
            {
                string[] tmp = str.Trim().Split(new char[] { '=' });
                if (tmp.Count() == 2)
                    _cookie.Add(tmp[0].Trim(), Uri.UnescapeDataString(tmp[1].Replace("+", " ")));
            }

        }        
        
        // Обработка запроса GET
        public override void handleGETRequest(HttpProcessor p)
        {
            try
            {
                PortalCore pc = new PortalCore(p);

                // Разбираем параметры
                if (p.httpHeaders["Cookie"] != null) parseCookie(pc._cookie,p.httpHeaders["Cookie"].ToString());
                if (p.http_url.IndexOf('?') != -1) parseGet(pc._get,p.http_url.Substring(p.http_url.IndexOf('?') + 1));

                // Получаем страницу от портала
                Template html = pc.getResult() ;

                // Выводим её в результат
                p.writeSuccess();
                p.outputStream.WriteLine(html.HTML());
                
            }
            catch (Exception e)
            {
                p.writeSuccess();
                p.outputStream.WriteLine("Error in portal app:<br><b>"+e.Message+"</b>");
            }

        }

        public override void handlePOSTRequest(HttpProcessor p, StreamReader inputData)
        {
            // Запрос POST в данной работе не используется
        }
    }

}
