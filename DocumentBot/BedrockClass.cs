// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved. 
// SPDX-License-Identifier:  Apache-2.0

using System.Text.Json.Nodes;
using Amazon;
using Amazon.BedrockRuntime;
using Amazon.BedrockRuntime.Model;
using Amazon.Util;
using AmazonBedrockWindowsApp;

namespace BedrockRuntimeActions
{
    public static class BedrockClass
    {
       /// <summary>
        /// Asynchronously invokes the Anthropic Claude 2 model to run an inference based on the provided input
        /// </summary>
        /// <param name="prompt">The prompt that you want Claude to complete.</param>
        /// <returns>The inference response from the model</returns>
        /// <remarks>
        /// The different model providers have individual request and response formats.
        /// For the format, ranges, and default values for Anthropic Claude, refer to:
        ///     https://docs.aws.amazon.com/bedrock/latest/userguide/model-parameters-claude.html
        /// </remarks>
        public static async Task<string> InvokeClaudeAsync(string prompt, AppSettings settings)
        {
            //Setting the model id to call in Amazon Bedrock
            string claudeModelId = "anthropic.claude-v2";
            //Creating Amazon Bedrock client object, you can optionally pass the region, 
            //you have to make sure if the service is available in your region
            AmazonBedrockRuntimeClient client = new(RegionEndpoint.USEast1);

            //preparing the inference parameters
            string payload = new JsonObject()
            {
                { "prompt", prompt },
                { "max_tokens_to_sample",settings.MaxTokens },
                { "temperature", settings.Temperature  },
                { "top_p", settings.TopP  },
                { "top_k",settings.TopK   },
                { "stop_sequences", new JsonArray(settings.StopSequence) }
            }.ToJsonString();

            string generatedText = "";
            try
            {
                //Invoking the model 
                InvokeModelResponse response = await client.InvokeModelAsync(new InvokeModelRequest()
                {
                    ModelId = claudeModelId,
                    Body = AWSSDKUtils.GenerateMemoryStreamFromString(payload),
                    ContentType = "application/json",
                    Accept = "application/json"
                });

                //Check if the http code is returned ok
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Parse the response 
                    return JsonNode.ParseAsync(response.Body).Result?["completion"]?.GetValue<string>() ?? "";
                }
                else
                {
                    Console.WriteLine("InvokeModelAsync failed with status code " + response.HttpStatusCode);
                }
            }
            catch (AmazonBedrockRuntimeException e)
            {
                Console.WriteLine(e.Message);
            }
            return generatedText;
        }

        /// <summary>
        /// Asynchronously invokes the AI21 Labs Jurassic-2 model to run an inference based on the provided input.
        /// </summary>
        /// <param name="prompt">The prompt that you want Claude to complete.</param>
        /// <returns>The inference response from the model</returns>
        /// <remarks>
        /// The different model providers have individual request and response formats.
        /// For the format, ranges, and default values for AI21 Labs Jurassic-2, refer to:
        ///     https://docs.aws.amazon.com/bedrock/latest/userguide/model-parameters-jurassic2.html
        /// </remarks>
        public static async Task<string> InvokeJurassic2Async(string prompt)
        {
            string jurassic2ModelId = "ai21.j2-mid-v1";

            AmazonBedrockRuntimeClient client = new(RegionEndpoint.USEast1);

            string payload = new JsonObject()
            {
                { "prompt", prompt },
                { "maxTokens", 200 },
                { "temperature", 0.5 }
            }.ToJsonString();

            string generatedText = "";
            try
            {
                InvokeModelResponse response = await client.InvokeModelAsync(new InvokeModelRequest()
                {
                    ModelId = jurassic2ModelId,
                    Body = AWSSDKUtils.GenerateMemoryStreamFromString(payload),
                    ContentType = "application/json",
                    Accept = "application/json"
                });

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return JsonNode.ParseAsync(response.Body)
                        .Result?["completions"]?
                        .AsArray()[0]?["data"]?
                        .AsObject()["text"]?.GetValue<string>() ?? "";
                }
                else
                {
                    Console.WriteLine("InvokeModelAsync failed with status code " + response.HttpStatusCode);
                }
            }
            catch (AmazonBedrockRuntimeException e)
            {
                Console.WriteLine(e.Message);
            }
            return generatedText;
        }

        // <summary>
        /// Asynchronously invokes the Meta Llama 2 Chat model to run an inference based on the provided input.
        /// </summary>
        /// <param name="prompt">The prompt that you want Llama 2 to complete.</param>
        /// <returns>The inference response from the model</returns>
        /// <remarks>
        /// The different model providers have individual request and response formats.
        /// For the format, ranges, and default values for Meta Llama 2 Chat, refer to:
        ///     https://docs.aws.amazon.com/bedrock/latest/userguide/model-parameters-meta.html
        /// </remarks>
        public static async Task<string> InvokeLlama2Async(string prompt)
        {
            string llama2ModelId = "meta.llama2-13b-chat-v1";

            AmazonBedrockRuntimeClient client = new(RegionEndpoint.USEast1);

            string payload = new JsonObject()
            {
                { "prompt", prompt },
                { "max_gen_len", 512 },
                { "temperature", 0.5 },
                { "top_p", 0.9 }
            }.ToJsonString();

            string generatedText = "";
            try
            {
                InvokeModelResponse response = await client.InvokeModelAsync(new InvokeModelRequest()
                {
                    ModelId = llama2ModelId,
                    Body = AWSSDKUtils.GenerateMemoryStreamFromString(payload),
                    ContentType = "application/json",
                    Accept = "application/json"
                });

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return JsonNode.ParseAsync(response.Body)
                        .Result?["generation"]?.GetValue<string>() ?? "";
                }
                else
                {
                    Console.WriteLine("InvokeModelAsync failed with status code " + response.HttpStatusCode);
                }
            }
            catch (AmazonBedrockRuntimeException e)
            {
                Console.WriteLine(e.Message);
            }
            return generatedText;
        }

        
    }
}