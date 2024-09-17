using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace LogParser {
   class Logs {
      // список для зберігання всіх подій логів
      public static List<LogEvent> LogList = new List<LogEvent>();

      // метод для додавання події в список
      public static void Add(LogEvent e) {
         LogList.Add(e);
      }

      // метод для виведення всіх подій в консоль
      public static void Show() {
         foreach (LogEvent log in LogList) {
            log.PrintToConsole();
         }
      }
   }

   class LogEvent {
      // властивості події логів
      public string Date { get; private set; }
      public string Result { get; private set; }
      public string IpFrom { get; private set; }
      public string Method { get; private set; }
      public string UrlTo { get; private set; }
      public int Response { get; private set; }

      // конструктор для ініціалізації властивостей
      public LogEvent(string date, string result, string ipFrom, string method, string urlTo, int response) {
         Date = date;
         Result = result;
         IpFrom = ipFrom;
         Method = method;
         UrlTo = urlTo;
         Response = response;
      }

      // метод для виведення події в консоль
      public void PrintToConsole() {
         Console.WriteLine($"Дата: {Date}");
         Console.WriteLine($"Результат: {Result}");
         Console.WriteLine($"IP: {IpFrom}");
         Console.WriteLine($"Метод: {Method}");
         Console.WriteLine($"URL: {UrlTo}");
         Console.WriteLine($"Код відповіді: {Response}");
         Console.WriteLine();
      }
   }

   class Program {
      static void Main(string[] args) {
         // завантажуємо XML-документ з файлу
         XmlDocument xml = new XmlDocument();
         xml.Load("log.xml");

         // проходимо через кожен вузол <event> в документі
         foreach (XmlNode node in xml.DocumentElement) {
            // отримуємо атрибути та елементи події
            string date = node.Attributes["date"].InnerText;
            string result = node.Attributes["result"].InnerText;
            string ipFrom = node["ip-from"].InnerText.Trim();
            string method = node["method"].InnerText;
            string urlTo = node["url-to"].InnerText;
            int response = Int32.Parse(node["response"].InnerText);

            // додаємо нову подію в список логів
            Logs.Add(new LogEvent(date, result, ipFrom, method, urlTo, response));
         }

         // виводимо всі події
         Logs.Show();
      }
   }
}