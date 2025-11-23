using System;
using System.Collections.Generic;

namespace WebsiteBuilderDemo
{
    // ========================
    // PRODUCT – Готовий сайт
    // ========================
    public class Website
    {
        private readonly List<string> _pages = new List<string>();

        public void AddPage(string page)
        {
            _pages.Add(page);
        }

        public void Show()
        {
            Console.WriteLine("======= Сайт створено =======");
            foreach (var page in _pages)
            {
                Console.WriteLine(page);
            }
            Console.WriteLine("==================================");
        }
    }

    // =======================================
    // BUILDER – Інтерфейс будівельника сайту
    // =======================================
    public interface IWebsiteBuilder
    {
        void BuildHomePage();
        void BuildContactsPage();
        void BuildServicesPage();
        Website GetWebsite();
    }

    // ==================================================
    // CONCRETE BUILDER – Конкретний будівельник сайту
    // ==================================================
    public class CorporateWebsiteBuilder : IWebsiteBuilder
    {
        private readonly Website _website = new Website();

        public void BuildHomePage()
        {
            _website.AddPage("Головна сторінка: Вітання, банер, навігація.");
        }

        public void BuildContactsPage()
        {
            _website.AddPage("Контакти: Email, телефон, карта, форма зворотного зв’язку.");
        }

        public void BuildServicesPage()
        {
            _website.AddPage("Послуги: список послуг компанії, описи, ціни.");
        }

        public Website GetWebsite()
        {
            return _website;
        }
    }

    // ==============================
    // DIRECTOR – Керує побудовою
    // ==============================
    public class WebsiteDirector
    {
        private IWebsiteBuilder _builder;

        public WebsiteDirector(IWebsiteBuilder builder)
        {
            _builder = builder;
        }

        public void SetBuilder(IWebsiteBuilder builder)
        {
            _builder = builder;
        }

        public Website BuildFullWebsite()
        {
            _builder.BuildHomePage();
            _builder.BuildContactsPage();
            _builder.BuildServicesPage();
            return _builder.GetWebsite();
        }
    }

    // ==============================
    // PROGRAM — Точка входу
    // ==============================
    class Program
    {
        static void Main()
        {
            IWebsiteBuilder builder = new CorporateWebsiteBuilder();
            WebsiteDirector director = new WebsiteDirector(builder);

            // Створення сайту
            Website website = director.BuildFullWebsite();

            // Вивести результат
            website.Show();

            Console.WriteLine("Натисніть будь-яку клавішу...");
            Console.ReadKey();
        }
    }
}
