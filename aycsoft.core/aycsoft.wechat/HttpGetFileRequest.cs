using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using aycsoft.util;

namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：get文件请求
    /// </summary>
    public class HttpGetFileRequest : IHttpSend
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<string> Send(string url, string path)
        {
            var (by, header) = await new HttpHelper().GetFile(url);

            if (header["Content-Type"].Contains("application/json"))
            {
                return Encoding.UTF8.GetString(by);
            }
            else
            {
                Regex regImg = new Regex("\"(?<fileName>.*)\"", RegexOptions.IgnoreCase);

                MatchCollection matches = regImg.Matches(header["Content-disposition"]);

                string fileName = matches[0].Groups["fileName"].Value;

                string filepath = path.TrimEnd('\\') + "\\" + fileName;

                System.IO.Stream so = new System.IO.FileStream(filepath, System.IO.FileMode.Create);

                so.Write(by, 0, by.Length);

                so.Close();
            }

            return header.ToJson();
        }
    }
}
