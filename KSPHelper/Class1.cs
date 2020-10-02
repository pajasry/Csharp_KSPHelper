using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KSPHelperLibrary
{
    public class KSPHelper
    {
        public List<string[]> Collection = new List<string[]>();
        public string[] firstLine;

        public List<string> Output = new List<string>();

        string _fileDir;
        private string filesDir {
            get {
                return this._fileDir;
            }
            set {
                if (!Directory.Exists(value))
                    throw new Exception($"Adresar {value} neexistuje. Zkontroluj, jestli jsi zadal spravnou cestu");
                _fileDir = value;
            }
        }
        string _fileName;
        private string fileName {
            get {
                return _fileName;
            }
            set {
                if (!File.Exists(filesDir + "\\" + value))
                    throw new Exception($"Soubor {value} neexistuje. Zkontroluj, jestli jsi zadal spravnou cestu a mas nastavenou spravnou priponu");
                _fileName = value.Split('.')[0];
            }
        }

        const string INPUT_SUFFIX = ".in";
        const string OUTPUT_SUFFIX = ".out";
        public string inputFilePath
        {
            get
            {
                return filesDir + "\\" + fileName + INPUT_SUFFIX;
            }
        }
        public string outputFilePath
        {
            get
            {
                return filesDir + "\\" + fileName + OUTPUT_SUFFIX;
            }
        }
        public bool LiveWrite = true;

        public KSPHelper(string dir, string inputFile) {
            filesDir = dir;
            fileName = fileName;
            LoadFromFile();
        }
        public KSPHelper(string path) {
            string dir = path.Replace(Path.GetFileName(path), "");
            filesDir = dir;
            fileName = Path.GetFileName(path);
            LoadFromFile();
        }

       private List<string[]> LoadFromFile() {
            foreach (string line in File.ReadAllLines(this.inputFilePath))
                this.Collection.Add(line.Split(' '));

            //removes first line
            this.firstLine = this.Collection[0];
            this.Collection.RemoveAt(0);
            return this.Collection;
        }

        public void AddOutput(string newLine) {
            this.Output.Add(newLine);
            if (!this.LiveWrite)
                return;

            using (StreamWriter sw = File.AppendText(this.outputFilePath)) {
                sw.WriteLine(newLine);
            }
        }

        public void FlushOutput() {
            using (StreamWriter sw = File.CreateText(this.outputFilePath))
            {
                foreach (string line in this.Output)
                    sw.WriteLine(line);
            }
        }
    }
}
