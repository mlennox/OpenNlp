﻿using System;
using System.IO;
using OpenNLP.Tools.Tokenize;
using SharpEntropy;
using SharpEntropy.IO;

namespace DataFileConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory() + "/../../";
            var gisModelFilePath = currentDirectory + "../Resources/Models/EnglishSD.nbin";

            var outputFilePath = currentDirectory + "Output/" + Path.GetFileNameWithoutExtension(gisModelFilePath) + ".txt";

            ConvertBinaryGisFileToPlainText(gisModelFilePath, outputFilePath);

            Console.WriteLine("Plain text written at {0}", outputFilePath);
            Console.ReadKey();
        }

        private static void ConvertBinaryGisFileToPlainText(string inputFilePath, string outputFilePath)
        {
            var reader = new BinaryGisModelReader(inputFilePath);
            var model = new GisModel(reader);

            var writer = new PlainTextGisModelWriter();
            writer.Persist(model, outputFilePath);
        }
    }
}
