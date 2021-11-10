using System;
//for using lists
using System.Collections.Generic;
//for exporting files
using System.IO;
//to serialize output files, because we were doing this in class
using System.Runtime.Serialization.Formatters.Binary;
//to scrape data at xpath, all web scraping activety/faculties
using HtmlAgilityPack;
//using this library (not mine) for ascii art (CodeCredit in code location)
using TextToAsciiArt;
//need to pause (literally only used once)
using System.Threading;

namespace WebScraperDemo_ScrapeAnyWikiSite
{
    //TO FINISH (not as critical)
    //ERROR CHECKING
    //MAKE CMD BOX START IN CENTER OF SCREEN
    //MAKE A FILE EXPLORER WINDOW POPUP AND LET YOU SAVE FILE TO CUSTOM LOCAION

    //test links, if u want to but u don't need to
    //https://en.wikipedia.org/wiki/Canada
    //https://en.wikipedia.org/wiki/War

    public class Program
    {
        //for exporting files, related to naming schema
        public static int ExportNumber = 0;

        //create lists to store ripped information
        public static List<string> LinksList { get; set; }
        public static List<string> ImageLinksList { get; set; }
        public static List<string> ParagraphsList { get; set; }
        public static List<string> WordsFoundList { get; set; }
        
        //create nessisary objects, former to load website, ladder to store website html in doc
        public static HtmlWeb myWebsite { get; set; }
        public static HtmlDocument myDocument { get; set; }

        //first to be called
        static void Main(/*string[] args*/)
        {
            //Write out menu in ASCII art!
            #region CodeCredit_ASCII_ART
            /*
             * This is credit to Raj Kumar Rsys for making the NuGet Library I am using.
             * I am using his library for the beginning ascii art. The title ascii art that is.
             * This is where I am using HIS program, not anywhere else though. 
             * 
             * CodeCreditLink: https://www.asptricks.net/2020/01/text-to-ascii-art-generator-nuget.html
             * 
             * Note To Myself: I should try to write my own ascii art program as a project!
             */
            #endregion
            ArtWriter artWritter = new ArtWriter();
            string programTitle_inAsciiArt = artWritter.WriteString("WIKI     RIP");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(programTitle_inAsciiArt);
            Console.WriteLine("----------------------------------------------------------------------");
            Console.ResetColor();

            //
            bool goodWebsite = false;
            string targetWebsite = string.Empty;

            //
            while (goodWebsite != true)
            {
                //ask for page to scrape, read next line into a string
                Console.WriteLine("Enter the wiki page to scrape:");
                targetWebsite = CustomUserInput();
                //make sure user enters acceptable website, needs to be link, needs to be to a wiki page
                if (!targetWebsite.Contains("https://") && !targetWebsite.Contains("wiki"))
                    //give user a red message, they must enter an actual URL to wiki page
                    CustomConsoleMessageLine("Please enter valid URL!", false);
                else goodWebsite = true;
            }
            //instanciate myWebsite, load myDocument with targetWebsite
            myWebsite = new HtmlWeb();
            Console.WriteLine("Searching for webpage:");
            myDocument = myWebsite.Load(targetWebsite);
            CustomConsoleMessageLine("Loaded Website!\n", true);
            //
            bool success = false;
            //
            do
            {
                //ask the user what they want to do, give options (use switch statement)
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("What would you like to search website for?");
                Console.ResetColor();
                //give user a series of shuggestions/options to choose from
                Console.WriteLine("[1] Look for links!");
                Console.WriteLine("[2] Look for image links!");
                Console.WriteLine("[3] Look for a word!");
                Console.WriteLine("[99] End Program...");
                //action based upon what user picked, use custom user input method
                string userPick = CustomUserInput();
                switch (userPick)
                {
                    case "1":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nLinkFinder----------------");
                        Console.ResetColor();
                        Console.WriteLine("Searching for links in the site!");
                        //call FindLinks, write the returned stirng to console
                        FindLinks();
                        //propt user for a responce
                        Console.WriteLine("\nWould you like to:");
                        Console.WriteLine("[1] View links!");
                        Console.WriteLine("[2] Export links!");
                        Console.WriteLine("[99] Past menu!");
                        string userResponce_1 = CustomUserInput();
                        if (userResponce_1 == "1")
                            View(LinksList);
                        else if (userResponce_1 == "2")
                            ExportToTxtFile(LinksList);
                        else if (userResponce_1 == "99")
                            Console.WriteLine("\nReturning to previous menu");
                        else
                            CustomConsoleMessageLine("Please enter an acceptable option", false);
                        
                        //might have to also reset success = false in start of loop but idk for sure

                        //should I only break if sucess == true??? PROLLY THSI ONE
                        //nvm it's fine I just did a == instead of a != in the while loop lol
                        break;

                    case "2":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nImageLinkFinder----------------");
                        Console.ResetColor();
                        Console.WriteLine("Searching for image links in the site!");
                        //call FindImageLinks, write the returned stirng to console
                        FindImageLinks();
                        //propt user for a responce
                        Console.WriteLine("\nWould you like to:");
                        Console.WriteLine("[1] View links!");
                        Console.WriteLine("[2] Export image links!");
                        Console.WriteLine("[99] Past menu!");
                        string userResponce_2 = CustomUserInput();
                        if (userResponce_2 == "1")
                            View(ImageLinksList);
                        else if (userResponce_2 == "2")
                            ExportToTxtFile(ImageLinksList);
                        else if (userResponce_2 == "99")
                            Console.WriteLine("\nReturning to previous menu");
                        else
                            CustomConsoleMessageLine("Please enter an acceptable option", false);
                        
                        break;

                    case "3":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nWordFinder----------------");
                        Console.ResetColor();
                        Console.WriteLine("Pulling text from the site!");
                        Console.WriteLine("What word would you like to look for?");
                        string userWordShuggestion = CustomUserInput();
                        //error check to make sure it is only one word
                        if (userWordShuggestion.Contains(' '))
                        {
                            CustomConsoleMessageLine("Please only specify ONE WORD!", false);
                            return;
                        }
                        else if (userWordShuggestion == "" | userWordShuggestion is null)
                        {
                            CustomConsoleMessageLine("Please specify a word!", false);
                            return;
                        }
                        else FindWord(userWordShuggestion);
                        //propt user for a responce
                        Console.WriteLine("\nWould you like to:");
                        Console.WriteLine("[1] View paragraphs that contained specified words!");
                        Console.WriteLine("[2] Export words!");
                        Console.WriteLine("[99] Past menu!");
                        //
                        string userResponce_3 = CustomUserInput();
                        if (userResponce_3 == "1")
                            ViewParagraphs(userWordShuggestion);
                        else if (userResponce_3 == "2")
                            //ExportToTxtFile(WordsFoundList);
                            CustomConsoleMessageLine("Sorry but we are doing matanence on this feature at the moment...", false);
                        else if (userResponce_3 == "99")
                            Console.WriteLine("\nReturning to previous menu");
                        else
                            CustomConsoleMessageLine("Please enter an acceptable option", false);
                        
                        break;

                    case "99":
                        //user is done with program for now
                        success = true;

                        //clear console
                        Console.Clear();

                        //write ascii text saying goodbye
                        string exitMessage = artWritter.WriteString("GOOD     BYE");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(exitMessage);
                        Console.WriteLine("----------------------------------------------------------------------");
                        
                        //wait 5 seconds and then exit
                        Thread.Sleep(1000);
                        Console.WriteLine(5 + "\n");

                        Thread.Sleep(1000);
                        Console.WriteLine(4 + "\n");

                        Thread.Sleep(1000);
                        Console.WriteLine(3 + "\n");

                        Thread.Sleep(1000);
                        Console.WriteLine(2 + "\n");

                        Thread.Sleep(1000);
                        Console.WriteLine(1 + "\n");

                        //no need to reset color again but it will bother me if I don't
                        Console.ResetColor();
                        
                        //breaking here should exit loop and program,
                        //no need for calling exit as program will inform user and exit on any keypress
                        break;
                    
                    //defualt option if not satisfied
                    default:
                        CustomConsoleMessageLine("Please seelct a valid option:", false);
                        break;
                }

            } while (success != true);
        }

        //CHANGE ALL METHODS TO PRIVATE AT SOME POINT
        public static void ViewParagraphs(string thatWord)
        {
            CustomConsoleMessageLine("\nStart of paragraphs----------------", true);
            //split paragraph into words, paragraph already initalized when find word called, go through paragraphs
            foreach (string paragraph2 in ParagraphsList)
            {
                //split paragraph by spaces, into words list
                string[] tmpParagraph = paragraph2.Split(' ');

                //go through each word in each paragraph
                for (int y = 0; y < tmpParagraph.Length; y++)
                {
                    //clean all words
                    tmpParagraph[y] = tmpParagraph[y].CleanResponce();
                    if (tmpParagraph[y] != "")
                    {

                        if (tmpParagraph[y].ToUpper().Contains(thatWord.ToUpper()))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write(tmpParagraph[y] + " ");
                            Console.ForegroundColor = ConsoleColor.White;//could also say color reset
                        }
                        else
                        {
                            Console.Write(tmpParagraph[y] + " ");
                        }
                    }
                }
                //between paragraphs have a space
                Console.WriteLine("\n");
            }
        }

        //look for link tag/attribute and save link
        public static string FindLinks()
        {    
            //new instance list every time, does not have old list contents
            LinksList = new List<string>();
            //looking for links in site
            foreach (var item in myDocument.DocumentNode.SelectNodes("//a/@href"))
            {
                string cleanHrefLink = item.OuterHtml;
                int startOfLink = cleanHrefLink.IndexOf("href");
                int endOflink = cleanHrefLink.IndexOf(' ', startOfLink);
                int lengthOfLink = endOflink - startOfLink;
                //Console.WriteLine(cleanHrefLink.Substring(startOfLink, /*lengthOfLink*/ cleanHrefLink.Length));
                if (lengthOfLink > 0)
                    //Console.WriteLine(cleanHrefLink.Substring(startOfLink, lengthOfLink));
                    LinksList.Add(cleanHrefLink.Substring(startOfLink, lengthOfLink));
                else LinksList.Add("NON VIABLE LINK!");
            }
            //
            return CustomConsoleMessageLine($"We found {LinksList.Count} links!", true);
        }
        
        //look for a image attribute then save link
        private static string FindImageLinks()
        {
            //
            ImageLinksList = new List<string>();
            //looking for links in site
            foreach (var item in myDocument.DocumentNode.SelectNodes("//a/img/@src"))
            {
                string cleanHrefLink = item.OuterHtml;
                int startOfLink = cleanHrefLink.IndexOf("src");
                int endOflink = cleanHrefLink.IndexOf(' ', startOfLink);
                int lengthOfLink = endOflink - startOfLink;
                if (lengthOfLink > 0)
                    //Console.WriteLine(cleanHrefLink.Substring(startOfLink, lengthOfLink));
                    ImageLinksList.Add(cleanHrefLink.Substring(startOfLink, lengthOfLink));
                else ImageLinksList.Add("NON VIABLE LINK!");
            }
            //
            return CustomConsoleMessageLine($"We found {ImageLinksList.Count} image links!", true);
        }
        
        //to find a word in a paragraph/site
        public static void FindWord(string word)
        {
            //instanciate a new word list
            ParagraphsList = new List<string>();

            //pull paragraphs from the website, at this xpath
            foreach (var item in myDocument.DocumentNode.SelectNodes("//p"))
                ParagraphsList.Add(item.InnerText);

            //inform user of working state
            Console.WriteLine($"\nSearching through {ParagraphsList.Count} paragraphs for {word}!");

            //split paragraph into words, delimiter based on spaces
            int instancesOfWord = 0;
            foreach (string paragraph in ParagraphsList)
            {
                //split paragraph by spaces, into words list
                string[] tmpParagraph = paragraph.Split(' ');
                //search/count instances of matching words in the array
                foreach (string theword in tmpParagraph)
                    //check words reguardless of capitalization, don't need to match case
                    if (theword.ToUpper().Contains(word.ToUpper()))
                        instancesOfWord++;
                        //return a paragraph with the text of words highlighted!!!!! DO THIS, DON'T FORGET
            }

            //error checking in case there is no matches, really just a custom responce
            if (instancesOfWord == 0)
                CustomConsoleMessageLine("We didn't find any instance of your word in the website :-(", false);
            else if (instancesOfWord > 0)
                CustomConsoleMessageLine($"We found {instancesOfWord} differnt occurances of your selected word", true);
        }
        
        //write the stuff to save to a file
        public static void ExportToTxtFile(List<string> ListOfWordsFound)
        {
            //get path to desktop desktop, taken from another project I was experimenting with
            string deployPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            //should I create a seralized file for practice?
            ExportNumber++;
            using (StreamWriter writer = new StreamWriter($"{deployPath}\\textFile{ExportNumber}.txt"))
            {
                foreach (string finding in ListOfWordsFound)
                {
                    writer.WriteLine(finding);
                }
            }

            //for some reason I can't add system forms idalog here. Prolly an issue wiht cmd compatabilt8y.
            


            //------------------
            using (Stream stream = File.Open($"{deployPath}\\seralizedFileExport{ExportNumber}.bin", FileMode.Create))
            {
                //create a binary formatter, to turn our object into binary format
                BinaryFormatter bin = new BinaryFormatter();
                //specify the stream and seralize the list of cars
                bin.Serialize(stream, ListOfWordsFound);
            }
            //tell the user we exporte a file, really should let them decide where I put file
            CustomConsoleMessageLine("Exported one text and one seralized bin file to desktop!\n", true);
        }

        public static void View(List<string> somelist)
        {
            //
            CustomConsoleMessageLine($"Here is {somelist.Count} items we found:", true);
            //
            for (int x = 0; x < somelist.Count; x++)
                Console.WriteLine(somelist[x]);
            //
            CustomConsoleMessageLine("Finished displaying--------------------------\n", true);
        }

        //method to write in speific color, got this idea from udemy course on selenium, I modified code!
        public static string CustomConsoleMessageLine(string message, bool goodmessage /* true-good | false-bad*/)
        {
            if (goodmessage == true) Console.ForegroundColor = ConsoleColor.Green;
            else Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            return message;
        }
        
        //a custom colored user input, will be pink (magenta ig)
        public static string CustomUserInput()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            string userInput = Console.ReadLine();
            Console.ResetColor();
            return userInput;
        }
    }
}
