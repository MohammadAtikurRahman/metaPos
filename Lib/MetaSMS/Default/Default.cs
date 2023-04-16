using System;
using System.IO;
using System.Net;
using System.Text;


namespace MetaSMS.Default 
{
    public class Default  
    {

        public string sendSmsByDefault(DefaultModel model)
        {

            //var url = "http://metasmsbd.com/api/send?senderKey=" + model.senderKey + "&numbers=" + model.phoneNumber + "&text=" + model.message + "&receiverType=" + model.receiverType + "&title=" + model.title;

            //var req = (HttpWebRequest)WebRequest.Create(url);
            //var resp = (HttpWebResponse)req.GetResponse();
            //var sr = new StreamReader(resp.GetResponseStream());
            //return sr.ReadToEnd();


            var url = "http://metasmsbd.com/api/sms/send/";
            // Create a request using a URL that can receive a post.   
            WebRequest request = WebRequest.Create(url);
            // Set the Method property of the request to POST.  
            request.Method = "POST";
            // Create POST data and convert it to a byte array.  
            string postData = "Contacts=" + model.phoneNumber + " &MessageText=" + model.message + "&SenderKey=" + model.senderKey + "&ReceiverType=" + model.receiverType + "&Title=" + model.title;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.  
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;
            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.  
            dataStream.Close();
            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            // Display the content.  
            // Clean up the streams.  
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }




    }
}
