using System;
using System.Collections.Generic;
using System.Text;

namespace WebScraperDemo_ScrapeAnyWikiSite
{
    public static class MethodExtensions
    {
        //clean the random characters from the responce, apply this in loop to each word in array
        public static string CleanResponce(this string singleRawWord)
        {//NEED TO CHECK LOGIC FOR IF ITS NEGATIVE 1
            //if nothing found retCleanWord will be origional
            string retCleanWord = singleRawWord;
            //track where char index the bad chars starts at
            int firstOccuranceOfBadChar = -1;
            //check for characters that I want to remove
            if (singleRawWord.Contains('#'))
                //find index of char where the stuff starts
                if (firstOccuranceOfBadChar > singleRawWord.IndexOf('#') && firstOccuranceOfBadChar != -1)
                    firstOccuranceOfBadChar = singleRawWord.IndexOf('#');
                else if (firstOccuranceOfBadChar == -1)
                    firstOccuranceOfBadChar = singleRawWord.IndexOf('#');
            if (singleRawWord.Contains('$'))
                if (firstOccuranceOfBadChar > singleRawWord.IndexOf('$') && firstOccuranceOfBadChar != -1)
                    firstOccuranceOfBadChar = singleRawWord.IndexOf('$');
                else if (firstOccuranceOfBadChar == -1)
                    firstOccuranceOfBadChar = singleRawWord.IndexOf('$');
            if (singleRawWord.Contains('%'))
                if (firstOccuranceOfBadChar > singleRawWord.IndexOf('%') && firstOccuranceOfBadChar != -1)
                    firstOccuranceOfBadChar = singleRawWord.IndexOf('%');
                else if (firstOccuranceOfBadChar == -1)
                    firstOccuranceOfBadChar = singleRawWord.IndexOf('%');
            if (singleRawWord.Contains('&'))
                if (firstOccuranceOfBadChar > singleRawWord.IndexOf('&') && firstOccuranceOfBadChar != -1)
                    firstOccuranceOfBadChar = singleRawWord.IndexOf('&');
                else if (firstOccuranceOfBadChar == -1)
                    firstOccuranceOfBadChar = singleRawWord.IndexOf('&');
            if (singleRawWord.Contains('*'))
                if (firstOccuranceOfBadChar > singleRawWord.IndexOf('*') && firstOccuranceOfBadChar != -1)
                    firstOccuranceOfBadChar = singleRawWord.IndexOf('*');
                else if (firstOccuranceOfBadChar == -1)
                    firstOccuranceOfBadChar = singleRawWord.IndexOf('*');
            if (singleRawWord.Contains('\n'))
                if (firstOccuranceOfBadChar > singleRawWord.IndexOf('\n') && firstOccuranceOfBadChar != -1)
                    firstOccuranceOfBadChar = singleRawWord.IndexOf('\n');
                //here I also have to check to make sure its not at start of sentance/word
                else if (firstOccuranceOfBadChar == -1 && singleRawWord.IndexOf("\n") > 0)
                    firstOccuranceOfBadChar = singleRawWord.IndexOf('\n');

            //remove everything after the endex of the fist offrance of bad characters
            if (firstOccuranceOfBadChar != -1 /*we are assuming it will be defualt -1 if nothing was found*/)
                retCleanWord = singleRawWord.Remove(firstOccuranceOfBadChar);

            //return the cleaned word
            return retCleanWord;
        }
    }
}
