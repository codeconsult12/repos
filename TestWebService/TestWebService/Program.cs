using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestWebService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                String fileToUpload = @"C:\Sample.jpg";
                String sharePointSite = "https://sprucetechnology.sharepoint.com/sites/BusinessCentral/";
                String documentLibraryName = "Shared Documents";

                string[] filenameSplits = fileToUpload.Split('\\');
                string filename = filenameSplits[filenameSplits.Length - 1];

                Uri siteUri = new Uri(sharePointSite);
                string realm = TokenHelper.GetRealmFromTargetUrl(siteUri);
                string accessToken = TokenHelper.GetAppOnlyAccessToken(TokenHelper.SharePointPrincipal,
                siteUri.Authority, realm).AccessToken;



                using (var clientContext = TokenHelper.GetClientContextWithAccessToken(sharePointSite.ToString(), accessToken))
                {
                    byte[] bytefile = System.IO.File.ReadAllBytes(fileToUpload);

                    HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(sharePointSite + "/_api/web/GetFolderByServerRelativeUrl('Shared%20Documents')/Files/add(url='" + filename + "',overwrite=true)");
                    endpointRequest.Method = "POST";
                    endpointRequest.Headers.Add("binaryStringRequestBody", "true");
                    endpointRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                    endpointRequest.GetRequestStream().Write(bytefile, 0, bytefile.Length);

                    HttpWebResponse endpointresponse = (HttpWebResponse)endpointRequest.GetResponse();


                    /////////After upload url will be like   https://sprucetechnology.sharepoint.com/sites/BusinessCentral/Shared%20Documents/sample.jpg
                }


            }
            catch
            (Exception ex)
            {

            }













        }
    }
}
