using GW_Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using YandexMoneyHttpClient;
using YandexMoneyHttpClient.Models;
using YandexMoneyHttpClient.Models.Requests;
using NPOI.XSSF.UserModel;
using Org.BouncyCastle.Asn1.Pkcs;
using System.IO;

namespace GW_YandexUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var certificatePath = args[0];
                var password = args[1];
                var uriStr = args[2];
                var agentId = args[3];
                var command = args[4];
                var tablePath = args[5];
                string requestId = "";
                if (args.Length > 6)
                    requestId = args[6];
                var xmlSerializer = new GenericXmlSerializer();
                var pemPacker = new PemPacker();


                var certificate = new X509Certificate2(certificatePath, password);
                var pkcs7Coder = new PKCS7Coder(certificate);

                var uri = new Uri(uriStr);
                var handler = new WebRequestHandler();
                handler.ClientCertificates.Add(certificate);

                using (var client = new YandexMoneyClient(xmlSerializer, pkcs7Coder, pemPacker, uri, handler))
                {
                    //var response = client.TestDepositionRequest(new TestDepositionRequest()
                    //{
                    //    AgentId = 203598,
                    //    DstAccount = "410019806982007",
                    //    Amount = "10.00",
                    //    ClientOrderId = Guid.NewGuid().ToString(),
                    //    DepositionPointId = "29",
                    //    Currency = "643",
                    //    Contract = "Зачисление на кошелек",
                    //    RequestDT = DateTime.Now
                    //});
                    //Console.WriteLine(response.Error);
                    //Console.WriteLine(response.Identification);
                    //Console.WriteLine(response.Status);
                    //Console.WriteLine(response.TechMessage);
                    //Console.ReadLine();

                    if (command == "check")
                    {
                        var checkDepositionPointRequest = new CheckDepositionPointsRequest()
                        {
                            AgentId = long.Parse(agentId),
                            RequestId = requestId
                        };
                        var result = client.CheckDepositionPointsRequest(checkDepositionPointRequest);
                        Console.WriteLine(result.Status);
                        Console.WriteLine(result.RequestId);
                        if (result.Error != null)
                        {
                            Console.WriteLine(result.Error.Code);
                            Console.WriteLine(result.Error.PointId);
                        }
                        Console.WriteLine();
                        foreach (var item in result.Items)
                        {
                            Console.WriteLine($"{item.Id} {item.Reason} {item.Status}");
                        }
                    }
                    else if (command == "add")
                    {
                        var parser = new AddressParser();
                        var wb = new XSSFWorkbook(tablePath);
                        var sheet = wb.GetSheetAt(0);

                        var addDepositionPointRequest = new AddDepositionPointsRequest()
                        {
                            AgentId = long.Parse(agentId),
                            RequestId = Guid.NewGuid().ToString(),
                            Points = new DepositionPoint[134]
                        };

                        for (int i = 0; i != 134; i++)
                        {
                            var row = sheet.GetRow(i + 1);
                            if (row != null)
                            {
                                var depositionPoint = new DepositionPoint()
                                {
                                    Id = (int)row.GetCell(1).NumericCellValue,
                                    Type = "atm",
                                    Address = parser.Parse(row.GetCell(6).StringCellValue),
                                    Subagent = false
                                };
                                addDepositionPointRequest.Points[i] = depositionPoint;
                            }
                        }

                        File.WriteAllText(@"C:\Users\it014\Desktop\request.txt", UTF8Encoding.UTF8.GetString(xmlSerializer.Serialize<AddDepositionPointsRequest>(addDepositionPointRequest)));
                        Console.ReadLine();
                        var result = client.AddDepositionPointsRequest(addDepositionPointRequest);
                        File.AppendAllText("log.txt", result.RequestId + "\r\n");
                        Console.WriteLine(result.RequestId);
                        Console.WriteLine(result.Status);
                        if (result.Error != null)
                        {
                            Console.WriteLine(result.Error.Code);
                            Console.WriteLine(result.Error.PointId);
                        }
                    }
                    else if (command == "balance")
                    {
                        var balnceRequest = new BalanceRequest()
                        {
                            AgentId = long.Parse(agentId),
                            ClientOrderId = Guid.NewGuid().ToString(),
                            RequestDT = DateTime.Now
                        };
                        var result = client.BalanceRequest(balnceRequest);
                        Console.WriteLine(result.Balance);
                        Console.WriteLine(result.ClientOrderId);
                        Console.WriteLine(result.ProcessedDt);
                        Console.WriteLine(result.Status);
                        Console.WriteLine(result.Error);
                    }
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().FullName);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.HResult);
                Console.ReadLine();
            }
        }
    }

    public class AddressParser
    {
        public DepositionPointAddress Parse(string str)
        {
            var split = str.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var address = new DepositionPointAddress();

            if(split[2].Contains("город"))
            {
                var city = split[2].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                address.RegionType = city[0];
                var s = "";
                for (var i = 1; i != city.Length; i++)
                {
                    s += city[i];
                    s += " ";
                }
                s = s.TrimEnd();
                address.Region = s;
            }

            if (split[3].Contains("проспект") || split[3].Contains("улица") || split[3].Contains("переулок") || split[3].Contains("проезд"))
            {
                var street = split[3].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                address.StreetType = street[0];
                var s = "";
                for (var i = 1; i != street.Length; i++)
                {
                    s += street[i];
                    s += " ";
                }
                s = s.TrimEnd();
                address.Street = s;
            }

            if (split[4].Contains("корпус") || split[4].Contains("строение") || split[4].Contains("дом")|| split[4].Contains("здание")|| split[4].Contains("сооружение"))
            {
                var house = split[4].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                address.HouseType = house[0];
                address.House = house[1].ToUpper();
                ////string num = "";
                ////string korp = "";
                //foreach(var chr in house[1])
                //{
                //    if (char.IsDigit(chr))
                //        num += chr;
                //    else
                //        korp += chr;
                //}
                //address.House = num;
                //address.Building = korp.ToUpper();

                if (split[4].Contains("корпус"))
                {
                    address.BuildingType = house[2];
                    address.Building = house[3];
                }
            }

            return address;
        }

    }
}
