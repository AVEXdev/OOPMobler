﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace OOPMöbler.Models
{
    public class Möbel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Count { get; set; }
        public int InitialCount { get; set; }
        public int BuyCount { get; set; }
        public int Points { get; set; }
        public int tempPointsInitialCount { get; set; }
        public int tempPointsBuyCount { get; set; }
        public string färg { get; set; }
        public int pris { get; set; }
        // Våran lista där vi lagt in olika variabler som beskriver produkten, här t ex Soffa, Stol och Bord, med olika beskrivning
        public static List<Möbel> CreateData()
        {
            List<Möbel> MöbelList = new List<Möbel>();
            MöbelList.Add(new Soffa { Id = 1, Title = "Erik", Author = "Ikea", sittPlatser = 4, utsida = "Läder", färg = "Grå", pris = 4599, Count = 5, InitialCount = 5 });
            MöbelList.Add(new Stol { Id = 4, Title = "Sofia", Author = "Ikea", Age = 2016, antalBen = 4, färg = "Svart", Count = 2, pris = 229, InitialCount = 2 });
            MöbelList.Add(new Bord { Id = 6, Title = "Fora", Author = "Ikea", Age = 2017, antalBen = 6, färg = "Vit", pris = 499, Count = 6, InitialCount = 6 });
            return MöbelList;
        }
        // Våran directory för att spara data i json fil
        public static string filepath = HttpContext.Current.Server.MapPath("~/App_Data/Storage/library.json");
        // Här sparar vi våran möbellista i json filen där vi använder oss utav Newtonsoft
        public static bool SaveData(List<Möbel> möbellist)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented
            };
            string json = JsonConvert.SerializeObject(möbellist.ToArray(), settings);
            System.IO.File.WriteAllText(filepath, json);
            return true;
        }
        // Här hämtar vi data från json filen, och definerar en variabel (data) för att gå till skapa data ifall filen inte existerar dit vi söker oss
        public static List<Möbel> GetData()
        {
            List<Möbel> data;
            if (System.IO.File.Exists(filepath))
            {
                var settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Objects
                };
                var json = System.IO.File.ReadAllText(filepath);
                data = JsonConvert.DeserializeObject<List<Möbel>>(json, settings);
            }
            else
            {
                data = CreateData();
            }
            // Våran algoritm för poäng systemet för att rangordna varje vara i en "lista" där vi kan bestämma vilken som ska vara högst upp
            // eller längst ner beroende på popularitet och framförallt köp
            data = data.OrderBy(x => x.InitialCount).ToList();
            int points = 0;
            foreach (var d1 in data)
            {
                points = points + 5;
                d1.tempPointsInitialCount = points;
                d1.Points = points;
            }
            data = data.OrderBy(x => x.BuyCount).ToList();
            points = 0;
            foreach (var d2 in data)
            {
                points = points + 3;
                d2.tempPointsBuyCount = points;
                d2.Points += points;
            }
            // Populärast högst upp (Descending går neråt) och sparar datan
            data = data.OrderByDescending(x => x.Points).ToList();
            SaveData(data);
            return data;
        }
    }
    // Specifika klasser skapade för varje möbel, då varje möbel har olika attributer
    public class Soffa : Möbel
    {
        public string utsida { get; set; }
        public int sittPlatser { get; set; }
    }
    public class Stol : Möbel
    {
        public int Age { get; set; }
        public int antalBen { get; set; }
    }
    public class Bord : Möbel
    {
        public int Age { get; set; }
        public int antalBen { get; set; }
    }
}