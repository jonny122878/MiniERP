using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
//using Aspose.Words;
//using Aspose.Words.Replacing;
// Load a Word docx document
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;


namespace MiniERP
{
    public static class ToWordExtension
    {
        static void ReplaceParserTag(this OpenXmlElement elem, Dictionary<string, string> data)
        {
            var pool = new List<Run>();
            var matchText = string.Empty;
            //var hiliteRuns = elem.Descendants<Run>() //找出鮮明提示
            //    .Where(o => o.RunProperties?.Elements<Highlight>().Any() ?? false).ToList();
            var hiliteRuns = elem.Descendants<Run>().ToList();
            foreach (var run in hiliteRuns)
            {
                var t = run.InnerText;
                if (t.StartsWith("["))
                {
                    pool = new List<Run> { run };
                    matchText = t;
                }
                else
                {
                    matchText += t;
                    pool.Add(run);
                }
                if (t.EndsWith("]"))
                {
                    var m = Regex.Match(matchText, @"\[\$(?<n>\w+)\$\]");
                    if (m.Success && data.ContainsKey(m.Groups["n"].Value))
                    {
                        var firstRun = pool.First();
                        firstRun.RemoveAllChildren<Text>();
                        firstRun.RunProperties.RemoveAllChildren<Highlight>();

                        #region test
                        var newText = data[m.Groups["n"].Value];
                        var lists = newText.Split('\n');

                        var Qty = lists.Length;
                        var step = 0;
                        foreach (var line in lists)
                        {
                            step++;
                            firstRun.Append(new Text(line));
                            if(step == Qty)
                            {
                                break;
                            }
                            firstRun.Append(new Break());
                        } 
                        #endregion


                        #region 參考
                        //var newText = data[m.Groups["n"].Value];
                        //var firstLine = true;
                        //foreach (var line in Regex.Split(newText, @"\\n"))
                        //{
                        //    if (firstLine) firstLine = false;
                        //    else firstRun.Append(new Break());
                        //    firstRun.Append(new Text(line));
                        //} 
                        #endregion
                        pool.Skip(1).ToList().ForEach(o => o.Remove());
                    }
                }

            }
        }

        public static byte[] GenerateDocx(this Dictionary<string, string> data ,byte[] template)
        {
            using (var ms = new MemoryStream())
            {
                ms.Write(template, 0, template.Length);
                using (var docx = WordprocessingDocument.Open(ms, true))
                {
                    docx.MainDocumentPart.HeaderParts.ToList().ForEach(hdr =>
                    {
                        hdr.Header.ReplaceParserTag(data);
                    });
                    docx.MainDocumentPart.FooterParts.ToList().ForEach(ftr =>
                    {
                        ftr.Footer.ReplaceParserTag(data);
                    });
                    docx.MainDocumentPart.Document.Body.ReplaceParserTag(data);
                    docx.Save();
                }
                return ms.ToArray();
            }
        }


//        public static void ToWord(this List<KeyValuePair<string,string>> sources,string sourceFile,string destFile)
//        {
//            var docxBytes = WordRender.GenerateDocx(File.ReadAllBytes("AnnounceTemplate.docx"),
//                new Dictionary<string, string>()
//                {
//                    ["Title"] = "澄清黑暗執行緒部落格併購傳聞",
//                    ["SeqNo"] = "2021-FAKE-001",
//                    ["PubDate"] = "2021-04-01",
//                    ["Source"] = "亞太地區公關部",
//                    ["Content"] = @"
//　　坊間媒體盛傳「史塔克工業將以美金 18 億元併購黑暗執行緒部落格」一事，
//本站在此澄清並無此事。\n\n
//　　史塔克公司執行長日前確實曾派遣代表來訪，雙方就技術合作一事交換意見，
//相談甚歡，惟本站暫無出售計劃，且傳聞金額亦不符合本站預估價值(謎之聲：180 元都嫌貴)，
//純屬不實資訊。\n\n  
//　　本站將秉持初衷，持續發揚野人獻曝、敝帚自珍精神，歡迎舊雨新知繼續支持。"
//                });
//            File.WriteAllBytes(
//                Path.Combine(ResultFolder, $"套表測試-{DateTime.Now:HHmmss}.docx"),
//                docxBytes);
//        }
    }
}