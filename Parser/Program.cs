using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;


namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            ChromeOptions options = new ChromeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };
            options.AddArgument("--disable-in-process-stack-traces");
            options.AddArgument("--disable-logging");
            options.AddArgument("--disable-extensions");
            //options.AddArgument("--headless");
            //options.AddArgument("--disable-gpu");
            //options.AddArgument("--log-level=3");

            IWebDriver driver = new ChromeDriver(options)
            {
                Url = "https://ircenter.gov.ua/index/dipo"
            };
            Console.WriteLine("Pages parse starting...");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            List<string> firstLinks = new List<string>(), secondLinks = new List<string>(), resList = new List<string>();
            
            IWebElement firstResult;


            List<IWebElement> listOfLinks = wait.Until(e => e.FindElements(By.XPath($"//*[@id='side-menu']/li"))).ToList();
            foreach (IWebElement a in listOfLinks)
            {
                firstLinks.Add(a.FindElement(By.TagName("a")).GetAttribute("href"));
            }

            foreach (var item in firstLinks)
            {
                Console.WriteLine($"Parsing {item.ToString()}");
                driver.Navigate().GoToUrl(item.ToString());
                firstResult = wait.Until(e => e.FindElement(By.XPath("//*[@id='main-content']/ul/li[2]/a")));
                driver.Navigate().GoToUrl(firstResult.GetAttribute("href"));
                listOfLinks = wait.Until(e => e.FindElements(By.XPath($"//*[@id='main-content']/div/div[3]/div/table[2]/tbody/tr"))).ToList();
                foreach (IWebElement a in listOfLinks)                  
                {
                    secondLinks.Add(a.FindElement(By.TagName("a")).GetAttribute("href"));
                }
                driver.Navigate().Back();
                //if (secondLinks.Count() > 0) break;
            }

            foreach (var item in secondLinks)
            {
                Console.WriteLine($"Parsing {item.ToString()}");
                string str = String.Empty;
                driver.Navigate().GoToUrl(item.ToString());
                str = string.Concat(wait.Until(e => e.FindElement(By.XPath("//*[@id='main-content']/div/div[3]/table/tbody/tr[1]/td[1]"))).Text, ";");
                for (int i = 2; i < 15; i++)
                {
                    str = string.Concat(str, wait.Until(e => e.FindElement(By.XPath($"//*[@id='main-content']/div/div[3]/table/tbody/tr[{i}]/td"))).Text, ";");
                }

                resList.Add(str);
             }


            //foreach (var item in firstLinks)
            //{
            //    driver.Navigate().GoToUrl(item.ToString());
            //    List<IWebElement> listOfElements = driver.FindElements(By.XPath($"//*[@id='side-menu']/ul/li/ul/li")).ToList();
            //    foreach (IWebElement b in listOfElements)
            //        secondLinks.Add(b.FindElement(By.TagName("a")).GetAttribute("href"));
            //    driver.Navigate().Back();
            //}

                                          
            //for (int i = 2; i < 3; i++)
            //{
            //    try
            //    {
            //        firstResult = wait.Until(e => e.FindElement(By.XPath($"//*[@id='side-menu']/li[{i}]/a")));
            //    }
            //    catch (Exception)
            //    {
            //        break;
            //    }

            //    //firstResult = driver.FindElement(By.XPath($"//*[@id='side-menu']/li[{i}]/a"));
            //    driver.Navigate().GoToUrl(firstResult.GetAttribute("href"));

            //    for (int y = 0; y < 100; y++)
            //    {
            //        try
            //        {
            //            firstResult = wait.Until(e => e.FindElement(By.XPath($"//*[@id='side-menu']/ul/li/ul/li[{y}]/a")));
            //        }
            //        catch (Exception)
            //        {
            //            break;
            //        }
            //        driver.Navigate().GoToUrl(firstResult.GetAttribute("href"));

            //        //*[@id="main-content"]/ul/li[2]


            //        //List<IWebElement> listOfElements = driver.FindElements(By.XPath($"//*[@id='side-menu']/ul/li/ul/li[{y}]/a")).ToList();
            //        //foreach (IWebElement a in listOfElements)
            //        //    names.Add(a.Text);

            //    }
            //    driver.Navigate().Back();
            //}



            //IWebElement firstResult;
            //int i = 1;
            //do
            //{
            //    try
            //    {
            //        Console.WriteLine($"Page {i} parsing...");

            //        List<IWebElement> listOfElements = driver.FindElements(By.XPath("//*[@id='side - menu']/li/a")).ToList();
            //        foreach (IWebElement a in listOfElements)
            //            names.Add(a.Text);


            //        firstResult = wait.Until(e => e.FindElement(By.XPath($"//*[@id='right_block_ajax']/div/div[3]/div[2]/div[2]/div/a[{i}]")));

            //        i++;
            //        driver.Navigate().GoToUrl(firstResult.GetAttribute("href"));
            //    }
            //    catch (Exception)
            //    {
            //        break;
            //    }
            //}
            //while (true);


            foreach (var item in secondLinks)
            {
                Console.WriteLine(item.ToString());
            }

            //System.IO.File.WriteAllLines("d:\\SavedLists.txt", firstLinks);
            //System.IO.File.WriteAllLines("d:\\SavedLists2.txt", secondLinks);
            System.IO.File.WriteAllLines("d:\\SavedLists3.txt", resList);

            driver.Quit();
            Console.WriteLine("--> The end <--");
            Console.ReadLine();

            //List<IWebElement> listOfPages = driver.FindElements(By.XPath("//*[@id='right_block_ajax']/div/div[3]/div[2]/div[2]/div/a")).ToList();

            //foreach (IWebElement a in listOfPages)
            //{
            //    driver.Navigate().GoToUrl(a.GetAttribute("href"));

            //    listOfElements = driver.FindElements(By.XPath("//*[@class='catalog_item main_item_wrapper item_wrap ']/div/div[2]/div[1]/div[1]/a/span")).ToList();

            //    foreach (IWebElement b in listOfElements)
            //        names.Add(b.Text);

            //    for (int i = 0; i < names.Count; i++)
            //        Console.WriteLine(names[i]);


            //}


            //string pathToFile = AppDomain.CurrentDomain.BaseDirectory + '\\';
            //IWebDriver driver = new ChromeDriver(pathToFile);

            //IWebElement element = driver.FindElement(By.XPath("//button[@id='header-Header sparkButton']"));

            //element.Click();
            //Thread.Sleep(2000);

            //element = driver.FindElement(By.XPath("//button[@data-tl-id='GlobalHeaderDepartmentsMenu-deptButtonFlyout-10']"));

            //element.Click();
            //Thread.Sleep(2000);

            //element = driver.FindElement(By.XPath("//div[text()='Coffee']/parent::a"));
            //driver.Navigate().GoToUrl(element.GetAttribute("href"));
            //Thread.Sleep(10000);

            //List<string> names = new List<string>(), prices = new List<string>();

            //List<IWebElement> listOfElements = driver.FindElements(By.XPath("//div[@class='tile-content']/div[@class='tile-title']/div")).ToList();

            //foreach (IWebElement a in listOfElements)
            //    names.Add(a.Text);

            //listOfElements = driver.FindElements(By.XPath("//div[@class='tile-content']/div[@class='tile-price']/span/span[contains(text(),'$')]")).ToList();

            //foreach (IWebElement a in listOfElements)
            //    prices.Add(a.Text);

            //for (int i = 0; i < prices.Count; i++)
            //    Console.WriteLine(names[i] + " " + prices[i]);


        }
    }
}
