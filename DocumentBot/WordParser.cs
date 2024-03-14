// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved. 
// SPDX-License-Identifier:  Apache-2.0

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;


public class WordParser
{
	public static string ParseWordFile(string filePath)
	{
        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filePath, true))
        {
            Body body = wordDoc.MainDocumentPart.Document.Body;
            StringBuilder contents = new StringBuilder();

            foreach (Paragraph co in
                        wordDoc.MainDocumentPart.Document.Body.Descendants<Paragraph>())
            {
                String innertext = co.InnerText;
                if (innertext != null)
                {
                    contents.AppendLine(innertext);
                }
            }

            Debug.WriteLine(contents);
            return contents.ToString();
        }
    }
}
