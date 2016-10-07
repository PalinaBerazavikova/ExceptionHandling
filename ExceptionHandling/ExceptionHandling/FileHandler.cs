using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling
{
    class FileHandler
    {
        public const string Extension = ".txt";
        public const string Exception = "Exception:";
        private void CheckFileOnExceptions(string path)
        {
            if (!File.Exists(path))
            {
                throw new System.IO.FileNotFoundException($"file {path} doesn't exist");
            }
            if (!Path.GetExtension(path).Equals(Extension))
            {
                throw new Exceptions.WrongExtensionException($"file {path} is not a txt file");
            }
        }

        public string GetPath()
        {
            string path = string.Empty;
            Console.WriteLine("Please enter the path to your txt file:");
            path = Console.ReadLine();
            this.CheckFileOnExceptions(path);
            return path;
        }

        public List<string> FindFirstSymbols(string path)
        {
            string line;
            List<string> lines = new List<string>();
            using (System.IO.StreamReader file = new System.IO.StreamReader(path))
            {
                while ((line = file.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            if (lines.Count == 0)
            {
                throw new Exceptions.EmptyFileException($"empty file {path}");
            }
            List<string> notEmptyLines = new List<string>();
            int lineNumber = 0;
            foreach (string str in lines)
            {
                bool isLineOkay = true;
                lineNumber++;
                try
                {
                    this.CheckEmptyLines(str, lineNumber);
                }
                catch (Exceptions.EmptyStringException e)
                {
                    isLineOkay = false;
                    Console.WriteLine($"{Exception} {e.Message}");
                    Logger.Write(ExceptionLogger.CreateExceptionString(e));
                }
                if (isLineOkay)
                {
                    string firstSymbol = str.Substring(0, 1);
                    if (firstSymbol.Equals(" "))
                    {
                        notEmptyLines.Add("Space symbol");
                    }
                    else
                    {
                        notEmptyLines.Add(firstSymbol);
                    }
                }
            }
            if (notEmptyLines.Count == 0)
            {
                throw new Exceptions.AllLinesAreEmptyException($"All lines in file {path} are empty");
            }
            return notEmptyLines;
        }

        private void CheckEmptyLines(string line, int lineNumber)
        {
            if (line.Equals(""))
            {
                throw new Exceptions.EmptyStringException($"empty line #{lineNumber} was founded");
            }
        }

        public void ShowFirstSymbols()
        {
            bool isOkay;
            string path;
            List<string> notEmptyLines = new List<string>() ;
            do
            {
                isOkay = true;
                try
                {
                    path = this.GetPath();
                }
                catch (FileNotFoundException e)
                {
                    isOkay = false;
                    this.HandleCatchedException(e);
                    continue;
                }
                catch (Exceptions.WrongExtensionException e)
                {
                    isOkay = false;
                    this.HandleCatchedException(e);
                    continue;
                }
                try
                {
                    notEmptyLines = this.FindFirstSymbols(path);
                }
                catch (Exceptions.EmptyFileException e)
                {
                    isOkay = false;
                    this.HandleCatchedException(e);
                    continue;
                }
                catch (Exceptions.AllLinesAreEmptyException e)
                {
                    isOkay = false;
                    this.HandleCatchedException(e);
                    continue;
                }
            } while (!isOkay);

            Console.WriteLine("Fist symbol of lines in file:");
            foreach (string symb in notEmptyLines)
            {
                Console.WriteLine(symb);
            }
        }

        private void HandleCatchedException(Exception e)
        {
            Console.WriteLine($"{Exception} {e.Message}");
            Logger.Write(ExceptionLogger.CreateExceptionString(e));
        }
    }
}
