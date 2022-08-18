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

        [FindsBy(How = How.XPath, Using = "//tr[contains(., ' Иванов Иван Иванович')]")]
        public IWebElement Sertif_petrov { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(., 'Выбрать')]")]
        public IWebElement Vibrat_button { get; set; }

        [FindsBy(How = How.XPath, Using = "//td[contains(., 'Подписано')]")]
        public IWebElement Text_podpis { get; set; }



        [FindsBy(How = How.Id, Using = "button-1066-btnEl")]
        public IWebElement Creat_button_akt { get; set; }

        [FindsBy(How = How.XPath, Using = "//label[contains(., 'Акт выездного обследования')]")]
        public IWebElement RadioButton_AktViezdnogo { get; set; }

        //  [FindsBy(How = How.XPath, Using = "//input[@id='srcext-gen4841-inputEl']")]

      //  [FindsBy(How = How.XPath, Using = "//*[@class='x-boundlist-list-ct']/ul/li[1]")]
      

        [FindsBy(How = How.Name, Using = "attr_82020_")]
        public IWebElement Nomer { get; set; }

        [FindsBy(How = How.Name, Using = "attr_82021_")]
        public IWebElement Date { get; set; }

        [FindsBy(How = How.XPath, Using = ".//span[@id='view_attr_99758969_']/table/tbody/tr/td[2]/table/tbody/tr/td[3]/div")]
        public IWebElement Mnoyu { get; set; }

        [FindsBy(How = How.XPath, Using = ".//div[@class='x-boundlist x-boundlist-floating x-layer x-boundlist-default'][1]/div[@class='x-boundlist-list-ct']/ul/li[1]")]
        public IWebElement Mnoyu_spisok { get; set; }

        [FindsBy(How = How.XPath, Using = ".//span[@id='view_attr_87507_']/table/tbody/tr/td[2]/table/tbody/tr/td[3]/div")]
        public IWebElement Na_osnovanii { get; set; }

        [FindsBy(How = How.XPath, Using = ".//div[@class='x-boundlist x-boundlist-floating x-layer x-boundlist-default'][2]/div[@class='x-boundlist-list-ct']/ul/li[1]")]
        public IWebElement Na_osnovanii_spisok { get; set; }

        [FindsBy(How = How.Name, Using = "attr_87516_")]
        public IWebElement Date_provedeniya { get; set; }

        [FindsBy(How = How.Name, Using = "attr_99759278_")]
        public IWebElement Po_resultatam_ustanovleno { get; set; }

        [FindsBy(How = How.XPath, Using = ".//span[@id='view_attr_99759083_']/table/tbody/tr/td[2]/table/tbody/tr/td[3]/div")]
        public IWebElement Podpisivayushiy { get; set; }
        
        [FindsBy(How = How.XPath, Using = ".//div[@class='x-boundlist x-boundlist-floating x-layer x-boundlist-default'][3]/div[@class='x-boundlist-list-ct']/ul/li[1]")]
        public IWebElement Podpisivayushiy_spisok { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(., 'Сохранить')]")]
        public IWebElement Save_document { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(., 'Акт выездного обследования')]")]
        public IWebElement Akt_viezdnogo_obsledovaniya_vkladka { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(., 'Перенести действия из задания')]")]
        public IWebElement Perenesti_deistvie_iz_zadaniya { get; set; }


        [FindsBy(How = How.XPath, Using = "//tr[contains(., 'Акт выездного обследования') and contains(., 'Подписан') and contains(., 'Auto_test')]")]
        public IWebElement Akt_Viezdnogo_delete { get; set; }

        [FindsBy(How = How.Id, Using = "button-1068-btnWrap")]
        public IWebElement Delete_button_object { get; set; }

        [FindsBy(How = How.Id, Using = "button-1005-btnWrap")]
        public IWebElement Delete_OK { get; set; }

       
    }
}
