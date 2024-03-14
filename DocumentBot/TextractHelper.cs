//Code courtesy https://gist.github.com/normj/86e4eceffc14c183c6040a5705e3918b
//Norm Johanson

using Amazon.S3.Model;
using Amazon.S3;
using Amazon.Textract.Model;
using Amazon.Textract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Amazon;
using System.Security.Policy;
using System.Linq.Expressions;
using System.Diagnostics;

namespace AmazonBedrockWindowsApp
{
    class TextractHelper
    {
        static StringBuilder builder = new StringBuilder();

        static async Task Main(string[] args)
        {
            try
            {
                await StartDetectTextAsync(string.Empty, string.Empty);
                await DetectTextAsync(string.Empty);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static async Task<String> DetectTextAsync(string filePath)
        {
            using (var textractClient = new AmazonTextractClient(RegionEndpoint.USEast1))
            {
                var bytes = File.ReadAllBytes(filePath);
                builder.Clear();
                var detectResponse = await textractClient.DetectDocumentTextAsync(new DetectDocumentTextRequest
                {
                    Document = new Document
                    {
                        Bytes = new MemoryStream(bytes)
                    }
                });

                foreach (var block in detectResponse.Blocks)
                {
                    BuildBlockString(block.Text);
                    //Console.WriteLine($"Type {block.BlockType}, Text: {block.Text}");
                }
                return builder.ToString();
            }
        }

        public static async Task<String> StartDetectTextAsync(string s3Bucket, string filename)
        {
            // The uploaded file
            var localFile = filename;
            builder.Clear();

            //Creating an object of AmazonTextractClient class
            //you can set your specific region in the constructor
            using (var textractClient = new AmazonTextractClient(RegionEndpoint.USEast1))
            //Creating an object of AmazonS3Client class
            using(var s3Client = new AmazonS3Client(RegionEndpoint.USEast1))
            {
             
                //setting basic parameters for the method call
                var putRequest = new PutObjectRequest
                {
                    BucketName = s3Bucket,
                    FilePath = localFile,
                    Key = Path.GetFileName(localFile)
                };
                try
                {
                    await s3Client.PutObjectAsync(putRequest);
                } catch (Exception ex)
                {
                    Debug.Write(ex.Message);
                }

               //Start document detection job
                var startResponse = await textractClient.StartDocumentTextDetectionAsync(new StartDocumentTextDetectionRequest
                {
                    DocumentLocation = new DocumentLocation
                    {
                        S3Object = new Amazon.Textract.Model.S3Object
                        {
                            Bucket = s3Bucket,
                            Name = putRequest.Key
                        }
                    }
                });

                String JobID = startResponse.JobId;
                //storing the jobid of the request for extracting the text
                var getDetectionRequest = new GetDocumentTextDetectionRequest
                {
                    JobId = startResponse.JobId
                };

                //Poll to detect job completion
                GetDocumentTextDetectionResponse getDetectionResponse = null;
                do
                {
                    //sleep timer to wait until next poll
                    Thread.Sleep(1000);
                    getDetectionResponse = await textractClient.GetDocumentTextDetectionAsync(getDetectionRequest);
                } while (getDetectionResponse.JobStatus == JobStatus.IN_PROGRESS);

                // prepare the response text if the job was successful
                // If the job was successful loop through the pages of results and print the detected text
                if (getDetectionResponse.JobStatus == JobStatus.SUCCEEDED)
                {
                    do
                    {
                        foreach (var block in getDetectionResponse.Blocks)
                        {
                            //build a string from each block
                            BuildBlockString( block.Text);
                        }
                        // Check to see if there are no more pages of data. If no then break.
                        if (string.IsNullOrEmpty(getDetectionResponse.NextToken))
                        {
                            break;
                        }
                        getDetectionRequest.NextToken = getDetectionResponse.NextToken;
                        getDetectionResponse = await textractClient.GetDocumentTextDetectionAsync(getDetectionRequest);

                    } while (!string.IsNullOrEmpty(getDetectionResponse.NextToken));
                }
                else
                {
                    String message = getDetectionResponse.StatusMessage;
                }
            }
            //return extracted text
            return builder.ToString();
        }


        public static void BuildBlockString(String str)
        {
            builder.AppendLine(str);
        }
    }

}
