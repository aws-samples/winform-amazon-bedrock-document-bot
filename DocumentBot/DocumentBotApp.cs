// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved. 
// SPDX-License-Identifier:  Apache-2.0

using BedrockRuntimeActions;
using Amazon.Textract;
using System.Text;
using System.Windows.Forms;

namespace AmazonBedrockWindowsApp
{
    public partial class DocumentBotApp : Form
    {
        AppSettings appSettings = new AppSettings();
        public DocumentBotApp()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Update the status of the execution on the label on the form
        /// </summary>
        /// <param></param>
        /// <returns>void</returns>
        /// <remarks>
        /// </remarks>
        private void UpdateStatus(string status)
        {
            lblStatus.Text = status;
        }

        /// <summary>
        /// Build the prompt from the content of the document, conversation history and the follow up question from the user
        /// </summary>
        /// <param></param>
        /// <returns>String</returns>
        /// <remarks>
        /// </remarks>
        private string BuildPrompt()
        {
            StringBuilder promptBuilder = new StringBuilder();
            //Persona Settings - Instructions on how the model should respond based on the persona definition and also provide any additional
            //instruction in explaning the rest of the prompt
            promptBuilder.Append("You are a helpful assistant. The following text includes the original content of a document,");
            promptBuilder.Append("the conversation history between the human and you so far and a followup question which you must answer ");
            promptBuilder.Append("with the most acurate response possible.");
            promptBuilder.Append("You are allowed to say 'I do not know' if you dont have the answer.");
            promptBuilder.Append(Environment.NewLine);

            //Context - The content of the document
            //Anthropic model generate more accurate responses, when context is passed in XML Structure
            promptBuilder.Append("<Document>" + txtDocumentContent.Text + "</Document>");
            promptBuilder.Append(Environment.NewLine);

            if (txtQuestion.Text != string.Empty)
            {
                //Input Data - Converstaion History
                promptBuilder.Append("<ConversationHistory>" + rtxtResponse.Text + "</ConversationHistory>");
                promptBuilder.Append(Environment.NewLine);

                //User Question - The question entered by the user. Anthropic requires the prompt to be asked in this fromat "Human: "
                promptBuilder.Append("\n\nHuman:" + txtQuestion.Text);

                //Output Indicator - Showing the model to provide response after "Assistant:"
                promptBuilder.Append("\n\nAssistant:");

            }
            else
            {
                promptBuilder.Append("\n\nHuman:Summarize the document");
                promptBuilder.Append("\n\nAssistant:");
            }
            return promptBuilder.ToString();
        }

        /// <summary>
        /// Update the content in the conversation textbox on the ui. This method combines the result returned from Amazon Bedrock and
        /// format that into a readable conversation
        /// </summary>
        /// <param name="question">The question the user entered.</param>
        /// <param name="response">The response received from Amzon Bedrock.</param>
        /// <returns>String</returns>
        /// <remarks>
        /// The selection start will have the textbox scroll to the end of the content. 
        /// </remarks>
        private void UpdateResponse(string question, string response)
        {
            rtxtResponse.Text = rtxtResponse.Text + Environment.NewLine + "Human: " + question + "" + Environment.NewLine;
            rtxtResponse.Text = rtxtResponse.Text + "------------------------------------------------------------------------------------" + Environment.NewLine;
            rtxtResponse.Text = rtxtResponse.Text + "Bot's response: " + Environment.NewLine + response;
            rtxtResponse.Text = rtxtResponse.Text + Environment.NewLine + "*================================end of transmission================================*";
            rtxtResponse.SelectionStart = rtxtResponse.Text.Length;
            rtxtResponse.ScrollToCaret();

        }

        /// <summary>
        /// Send the question to Amazon Bedrock and udpate the status of the execution 
        /// </summary>
        /// <param ></param>
        /// <returns>void</returns>
        /// <remarks>
        /// by default the forms submit attribute is set to this button. so once the user press the return key, 
        /// this method will be called. 
        /// </remarks>
        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (txtQuestion.Text == string.Empty) { MessageBox.Show("Enter a question to send to the bot"); return; }

            UpdateStatus("Contacting Amazon Bedrock ....");

            btnSend.Enabled = false;
            btnUpload.Enabled = false;

            string response = await BedrockClass.InvokeClaudeAsync(BuildPrompt(), appSettings);

            UpdateStatus("Response received....");

            UpdateResponse(txtQuestion.Text, response);

            txtQuestion.Text = string.Empty;
            btnSend.Enabled = true;
            btnUpload.Enabled = true;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //you can comment the below lines if you do not want the settings to be shown in the property grid
            prpGrd.SelectedObject = appSettings;
            txtDocumentContent.Text = "This is a conversational chat with the Document bot, where you can first upload a pdf or word document and ask questions related to the document. Upon uploading first the content of the document is extracted using respective services and then passed on to Amazon Bedrock for deriving insight!" + Environment.NewLine + "Have Fun!";

        }

        /// <summary>
        /// Select a file - pdf or word doc from the local drive, extract the content using either Amazon Textract or OpenXML for word.
        /// Pass the extracted content to Amazon Bedrock for summarization. 
        /// </summary>
        /// <param></param>
        /// <returns>none</returns>
        /// <remarks>
        /// The hyperparameters for the model is initialized in the AppSettings class which can be changed. 
        /// </remarks>
        private async void btnUpload_Click(object sender, EventArgs e)
        {

            try
            {
                txtDocumentContent.Text = string.Empty;
                txtQuestion.Text = string.Empty;
                rtxtResponse.Text = string.Empty;
                UpdateStatus(string.Empty);

                // open a dialog box asking the user to select a word or pdf file
                OpenFileDialog openFileDialog = new OpenFileDialog();
                //uncomment if you would like to use pdf documents only and comment the below lines
                //openFileDialog.Filter = "PDF Files|*.pdf";
                //openFileDialog.Title = "Select a PDF File";
                openFileDialog.Filter = "Word Documents|*.docx|PDF Files|*.pdf";
                openFileDialog.Title = "Select a Word or PDF File";
                DialogResult result = openFileDialog.ShowDialog();




                if (result == DialogResult.OK)
                {

                    btnUpload.Enabled = false;

                    string filePath = openFileDialog.FileName;

                    //check if the selected document is a word document
                    if (".doc .docx".Contains(Path.GetExtension(openFileDialog.FileName).ToString()))
                    {
                        UpdateStatus("Extracting content from Word Document ....");
                        txtDocumentContent.Text = WordParser.ParseWordFile(filePath);
                    }
                    else
                    {
                        UpdateStatus("Contacting Amazon Textract for extracting the content ....");
                        //Using Textract async function, for uploading the document to S3 first. This
                        //method will also be useful when the PDF is complex and image type.
                        string responseString = await TextractHelper.StartDetectTextAsync(appSettings.S3BucketName, filePath);
                        txtDocumentContent.Text = responseString;
                    }

                    UpdateStatus("Content successfully extracted ....");

                    string Prompt = BuildPrompt();

                    UpdateStatus("Contacting Amazon Bedrock ....");

                    string response = await BedrockClass.InvokeClaudeAsync(Prompt, appSettings);
                    UpdateStatus("Response received....");

                    if (response == "")
                    {
                        MessageBox.Show("Empty response received. You may need to enable the Anthropic Claude model in Bedrock");
                    }
                    else
                    {
                        UpdateResponse("Summarize the document", response);
                    }


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnUpload.Enabled = true;
            }
        }
    }

}
