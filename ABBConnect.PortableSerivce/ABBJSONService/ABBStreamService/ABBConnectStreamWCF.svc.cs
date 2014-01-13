using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace ABBStreamService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ABBConnectStreamWCF" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ABBConnectStreamWCF.svc or ABBConnectStreamWCF.svc.cs at the Solution Explorer and start debugging.
    public class ABBConnectStreamWCF : IABBConnectStreamWCF
    {
        public void saveImage(Stream stream)
        {
            byte[] initialBuffer = new byte[200];
            //int bytesRead, totalBytesRead = 0;
            string encodedData = "";
            string firstPackage = "";
            int i = 0;
            string[] words = new string[]{};
            string feedId = "";
            string arrayLength = "";
            byte[] filebytes = new byte[]{};
            byte[] firstImagePackage = new byte[] { };

            stream.Read(initialBuffer, 0, initialBuffer.Length);
            char[] chars = new char[initialBuffer.Length / sizeof(char)];
            System.Buffer.BlockCopy(initialBuffer, 0, chars, 0, chars.Length);
            firstPackage = new string(chars);
            words = firstPackage.Split(';');
            arrayLength = words[0] + ";";
            feedId = words[1] + ";";

            byte[] buffer = new byte[Int32.Parse(words[0])];
            filebytes = new byte[(feedId.Length + arrayLength.Length) * sizeof(char)];
            long pos = stream.Position;
            //stream.Position = 0;
            //stream.Seek(0, SeekOrigin.Begin);

            firstImagePackage = new byte[Int32.Parse(words[0])];
            stream.Read(buffer, 0, buffer.Length);
            System.Buffer.BlockCopy(initialBuffer, filebytes.Length, firstImagePackage, 0, initialBuffer.Length - filebytes.Length);
            System.Buffer.BlockCopy(buffer, 0, firstImagePackage, initialBuffer.Length - filebytes.Length, buffer.Length - (initialBuffer.Length - filebytes.Length));

            encodedData = Convert.ToBase64String(firstImagePackage);

            //do
            //{
            //    bytesRead = stream.Read(buffer, 0, buffer.Length);

            //    if (i == 0)
            //    {
            //        chars = new char[buffer.Length / sizeof(char)];
            //        System.Buffer.BlockCopy(buffer, 0, chars, 0, chars.Length);
            //        firstPackage = new string(chars);
            //        words = firstPackage.Split(';');
            //        feedId = words[0] + ";";
            //        filebytes = new byte[feedId.Length * sizeof(char)];
            //        encodedData = Convert.ToBase64String(buffer, filebytes.Length, buffer.Length - filebytes.Length);
            //        i++;
            //    }
            //    else
            //        encodedData = encodedData + Convert.ToBase64String(buffer);
            //    totalBytesRead += bytesRead;
            //} while (bytesRead > 0);

            //System.Buffer.BlockCopy(feedId.ToCharArray(), 0, filebytes, 0, feedId.Length * sizeof(char));

            //string base64Remove = Convert.ToBase64String(filebytes);
            //base64Remove = base64Remove.Remove(base64Remove.Length - 2);

            string imageFile = "data:image/jpeg;base64," + encodedData;

            int id = Int32.Parse(words[1]);

            using (SqlConnection sqlConn = new SqlConnection("Data Source=www3.idt.mdh.se;" + "Initial Catalog=ABBConnect;" + "User id=rgn09003;" + "Password=ABBconnect1;")) //here goes connStrng or the variable of it
            {

                sqlConn.Open();
                string sqlQuery = "AddImageToFeed";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@feedID", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@image", SqlDbType.NVarChar).Value = imageFile;

                    cmd.ExecuteNonQuery();
                }
                sqlConn.Close();
            }
        }
    }
}
