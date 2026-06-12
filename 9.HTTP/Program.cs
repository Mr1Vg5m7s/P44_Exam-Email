using HtmlAgilityPack;
using System.Net;
using System.Text;

namespace _9.HTTP
{
    internal class Program
    {
        class Car
        {
            public string? Model { get; set; }
            public string? FileName { get; set; }
            public string? VIN { get; set; }
            public decimal Price { get; set; }

            public override string ToString()
            {
                return $"{Model} {Price} {VIN} {FileName}";
            }
        }


        public static async Task DownLoadFile(string url, string path)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    Console.WriteLine("Start download...");
                    byte[] buffer = await client.GetByteArrayAsync(url);
                    await File.WriteAllBytesAsync(path, buffer);
                    Console.WriteLine("Download complete");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }


        static async Task Main(string[] args)
        {
            /// WebServer

            //HttpListener listener = new HttpListener();
            //listener.Prefixes.Add("http://localhost:8080/connect/");
            //listener.Prefixes.Add("http://localhost:8080/info/");
            //listener.Start();

            //var context = listener.GetContext();
            //var request = context.Request;
            //var response = context.Response;
            //var user = context.User;

            //Console.WriteLine($"Адреса серверу : {request.Url}");
            //Console.WriteLine($"Метод запиту   : {request.HttpMethod}");
            ////Console.WriteLine($"Користувач     : {user.Identity.Name}");
            //Console.WriteLine("Заголовки");
            //foreach (string key in request.Headers.Keys)
            //{
            //    Console.WriteLine($"{key} - {request.Headers[key]}");
            //}


            //string responseString = @"
            //<!DOCTYPE html>
            //<html lang=""en"">
            //<head>
            //    <meta charset=""UTF-8"">
            //    <title>Document</title>
            //    <style>
            //        body
            //        {
            ///*            font-family: Tahoma, sans-serif;*/
            //            background-color: #f5eada;
            //        }
            //        sup
            //        {
            //            color : rgb(19, 50, 181);
            //            font-weight: bold;
            //        }

            //        .lower
            //        {
            //            font-size: 12px;
            //        }

            //        .bolder
            //        {
            //            font-weight: bold;
            //        }

            //        .formula
            //        {
            //            text-align: center;
            //            background-color: tomato;
            //            margin: auto;
            //            width: 100px;

            //        }

            //        #bold1
            //        {
            //            font-weight: bold;
            //        }

            //    </style>
            //</head>
            //<body>
            //    <h1> Vehicle</h1>

            //    <p>A vehicle &amp;(from &pi; Latin: &#960; vehiculum<sup>[1]</sup>) is a machine that transports people or cargo. Vehicles include wagons, bicycles, motor vehicles (motorcycles, cars, trucks, buses), railed vehicles (trains, trams), watercraft (ships, boats), amphibious vehicles (screw-propelled vehicle, hovercraft), aircraft (airplanes, helicopters) and spacecraft.<sup>[2]</sup></p>

            //    <p id=""bold1"">Land vehicles are classified broadly by what is used to apply steering and drive forces against the ground: wheeled, tracked, railed or skied. ISO 3833-1977 is the standard, also internationally used in legislation, for road vehicles types, terms and definitions.<sup>[3]</sup></p>
            //    <hr>
            //    <p class=""lower"">[1] - ""vehicle, n."", OED Online, Oxford University Press, November 2010</p>

            //    <p class=""lower"">[2] - Halsey, William D. (Editorial Director): MacMillan Contemporary Dictionary, page 1106. MacMillan Publishing, 1979. ISBN 0-02-080780-5</p>

            //    <p class=""lower bolder"">[3] - ISO 3833:1977 Road vehicles – Types – Terms and definitions Webstore.anis.org</p>

            //</body>
            //</html>";


            //string ttt = @"Hello WORLD";
            //byte[] bytes;
            //if (request.Url.ToString() == "http://localhost:8080/connect/")
            //{
            //    bytes = Encoding.UTF8.GetBytes(responseString);

            //}
            //else
            //{
            //    bytes = Encoding.UTF8.GetBytes(ttt);

            //}
            //using (var stream = response.OutputStream)
            //{
            //    stream.Write(bytes, 0, bytes.Length);
            //}

            //listener.Stop();
            //listener.Close();


            /// DownLoad File

            //await DownLoadFile("https://cdn3.riastatic.com/photosnew/auto/photo/_escape__643610718fx.webp", "auto.png");



            /// parsing HTML

            string url = "https://auto.ria.com/uk/search/?search_type=1&category=1&all[0].any[0].brand=24&all[0].any[0].any[0].model=1183&all[0].any[0].year[0]=2016&all[0].any[0].year[1]=2016&abroad=0&customs_cleared=1&page=";
            HttpClient client = new HttpClient();
            List<Car> cars = new List<Car>();
            int page = 0;
            while (true)
            {
                try
                {
                    string htmlContent;
                    string urlPage = url + (page++).ToString();
                    try
                    {
                        htmlContent = await client.GetStringAsync(urlPage);
                    }
                    catch (Exception)
                    {
                        break;
                    }

                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(htmlContent);

                    if(!Directory.Exists("image"))
                    {
                        Directory.CreateDirectory("image");
                    }

                    var items = doc.DocumentNode.SelectNodes("//a[@class='link product-card horizontal']");
                    foreach (var item in items)
                    {
                        Car car = new Car();
                        car.FileName = item.SelectSingleNode("//img").GetAttributeValue("src", string.Empty);
                        car.Model = item.SelectSingleNode("//div[@class='common-text size-16-20 titleS fw-bold mb-4']").InnerText;
                        car.Price = decimal.Parse(item.SelectSingleNode("//span[@class='common-text titleM c-green']").InnerText.Replace("&nbsp", "").Replace(" $", ""));

                        cars.Add(car);

                        if(!string.IsNullOrEmpty(car.FileName))
                        {
                            string file = car.FileName.Split('/').Last();
                            DownLoadFile(car.FileName, $"image/{file}");
                        }
                    }
                    
                }
                catch (Exception)
                {
                    throw;
                }
            }

            foreach (var item in cars)
            {
                Console.WriteLine(item);
            }

        }
    }
}
