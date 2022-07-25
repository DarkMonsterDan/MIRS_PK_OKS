using OpenQA.Selenium;

using SeleniumExtras.PageObjects;

namespace MIRSPKOKS.PagesQA
{
    class MainPage
    {
        [FindsBy(How = How.Id, Using = "ext-gen1044")]
        public IWebElement Iframe_test { get; set; }

        [FindsBy(How = How.XPath, Using = "//li[contains(., 'Надзор')]")]
        public IWebElement Nadzor { get; set; }

        [FindsBy(How = How.XPath, Using = "//iframe[@src = '../../../altair/admin/html/dist/dashboard.php?id_dashboard=65']")]
        public IWebElement Iframe_test2 { get; set; }

        [FindsBy(How = How.Id, Using = "grid_82")]
        public IWebElement Iframe_test3 { get; set; }

        [FindsBy(How = How.XPath, Using = "//tr[contains(., 'тестовая запись от 19.05.22')]")]
        public IWebElement Test_line_object { get; set; }

        [FindsBy(How = How.Id, Using = "button-1067-btnEl")]
        public IWebElement Edit_button_object { get; set; }

        [FindsBy(How = How.XPath, Using = "//tr[contains(., 'Акты проверок')]")]
        public IWebElement Akt_Proverki { get; set; }

        [FindsBy(How = How.XPath, Using = "//iframe[@src = 'grid.php?id=81617&id_obj=81617&id_master=99759186&id_slave=0&id_field=15919&obj_card=1&ids=15919&list=1&popup=1&popupTp=6&outref=1&id_ref=86332&id_ref_type=23&if_hist=undefined&id_combo=undefined&if_mongo=undefined&grey_style=1&if_tab=1']")]
        public IWebElement Iframe_test4 { get; set; }

        [FindsBy(How = How.XPath, Using = "//tr[contains(., 'Акт выездного обследования') and contains(., 'Проект')]")]
        public IWebElement Akt_Viezdnogo { get; set; }

        [FindsBy(How = How.Id, Using = "button-1067-btnEl")]
        public IWebElement Edit_button_akt { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(., 'Журнал')]")]
        public IWebElement Zhurnal { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(., 'Подписать')]")]
        public IWebElement Podpis { get; set; }

        [FindsBy(How = How.XPath, Using = "//label[contains(., 'Добавить автоматически сформированную печатную форму документа')]")]
        public IWebElement Radio_button_Avtomat { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(., 'Ок')]")]
        public IWebElement Ok_button { get; set; }

        [FindsBy(How = How.XPath, Using = "//tr[contains(., ' Петров Евгений Леонидович')]")]
        public IWebElement Sertif_petrov { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(., 'Выбрать')]")]
        public IWebElement Vibrat_button { get; set; }

        [FindsBy(How = How.XPath, Using = "//td[contains(., 'Подписано')]")]
        public IWebElement Text_podpis { get; set; }

    }
}
