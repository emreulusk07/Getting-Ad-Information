using DocumentShare.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DocumentShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Cors;
using System.IO.Compression;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.StaticFiles;
using System.Text;
using System.Net.Http;
using System.Net;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System.Collections;

namespace GettingAdInformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class FilesController : ControllerBase
    {

        public FilesController()
        {
        }

        [HttpGet]
        public async Task<ActionResult> GetFiles()
        {
            try
            {
                var average = 0;
                var totalPrice = 0;
                var counter = 0;
                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load("https://www.sahibinden.com");
                IList<HtmlNode> nodes = document.QuerySelectorAll("div.uiBox").QuerySelectorAll("ul.vitrin-list li");
                ArrayList productInformations = new ArrayList();

                var data = nodes.Select((node) =>
                {
                    var price = "";
                    var name = node.QuerySelector("a").InnerText; // ürünün ismi alınır
                    var product = node.QuerySelector("a").GetAttributeValue("href", "/"); // ürünün detay sayfası linki alınır
                    var detailUrl = "https://www.sahibinden.com" + product;
                    HtmlDocument documentDetail = web.Load(detailUrl);
                    IList<HtmlNode> nodes2 = documentDetail.QuerySelectorAll("div.classifiedDetail").QuerySelectorAll("div.classifiedDetailContent").QuerySelectorAll("div.classifiedInfo");
                    foreach (var doc in nodes2)
                    {
                        price = doc.QuerySelector("h3").InnerText; // ürünün fiyatı alınır
                    }

                    //totalPrice += Convert.ToInt32(price);
                    counter++;
                    productInformations.Add(name.Replace("\n                        ", "").Replace("\n                    ", ""));
                    productInformations.Add(price.Replace("\n                        ", "").Replace(" ", "").Replace("\n", ""));

                    string[] productInfo = {
                        name.Replace("\n                        ", "").Replace("\n                    ", ""),
                        price.Replace("\n                        ", "").Replace(" ", "").Replace("\n", "")
                    };

                    return new
                    {
                        name = name.Replace("\n                        ", "").Replace("\n                    ", ""),
                        price = price.Replace("\n                        ", "").Replace(" ", "").Replace("\n", "")
                    };
                });

                // ürünün bilgileri dosyaya aktarılır
                string filePath = "D:\\productInfo.txt";
                if (System.IO.File.Exists(filePath))
                {
                    // Dosya var
                    foreach (var item in productInformations)
                    {
                        System.IO.File.WriteAllLines(filePath, productInformations.Cast<string>().ToArray());
                    }
                }
                else
                {
                    // Dosya yok
                    Console.WriteLine("Please check the file path.");
                }

                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
