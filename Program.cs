using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string inputImageDataFile, kernelDataFile, outputImageDataFile;
        Console.Write("Enter the address of the input image data file: ");
        inputImageDataFile = Console.ReadLine();

        Console.Write("Enter the address of the convolution kernel data file: ");
        kernelDataFile = Console.ReadLine();

        Console.Write("Enter the address of the output image data file: ");
        outputImageDataFile = Console.ReadLine();

        double[,] inputImageData = ReadImageDataFromFile(inputImageDataFile);

        double[,] kernelData = ReadImageDataFromFile(kernelDataFile);

        double[,] outputImageData = ConvolveImage(inputImageData, kernelData);

        WriteImageDataToFile(outputImageDataFile, outputImageData);

        Console.WriteLine("Convolution completed successfully.");
    }

    static double[,] ReadImageDataFromFile(string fileAddress)
    {
        string[] lines = File.ReadAllLines(fileAddress);
        int numRows = lines.Length;
        int numCols = lines[0].Split(' ').Length;
        double[,] imageData = new double[numRows, numCols];

        for (int i = 0; i < numRows; i++)
        {
            string[] rowValues = lines[i].Split(' ');

            for (int j = 0; j < numCols; j++)
            {
                imageData[i, j] = double.Parse(rowValues[j]);
            }
        }

        return imageData;
    }

    static void WriteImageDataToFile(string fileAddress, double[,] imageData)
    {
        int numRows = imageData.GetLength(0);
        int numCols = imageData.GetLength(1);

        using (StreamWriter writer = new StreamWriter(fileAddress))
        {
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    writer.Write(imageData[i, j]);
                    writer.Write(" ");
                }

                writer.WriteLine();
            }
        }
    }

    static double[,] ConvolveImage(double[,] inputImageData, double[,] kernelData)
    {
        int imageHeight = inputImageData.GetLength(0);
        int imageWidth = inputImageData.GetLength(1);
        int kernelSize = kernelData.GetLength(0);
        int padding = kernelSize / 2;

        double[,] outputImageData = new double[imageHeight, imageWidth];

        for (int i = padding; i < imageHeight - padding; i++)
        {
            for (int j = padding; j < imageWidth - padding; j++)
            {
                double sum = 0.0;

                for (int k = -padding; k <= padding; k++)
                {
                    for (int l = -padding; l <= padding; l++)
                    {
                        sum += inputImageData[i + k, j + l] * kernelData[k + padding, l + padding];
                    }
                }

                outputImageData[i, j] = sum;
            }
        }

        return outputImageData;
    }
}
