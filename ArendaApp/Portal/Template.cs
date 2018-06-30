using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Portal
{
    // Класс шаблона - возможность записать значение в переменную
    public class Template
    {
        private string tpl;
        public Template(string TplName)
        {
            tpl = File.ReadAllText(TplName+".tpl",Encoding.GetEncoding(1251));
        }
        // Установка переменной - строки
        public void setVar(String var, String varvalue)
        {
            tpl = tpl.Replace("{" + var.ToUpper() + "}", varvalue);
            tpl = tpl.Replace("{" + var.ToLower() + "}", varvalue);
        }
        // Установка переменной - числа
        public void setVar(String var, int varvalue)
        {
            setVar(var, varvalue.ToString("D"));
        }
        public String HTML()
        {
            return tpl;
        }
    }
}
