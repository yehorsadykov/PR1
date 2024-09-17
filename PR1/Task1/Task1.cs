using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ind {
   class Notes {
      // список для зберігання всіх нотаток
      public static List<Note> NotesList = new List<Note>();

      // метод для додавання нотатки до списку
      public static void Add(Note n) {
         NotesList.Add(n);
      }

      // метод для виведення всіх нотаток на екран
      public static void Show() {
         foreach (Note n in NotesList) {
            n.PrintToConsole();
         }
      }
   }

   class Note {
      // властивості нотатки
      public string Id { get; private set; }
      public string Date { get; private set; }
      public string Time { get; private set; }
      public string Subject { get; private set; }
      public string Tel { get; private set; }

      // конструктор для ініціалізації властивостей нотатки
      public Note(string id, string date, string time, string subject, string tel) {
         Id = id;
         Date = date;
         Time = time;
         Subject = subject;
         Tel = tel;
      }

      // метод для виведення інформації про нотатку на екран
      public void PrintToConsole() {
         Console.WriteLine($"ID: {Id}");
         Console.WriteLine($"Дата: {Date}");
         Console.WriteLine($"Час: {Time}");
         Console.WriteLine($"Тема: {Subject}");
         Console.WriteLine($"Телефон: {Tel}");
      }
   }

   class Program {
      static void Main(string[] args) {
         // завантаження XML-документу
         XmlDocument xml = new XmlDocument();
         xml.Load("notes.xml");

         // проходимо через кожен вузол <note> в документі
         foreach (XmlNode node in xml.DocumentElement) {
            // отримуємо атрибути та елементи нотатки
            string id = node.Attributes["id"].InnerText;
            string date = node.Attributes["date"].InnerText;
            string time = node.Attributes["time"].InnerText;
            string subject = node["subject"].InnerText;
            string tel = node["text"]["tel"].InnerText;

            // додаємо нову нотатку до списку нотаток
            Notes.Add(new Note(id, date, time, subject, tel));
         }

         // виводимо всі нотатки на екран
         Notes.Show();
      }
   }
}