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
namespace SearchEngine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Zemberek zemberek = new Zemberek(new TurkiyeTurkcesi());
        string path = "";
        string pathU = "";
        List<List<List<Word>>> univercityPagewordList = new List<List<List<Word>>>();   //üniversitelerin listesi
        List<List<Word>> univercityWordlist = new List<List<Word>>();
        List<string> uWords = new List<string>();
        List<List<string>> files = new List<List<string>>();
        bool addedSearchFile = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Temizlenmiş üniversite sayfa klasörünü seçin (CleanWeb)");

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)    //Dosya okuma bölümü
            {
                path = folderBrowserDialog.SelectedPath;

                string[] directoryPaths = Directory.GetDirectories(path);
                string[] filePaths;
                for (int i = 0; i < directoryPaths.Length; i++)
                {
                    filePaths = Directory.GetFiles(directoryPaths[i]);
                    filePaths = filePaths.OrderBy(x => int.Parse(Path.GetFileNameWithoutExtension(x))).ToArray<string>();
                    files.Add(new List<string>(filePaths));
                    List<List<Word>> univercityPageList = new List<List<Word>>();   //üniversitelerin sayfa listesi
                    for (int j = 0; j < filePaths.Length; j++)
                    {
                        List<Word> wordList = new List<Word>(); //kelimelerin listesi
                        string[] tempWords = File.ReadAllLines(filePaths[j]);
                        Word word;
                        for (int k = 0; k < tempWords.Length; k++)
                        {
                            word = new Word();
                            string[] wordSplit = tempWords[k].Split(';');
                            word.Name = wordSplit[0];
                            word.Count = int.Parse(wordSplit[1]);
                            word.Tf = double.Parse(wordSplit[2]);
                            word.TfIdf = double.Parse(wordSplit[3]);
                            word.NextIdf = double.Parse(wordSplit[4]);
                            wordList.Add(word);
                        }
                        univercityPageList.Add(wordList);
                    }
                    univercityPagewordList.Add(univercityPageList);

                }

            }


            FolderBrowserDialog folderBrowserDialogU = new FolderBrowserDialog();
            MessageBox.Show("Temizlenmiş üniversiteler klasörünü seçiniz (Univercty)");
            if (folderBrowserDialogU.ShowDialog() == DialogResult.OK)
            {
                pathU = folderBrowserDialogU.SelectedPath;
                string[] filePathsU = Directory.GetFiles(pathU);
                for (int i = 0; i < filePathsU.Length; i++)
                {
                    List<Word> wordListU = new List<Word>(); //kelimelerin listesi
                    string[] tempWords = File.ReadAllLines(filePathsU[i]);
                    Word word;
                    for (int j = 0; j < tempWords.Length; j++)
                    {
                        word = new Word();
                        string[] wordSplit = tempWords[j].Split(';');
                        word.Name = wordSplit[0];
                        word.Count = int.Parse(wordSplit[1]);
                        word.TfIdf = double.Parse(wordSplit[2]);
                        wordListU.Add(word);
                        if (uWords.Find(temp => { if (temp == word.Name) return true; else return false; }) == null)
                        {
                            uWords.Add(word.Name);
                        }

                    }

                    univercityWordlist.Add(wordListU);

                }
                uWords.Sort();
            }

        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (addedSearchFile)    //  Aranan kelime dökümanı Yeni arama yapılırken eskisi siliniyor.
            {
                for (int i = 0; i < univercityPagewordList.Count; i++)
                {
                    univercityPagewordList[i].RemoveAt(univercityPagewordList[i].Count - 1);
                }
                addedSearchFile = false;
            }

            string textSearch = textBox1.Text;
            List<Word> searchWordList = new List<Word>();   //Aranacak kelimeler listesi

            if (textSearch != "")   //input kontrolü
            {
                string[] wordSplit = textSearch.Split(' ');
                Word word;
                for (int i = 0; i < wordSplit.Length; i++)
                {
                    word = new Word();
                    if (zemberek.kelimeDenetle(wordSplit[i]))
                    {
                        word.Name = zemberek.kelimeCozumle(wordSplit[i])[0].icerik().ToString();

                        Word w = searchWordList.Find(tempWord => { if (tempWord.Name == wordSplit[i]) return true; else return false; });
                        if (w is null)
                        {
                            word.Count++;
                            searchWordList.Add(word);
                        }
                        else
                        {

                            w.Count++;
                        }
                    }
                }
                CleanStopWords(searchWordList);
                searchWordList.Sort();
                TermFrequency(searchWordList);  //tf bulunuuyor

                for (int i = 0; i < univercityPagewordList.Count; i++)
                {
                    InverseDocumentFrequency(searchWordList, univercityPagewordList[i]);
                }

                List<Result> cosineResultList = CosineSimilarity(univercityPagewordList);

                string[] linkFiles = Directory.GetFiles(path);
                listBox1.Items.Clear();
                int c;
                if (cosineResultList.Count != 0)
                {
                    if (cosineResultList.Count > 20)
                        c = 20;
                    else
                        c = cosineResultList.Count;
                    for (int i = 0; i < c; i++)
                    {
                        string[] links = File.ReadAllLines(linkFiles[cosineResultList[i].I]);
                        string[] temp = links[cosineResultList[i].J].Split(';');
                        listBox1.Items.Add(temp[0].Replace('"', ' ').Trim());
                    }
                }
                else
                {
                    MessageBox.Show("Sonuç Yok");
                }

                addedSearchFile = true;

            }



        }
        private void TermFrequency(List<Word> wordlist)
        {

            for (int i = 0; i < wordlist.Count; i++)
            {
                if (i != wordlist.Count - 1)
                {
                    //wordlist[i].TfIdf = ((0.5f * (double)wordlist[i].Count) / wordlist[wordlist.Count - 1].Count) + 0.5f;
                    wordlist[i].TfIdf = (((double)wordlist[i].Count) / wordlist[wordlist.Count - 1].Count);
                }
                else
                {
                    if (wordlist.Count == 1)
                    {
                        // wordlist[i].TfIdf = (0.5f * (double)wordlist[i].Count) + 0.5f;
                        // wordlist[i].TfIdf = ((double)wordlist[i].Count);
                        wordlist[i].TfIdf = 1;
                    }
                    else
                    {
                        // wordlist[i].TfIdf = ((0.5f * (double)wordlist[i].Count) / wordlist[wordlist.Count - 2].Count) + 0.5f;
                        wordlist[i].TfIdf = (((double)wordlist[i].Count) / wordlist[wordlist.Count - 2].Count);
                    }

                }

            }
        }

        private void InverseDocumentFrequency(List<Word> searchWordList, List<List<Word>> univercityPageList)
        {
            List<Word> tempSearchWordList = new List<Word>();

            int count = 0;
            for (int i = 0; i < searchWordList.Count; i++)
            {
                Word word = new Word();
                string findName = searchWordList[i].Name;
                for (int j = 0; j < univercityPageList.Count; j++)
                {
                    word = univercityPageList[j].Find(temp => { if (temp.Name == findName) return true; else return false; });
                    if (!(word is null))
                    {
                        break;

                    }
                }
                Word tempWord = new Word();
                if (!(word is null) && word.Name == findName)
                {
                    tempWord.TfIdf = searchWordList[i].TfIdf * word.NextIdf;
                }
                else
                {
                    double idf = Math.Log10(((double)univercityPageList.Count + 1) / (count + 1));
                    tempWord.TfIdf = searchWordList[i].TfIdf * idf;
                }
                tempWord.Name = searchWordList[i].Name;
                tempWord.Count = searchWordList[i].Count;
                tempSearchWordList.Add(tempWord);
                count = 0;
            }
            univercityPageList.Add(tempSearchWordList);

        }

        private List<Result> CosineSimilarity(List<List<List<Word>>> univercityPagewordList)
        {
            List<Result> cosineResultList = new List<Result>();

            for (int i = 0; i < univercityPagewordList.Count; i++)
            {
                List<Word> searchWordList = univercityPagewordList[i][univercityPagewordList[i].Count - 1];
                double searchListSquareSum = 0;
                for (int h = 0; h < searchWordList.Count; h++)  //aranacak kelimelerin büyüklüğü - uzunluk
                {
                    searchListSquareSum += Math.Pow(searchWordList[h].TfIdf, 2);
                }
                for (int j = 0; j < univercityPagewordList[i].Count - 1; j++)
                {
                    double productTfidfSum = 0;
                    double tfIdfsaquareSum = 0;


                    for (int l = 0; l < univercityPagewordList[i][j].Count; l++)
                    {
                        for (int k = 0; k < searchWordList.Count; k++)
                        {
                            // if (univercityPagewordList[i][j][l].Name == "dokuz" && i == 1)
                            {
                                if (searchWordList[k].Name == univercityPagewordList[i][j][l].Name)
                                {
                                    productTfidfSum += (univercityPagewordList[i][j][l].Tf * univercityPagewordList[i][j][l].NextIdf) * searchWordList[k].TfIdf;
                                    tfIdfsaquareSum += Math.Pow((univercityPagewordList[i][j][l].Tf * univercityPagewordList[i][j][l].NextIdf), 2);
                                }
                                else
                                {
                                    tfIdfsaquareSum += Math.Pow(univercityPagewordList[i][j][l].TfIdf, 2);
                                }
                            }
                        }


                    }

                    Result result = new Result();
                    result.I = i;
                    result.J = int.Parse(Path.GetFileNameWithoutExtension(files[i][j]));
                    result.Cosine = productTfidfSum / (Math.Sqrt(tfIdfsaquareSum) * Math.Sqrt(searchListSquareSum));
                    if (!double.IsNaN(result.Cosine) && result.Cosine != 0)
                    {
                        cosineResultList.Add(result);
                    }



                }
            }
            cosineResultList.Sort();
            return cosineResultList;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                webBrowser1.Navigate(listBox1.SelectedItem.ToString());
            }
            catch (Exception)
            {


            }

        }
        private void CleanStopWords(List<Word> words)
        {
            List<string> stopWords = new List<string>(new string[] { "ve","veya","ama", "ancak", "arada", "bazı", "beri", "bile", "biz", "bu", "şu", "çok"
            ,"daha","da","de","değil","en","gibi","hem","göre","hep","her","ise","ile","kadar","kendi","kez","ki","mu","ne","onlar","ben","sen","pek","şey","tüm","zaten","ya"});
            string temp = "";
            for (int i = 0; i < words.Count; i++)
            {
                if (words[i].Name.Length <= 2 || (temp = stopWords.Find(word => { if (word == words[i].Name) return true; else return false; })) == words[i].Name)
                {
                    words.RemoveAt(i);
                    i--;
                }
            }

        }

        private void rawFrequencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Word k;
            StreamWriter sw = new StreamWriter(pathU + "\\rawfrequency.csv");
            sw.Write("\t");
            for (int i = 0; i < uWords.Count; i++)
            {
                sw.Write(";" + uWords[i]);
            }
            sw.WriteLine();


            for (int i = 0; i < univercityWordlist.Count; i++)
            {
                sw.Write(Path.GetFileNameWithoutExtension(Directory.GetFiles(pathU)[i]));

                for (int j = 0; j < uWords.Count; j++)
                {
                    k = univercityWordlist[i].Find(temp => { if (temp.Name == uWords[j]) return true; else return false; });
                    if (k != null)
                    {
                        sw.Write(";" + k.Count);
                    }
                    else
                    {
                        sw.Write(";");
                    }
                }
                sw.WriteLine();

            }
            sw.Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(pathU + "\\tfidf.csv");
            sw.Write("\t");
            for (int i = 0; i < uWords.Count; i++)
            {
                sw.Write(";" + uWords[i]);
            }
            sw.WriteLine();
            Word k;

            for (int i = 0; i < univercityWordlist.Count; i++)
            {
                sw.Write(Path.GetFileNameWithoutExtension(Directory.GetFiles(pathU)[i]));

                for (int j = 0; j < uWords.Count; j++)
                {
                    k = univercityWordlist[i].Find(temp => { if (temp.Name == uWords[j]) return true; else return false; });
                    if (k != null)
                    {
                        sw.Write(";" + k.TfIdf);
                    }
                    else
                    {
                        sw.Write(";");
                    }
                }
                sw.WriteLine();

            }
            sw.Close();
        }

        private void rawFrequecyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(pathU + "\\pageRawFrequency.csv");

            sw.Write("\t");
            for (int j = 0; j < uWords.Count; j++)
            {
                sw.Write(";" + uWords[j]);
            }
            Word ke;
            sw.WriteLine();
            for (int i = 0; i < univercityPagewordList.Count; i++)
            {
                for (int j = 0; j < univercityPagewordList[i].Count; j++)
                {
                    sw.Write(Path.GetFileNameWithoutExtension(Directory.GetFiles(pathU)[i]) + " " + j);
                    for (int k = 0; k < uWords.Count; k++)
                    {
                        ke = univercityPagewordList[i][j].Find(temp => { if (temp.Name == uWords[k]) return true; else return false; });
                        if (ke != null)
                        {
                            sw.Write(";" + ke.Count);
                        }
                        else
                        {
                            sw.Write(";");
                        }

                    }

                    sw.WriteLine();
                }
                sw.WriteLine();
            }
            sw.Close();
        }

        private void ınverseTermFrequencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(pathU + "\\pageTfIdf.csv");

            sw.Write("\t");
            for (int j = 0; j < uWords.Count; j++)
            {
                sw.Write(";" + uWords[j]);
            }
            Word ke;
            sw.WriteLine();
            for (int i = 0; i < univercityPagewordList.Count; i++)
            {
                for (int j = 0; j < univercityPagewordList[i].Count; j++)
                {
                    sw.Write(Path.GetFileNameWithoutExtension(Directory.GetFiles(pathU)[i]) + " " + j);
                    for (int k = 0; k < uWords.Count; k++)
                    {
                        ke = univercityPagewordList[i][j].Find(temp => { if (temp.Name == uWords[k]) return true; else return false; });
                        if (ke != null)
                        {
                            sw.Write(";" + ke.TfIdf);
                        }
                        else
                        {
                            sw.Write(";");
                        }

                    }

                    sw.WriteLine();
                }
                sw.WriteLine();
            }
            sw.Close();
        }
    }
}
