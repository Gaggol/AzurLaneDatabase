using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AzurLaneDatabase
{
    internal class LoadDB
    {
        string dbFile = Environment.CurrentDirectory + "/Resources/db.txt";
        string saveFile = Environment.CurrentDirectory + "/Resources/saved.txt";

        public List<Character> characters = new List<Character>();

        public LoadDB() {
            Start();
        }

        public void Start(bool forceDBFile = false) {
            string[] lines;
            if(!forceDBFile) {
                if(File.Exists(saveFile)) {
                    lines = File.ReadAllLines(saveFile);
                } else {
                    lines = File.ReadAllLines(dbFile);
                }
            } else {
                lines = File.ReadAllLines(dbFile);
            }
            //public Character(string Name, int rarity, int stars, int level, bool canRetrofit = false, bool isRetrofit = false) {

            try {
                bool versionTwo = false;
                foreach(string line in lines) {
                    if(line[0] == '#') {
                        if(line.Contains("V:2")) { 
                            versionTwo = true; 
                        }
                        continue;
                    }
                    string[] parts = line.Split(',');
                    //Name, Rarity,
                    //Stars, IsOwned,
                    //CanRetro, IsRetro

                    if(versionTwo) {
                        characters.Add(new Character(parts[0], int.Parse(parts[1]),
                                            int.Parse(parts[2]), bool.Parse(parts[3]),
                                            bool.Parse(parts[4]), bool.Parse(parts[5])));
                    } else {
                        characters.Add(new Character(parts[0], int.Parse(parts[1]),
                                            int.Parse(parts[2]), Utility.bParse(int.Parse(parts[3])),
                                            bool.Parse(parts[4]), bool.Parse(parts[5])));
                    }
                }
            } catch(Exception e) {
                MainWindow.Instance.OpenConsole();
                Console.WriteLine(e);
                Start(true);
            }
        }

        public void Save() {
            StringBuilder sb = new StringBuilder();
            sb.Append("#Name, Rarity, Stars, IsOwned, Can be Retrofit, Retrofitted, V:2" + Environment.NewLine);

            foreach(Character c in characters) {
                string a = $"{c.Name},{(int)c.rarity},{c.stars},{c.isOwned},{c.canRetrofit},{c.isRetrofit}{Environment.NewLine}";
                sb.Append(a);
            }

            File.WriteAllText(saveFile, sb.ToString());
            TestNewSave();
            MainWindow.Instance.ChangeTitle(false);
        }

        bool TestNewSave() {
            List<Character> _tempCharacters = new List<Character>();

            string[] lines = File.ReadAllLines(saveFile);

            foreach(string line in lines) {
                if(line[0] == '#') continue;
                string[] parts = line.Split(',');
                
                _tempCharacters.Add(new Character(parts[0], int.Parse(parts[1]),
                                    int.Parse(parts[2]), bool.Parse(parts[3]),
                                    bool.Parse(parts[4]), bool.Parse(parts[5])));
            }

            return true;
        }
    }
}
