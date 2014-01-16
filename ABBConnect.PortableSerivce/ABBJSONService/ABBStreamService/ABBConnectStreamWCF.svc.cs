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
        /// <summary>
        /// Method that accepts a stream of a byte array containing the image. At the beginning a small package is received, where the size of the
        /// real image byte array is defined and also the id of the feed in which the image should be placed. Since the methods of WCF, which do contain a Stream 
        /// do not accept more parameters, those values had to be passed in the first package. Moreover, the size of the actual image byte array was needed to request
        /// a package of the exact same size and store the right string in the database.
        /// 
        /// Later the rest package is received and stored in a byte array with size similar to the size defined from the first package. Then the byte array is 
        /// converted to a base64 string and stored in the database.
        /// </summary>
        /// <param name="stream"></param>
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
