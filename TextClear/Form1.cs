using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using net.zemberek.erisim;
using net.zemberek.tr.yapi;
namespace TextClear
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Zemberek zemberek = new Zemberek(new TurkiyeTurkcesi());
        private void Form1_Load(object sender, EventArgs e)
        {
            string path = "";
            string writePath = "";

            List<List<List<Word>>> univercityPagewordList = new List<List<List<Word>>>();
            List<List<Word>> univercityListWordList = new List<List<Word>>();
            List<List<string>> files = new List<List<string>>();

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            string[] filePaths;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog.SelectedPath;

                string[] directoryPaths = Directory.GetDirectories(path);
                for (int i = 0; i < directoryPaths.Length; i++)
                {
                    filePaths = Directory.GetFiles(directoryPaths[i]);
                    filePaths = filePaths.OrderBy(x => int.Parse(Path.GetFileNameWithoutExtension(x))).ToArray<string>();
                    files.Add(new List<string>(filePaths));
                }

                for (int i = 0; i < directoryPaths.Length; i++)
                {
                    string tempPath = writePath + @"\" + Path.GetFileName(directoryPaths[i]);
                    Directory.CreateDirectory(tempPath);
                    List<List<Word>> univercityPageList = new List<List<Word>>();
                    List<Word> univercityWordList = new List<Word>();

                    for (int j = 0; j < files[i].Count; j++)
                    {

                        List<Word> wordList = new List<Word>();
                        string file = File.ReadAllText(files[i][j]);
                        file = CharacterRemove(file);   //  gereksiz karakter temizleme
                        List<string> words = TextSplit(file);    // kelimeleri ayırma
                        FindStem(words);    //kök bulma 
                        CleanStopWords(words);  // stop words temizliği
                        Word word;
                        for (int k = 0; k < words.Count; k++)   //words listesindeki aynı kelimeleri kelime ve sayısı olarak tutar.
                        {
                            if (wordList.FindAll(name =>
                            {
                                if (name.Name == words[k])
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }).Count == 0)
                            {
                                word = new Word();
                                word.Name = words[k];
                                word.Count = words.FindAll(name =>
                                {
                                    if (name == words[k])
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }).Count;
                                wordList.Add(word);
                            }

                        }

                        wordList.Sort();
                        univercityWordList.AddRange(wordList);

                        //tf
                        TermFrequency(wordList);
                        univercityPageList.Add(wordList);

                    }

                    univercityListWordList.Add(univercityWordList);

                    //idf
                    InverseDocumentFrequency(univercityPageList);
                    univercityPagewordList.Add(univercityPageList);

                }

                //Üniversiteler arası kelime name count tf-idf
                writePath = Directory.GetParent(path).FullName + @"\CleanWebSites";
                Directory.CreateDirectory(writePath);

                //  Dosyaya yazırma aşaması
                for (int i = 0; i < univercityPagewordList.Count; i++)
                {
                    string tempPath = writePath + @"\" + Path.GetFileName(directoryPaths[i]);
                    Directory.CreateDirectory(tempPath);
                    for (int j = 0; j < univercityPagewordList[i].Count; j++)
                    {
                        string tempFilePath = tempPath + @"\" + Path.GetFileName(files[i][j]);
                        //  string tempFilePath = tempPath + @"\" + j + ".txt";
                        StreamWriter sw = new StreamWriter(tempFilePath);

                        for (int k = 0; k < univercityPagewordList[i][j].Count; k++)
                        {
                            sw.WriteLine(univercityPagewordList[i][j][k].Name + ";" + univercityPagewordList[i][j][k].Count + ";" + univercityPagewordList[i][j][k].Tf.ToString("F7") + ";" + univercityPagewordList[i][j][k].TfIdf.ToString("F7") + ";" + univercityPagewordList[i][j][k].NextIdf.ToString("F7"));
                        }
                        sw.Close();
                    }
                }

                for (int i = 0; i < univercityListWordList.Count; i++)
                {
                    for (int j = 0; j < univercityListWordList[i].Count; j++)
                    {
                        Word word = univercityListWordList[i][j];
                        Word tempWord = new Word(word.Name);
                        List<Word> wordList = univercityListWordList[i].FindAll(temp =>
                        {
                            if (temp.Name == word.Name)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }

                        });
                        for (int k = 0; k < wordList.Count; k++)
                        {
                            tempWord.Count += wordList[k].Count;
                        }
                        univercityListWordList[i].RemoveAll(temp =>
                        {
                            if (temp.Name == tempWord.Name)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        });
                        univercityListWordList[i].Add(tempWord);

                    }
                    univercityListWordList[i].Sort();
                    TermFrequency(univercityListWordList[i]);
                }
                InverseDocumentFrequency(univercityListWordList);


                //  Dosyaya yazırma aşaması
                writePath = Directory.GetParent(path).FullName + @"\UnivercityTfIdf";
                Directory.CreateDirectory(writePath);
                for (int i = 0; i < univercityListWordList.Count; i++)
                {
                    string tempFilePath = writePath + @"\" + Path.GetFileName(directoryPaths[i]) + ".txt";
                    StreamWriter sw = new StreamWriter(tempFilePath);
                    for (int j = 0; j < univercityListWordList[i].Count; j++)
                    {
                        Word word = univercityListWordList[i][j];
                        sw.WriteLine(word.Name + ";" + word.Count + ";" + word.TfIdf.ToString("F7"));
                    }
                    sw.Close();
                }


            }
        }
        private void TermFrequency(List<Word> wordlist)
        {

            for (int i = 0; i < wordlist.Count; i++)
            {
                if (i != wordlist.Count - 1)
                {
                    /*
                    wordlist[i].TfIdf = ((0.5f * (double)wordlist[i].Count) / wordlist[wordlist.Count - 1].Count) + 0.5f;
                    wordlist[i].Tf = ((0.5f * (double)wordlist[i].Count) / wordlist[wordlist.Count - 1].Count) + 0.5f;
                    */
                    wordlist[i].TfIdf = (((double)wordlist[i].Count) / wordlist[wordlist.Count - 1].Count);
                    wordlist[i].Tf = (((double)wordlist[i].Count) / wordlist[wordlist.Count - 1].Count);
                }
                else
                {
                    if (wordlist.Count == 1)
                    {
                        /*
                        wordlist[i].TfIdf = ((0.5f * (double)wordlist[i].Count)) + 0.5f;
                        wordlist[i].Tf = ((0.5f * (double)wordlist[i].Count)) + 0.5f;
                        */
                        /*
                        wordlist[i].TfIdf = (((double)wordlist[i].Count));
                        wordlist[i].Tf = (((double)wordlist[i].Count));
                        */
                        wordlist[i].TfIdf = 1;
                        wordlist[i].Tf = 1;

                    }
                    else
                    {
                        /*
                        wordlist[i].TfIdf = ((0.5f * (double)wordlist[i].Count) / wordlist[wordlist.Count - 2].Count) + 0.5f;
                        wordlist[i].Tf = ((0.5f * (double)wordlist[i].Count) / wordlist[wordlist.Count - 2].Count) + 0.5f;
                        */
                        wordlist[i].TfIdf = (((double)wordlist[i].Count) / wordlist[wordlist.Count - 2].Count);
                        wordlist[i].Tf = (((double)wordlist[i].Count) / wordlist[wordlist.Count - 2].Count);
                    }

                }

            }
        }
        private void InverseDocumentFrequency(List<List<Word>> univercityPageList)
        {
            int count = 0;

            for (int i = 0; i < univercityPageList.Count; i++)
            {
                for (int j = 0; j < univercityPageList[i].Count; j++)
                {
                    string findName = univercityPageList[i][j].Name;
                    for (int k = 0; k < univercityPageList.Count; k++)
                    {

                        Word word = univercityPageList[k].Find(temp => { if (temp.Name == findName) return true; else return false; });
                        if (!(word is null))
                        {
                            count++;

                        }
                    }

                    double idf = Math.Log10(((double)univercityPageList.Count + 1) / count);
                    double NextIdf = Math.Log10(((double)univercityPageList.Count + 1) / (count + 1));
                    univercityPageList[i][j].TfIdf *= idf;
                    univercityPageList[i][j].NextIdf = NextIdf;
                    count = 0;

                }

            }
        }
        private string CharacterRemove(string text)
        {
            List<string> characters = new List<string>(new string[] { ".", ",", "!", "?", "/", "+", "-", "*", "\n", ":", ")", "(", ";" });
            for (int i = 0; i < 9; i++)
            {
                characters.Add(i.ToString());
            }
            for (int i = 0; i < characters.Count; i++)
            {
                text = text.Replace(characters[i], " ");
            }
            text = text.ToLower();  //Küçük harf yapıldı...
            return text;
        }
        private string AdjacentWords(string text)
        {
            for (int i = 0; i < text.Length - 1; i++)
            {
                if (char.IsLower(text[i]) && char.IsUpper(text[i + 1]))
                {
                    text = text.Insert(i, " ");
                }
            }
            return text;
        }
        private List<string> TextSplit(string text)
        {
            List<string> words = new List<string>(text.Split(' '));
            return words;
        }
        private void FindStem(List<string> words)
        {
            List<string> tempStem = new List<string>();

            for (int i = 0; i < words.Count; i++)
            {

                if (zemberek.kelimeDenetle(words[i]))
                {

                    words[i] = zemberek.kelimeCozumle(words[i])[0].kok().icerik();
                }
                else
                {
                    words.RemoveAt(i);
                    i--;
                }
            }


        }
        private void CleanStopWords(List<string> words)
        {
            List<string> stopWords = new List<string>(new string[] { "ve","veya","ama", "ancak", "arada", "bazı", "beri", "bile", "biz", "bu", "şu", "çok"
            ,"daha","da","de","değil","en","gibi","hem","göre","hep","her","ise","ile","kadar","kendi","kez","ki","mu","ne","onlar","ben","sen","pek","şey","tüm","zaten","ya"});
            string temp = "";
            for (int i = 0; i < words.Count; i++)
            {
                if (words[i].Length <= 2 || (temp = stopWords.Find(word => { if (word == words[i]) return true; else return false; })) == words[i])
                {
                    words.RemoveAt(i);
                    i--;
                }
            }

        }

    }
}
