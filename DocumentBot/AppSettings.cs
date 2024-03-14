// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved. 
// SPDX-License-Identifier:  Apache-2.0
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonBedrockWindowsApp
{
     public class AppSettings
    {
        private float temperature = 0.0f;
        private int topP = 0;
        private int topK = 10;
        private int maxTokens = 500;
        private string stopSequence = "\n\nHuman:";
        private string s3BucketName = "YOURBUCKETNAMEHERE";
        private bool useAsynchronousMethod = true;
        //generate get/set methods for all of the above properties
        
        [CategoryAttribute("Model Hyperparameters"), DescriptionAttribute("Temperature (temperature)– Use a lower value to decrease randomness in the response. value range 0.0 to 1.0.")]
        public float Temperature
        {
            get { return temperature; }

            set { if (value < 0 || value > 1)
                    this.temperature = 0;
                else
                    this.temperature = value; 
            }
        }
        [CategoryAttribute("Model Hyperparameters"), DescriptionAttribute("Top P (topP) – Use a lower value to ignore less probable options. Decimal values from 0 to 1 are accepted.")]
        public int TopP { get => topP; set => topP = value; }
        [CategoryAttribute("Model Hyperparameters"), DescriptionAttribute("Top K (topK) – Specify the number of token choices the model uses to generate the next token.")]
        public int TopK { get => topK; set => topK = value; }
        [CategoryAttribute("Model Hyperparameters"), DescriptionAttribute("Maximum length (max_tokens_to_sample) – Specify the maximum number of tokens to use in the generated response. We recommend a limit of 4,000 tokens for optimal performance.")]
        public int MaxTokens { get => maxTokens; set => maxTokens = value; }
        [CategoryAttribute("Model Hyperparameters"), DescriptionAttribute("Stop sequences (stop_sequences) – Configure up to four sequences that the model recognizes. After a stop sequence, the model stops generating further tokens. The returned text doesn't contain the stop sequence.")]
        public string StopSequence { get => stopSequence; set => stopSequence = value; }
        [CategoryAttribute("Textract Settings"), DescriptionAttribute("The name of the S3 bucket where the document will be uploaded.")]
        public string S3BucketName { get => s3BucketName; set => s3BucketName = value; }

        public AppSettings() { }

    }
}
